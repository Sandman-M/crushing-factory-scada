using FactoryApi.Models;

namespace FactoryApi.Services
{
    public class SimulationWorker : BackgroundService
    {
        private readonly EquipmentStateStore _stateStore;
        private readonly Random _random = new Random();

        public SimulationWorker(EquipmentStateStore stateStore)
        {
            _stateStore = stateStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var allEquipment = _stateStore.GetAll().ToList();
                var eqDict = allEquipment.ToDictionary(e => e.Id);

                foreach (var eq in allEquipment)
                {
                    if (eq.Type == "Bunker")
                    {
                        int currentLevel = eq.Level ?? 50;
                        currentLevel += _random.Next(-5, 6);
                        currentLevel = Math.Clamp(currentLevel, 0, 100);
                        eq.Level = currentLevel;

                        if (currentLevel >= 95) eq.Status = EquipmentStatuses.Alarm;
                        else if (currentLevel >= 85) eq.Status = EquipmentStatuses.Warning;
                        else eq.Status = EquipmentStatuses.Run;

                        continue;
                    }

                    if (_random.NextDouble() < 0.1)
                    {
                        eq.Status = EquipmentStatuses.Off;
                        eq.Load = 0;
                        if (eq.Type == "Crusher") eq.Temperature = 20;
                    }
                    else if (_random.NextDouble() < 0.15)
                    {
                        eq.Status = EquipmentStatuses.Idle;
                        eq.Load = 0;
                        if (eq.Type == "Crusher") eq.Temperature = _random.Next(30, 50);
                    }
                    else
                    {
                        eq.Load = _random.Next(40, 101);
                        if (eq.Type == "Crusher")
                        {
                            eq.Temperature = _random.Next(50, 96);
                            if (eq.Temperature >= 90 || eq.Load >= 98) eq.Status = EquipmentStatuses.Alarm;
                            else if (eq.Temperature >= 80 || eq.Load >= 85) eq.Status = EquipmentStatuses.Warning;
                            else eq.Status = EquipmentStatuses.Run;
                        }
                        else
                        {
                            eq.Temperature = null;
                            if (eq.Load >= 98) eq.Status = EquipmentStatuses.Alarm;
                            else eq.Status = EquipmentStatuses.Run;
                        }
                    }
                }

                if (eqDict.TryGetValue("conv-k1-m1", out var line1) && eqDict.TryGetValue("conv-k2-m2", out var line2))
                {
                    if (line1.Status != EquipmentStatuses.Run && line2.Status != EquipmentStatuses.Run)
                    {
                        line1.Status = EquipmentStatuses.Run;
                        line1.Load = _random.Next(50, 80);
                    }
                }

                void SyncPair(string id1, string id2)
                {
                    if (eqDict.TryGetValue(id1, out var eq1) && eqDict.TryGetValue(id2, out var eq2))
                    {
                        if (eq1.Status == EquipmentStatuses.Off || eq1.Status == EquipmentStatuses.Alarm)
                        {
                            eq2.Status = EquipmentStatuses.Off;
                            eq2.Load = 0;
                            eq2.Temperature = 20;
                        }
                        else if (eq2.Status == EquipmentStatuses.Off || eq2.Status == EquipmentStatuses.Alarm)
                        {
                            eq1.Status = EquipmentStatuses.Off;
                            eq1.Load = 0;
                            eq1.Temperature = 20;
                        }
                    }
                }

                SyncPair("ksd-3", "kmd-3");
                SyncPair("ksd-4", "kmd-4");
                SyncPair("ksd-5", "kmd-5");
                SyncPair("ksd-6", "kmd-6");

                foreach (var eq in allEquipment)
                {
                    _stateStore.AddOrUpdate(eq);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}