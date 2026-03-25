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
                    SimulateEquipmentState(eq);
                }

                ApplyConveyorInterlock(eqDict);
                ApplyCrusherPairsSync(eqDict);
                ApplyCascadeStop(allEquipment, eqDict);

                foreach (var eq in allEquipment)
                {
                    _stateStore.AddOrUpdate(eq);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        private void SimulateEquipmentState(Equipment eq)
        {
            switch (eq.Type)
            {
                case "Bunker":
                    SimulateBunker(eq);
                    break;
                default:
                    SimulateMachine(eq);
                    break;
            }
        }

        private void SimulateBunker(Equipment eq)
        {
            int currentLevel = eq.Level ?? 50;
            currentLevel += _random.Next(-5, 6);
            currentLevel = Math.Clamp(currentLevel, 0, 100);
            eq.Level = currentLevel;

            if (currentLevel >= 95) eq.Status = EquipmentStatuses.Alarm;
            else if (currentLevel >= 85) eq.Status = EquipmentStatuses.Warning;
            else eq.Status = EquipmentStatuses.Run;
        }

        private void SimulateMachine(Equipment eq)
        {
            double chance = _random.NextDouble();

            if (chance < 0.1)
            {
                StopEquipment(eq);
            }
            else if (chance < 0.15)
            {
                eq.Status = EquipmentStatuses.Idle;
                eq.Load = 0;
                if (eq.Type == "Crusher") eq.Temperature = _random.Next(30, 50);
            }
            else
            {
                eq.Load = _random.Next(40, 101);

                switch (eq.Type)
                {
                    case "Crusher":
                        eq.Temperature = _random.Next(50, 96);
                        if (eq.Temperature >= 90 || eq.Load >= 98) eq.Status = EquipmentStatuses.Alarm;
                        else if (eq.Temperature >= 80 || eq.Load >= 85) eq.Status = EquipmentStatuses.Warning;
                        else eq.Status = EquipmentStatuses.Run;
                        break;

                    case "Conveyor":
                        eq.Temperature = null;
                        if (eq.Load >= 98) eq.Status = EquipmentStatuses.Alarm;
                        else eq.Status = EquipmentStatuses.Run;
                        break;
                }
            }
        }

        private void ApplyConveyorInterlock(Dictionary<string, Equipment> eqDict)
        {
            if (eqDict.TryGetValue("conv-k1-m1", out var line1) && eqDict.TryGetValue("conv-k2-m2", out var line2))
            {
                if (line1.Status != EquipmentStatuses.Run && line2.Status != EquipmentStatuses.Run)
                {
                    line1.Status = EquipmentStatuses.Run;
                    line1.Load = _random.Next(50, 80);
                }
            }
        }

        private void ApplyCrusherPairsSync(Dictionary<string, Equipment> eqDict)
        {
            SyncPair(eqDict, "ksd-3", "kmd-3");
            SyncPair(eqDict, "ksd-4", "kmd-4");
            SyncPair(eqDict, "ksd-5", "kmd-5");
            SyncPair(eqDict, "ksd-6", "kmd-6");
        }

        private void SyncPair(Dictionary<string, Equipment> eqDict, string id1, string id2)
        {
            if (eqDict.TryGetValue(id1, out var eq1) && eqDict.TryGetValue(id2, out var eq2))
            {
                bool isEq1Stopped = eq1.Status == EquipmentStatuses.Off || eq1.Status == EquipmentStatuses.Alarm;
                bool isEq2Stopped = eq2.Status == EquipmentStatuses.Off || eq2.Status == EquipmentStatuses.Alarm;

                if (isEq1Stopped && !isEq2Stopped)
                {
                    StopEquipment(eq2);
                }
                else if (isEq2Stopped && !isEq1Stopped)
                {
                    StopEquipment(eq1);
                }
            }
        }

        private void ApplyCascadeStop(List<Equipment> allEquipment, Dictionary<string, Equipment> eqDict)
        {
            if (eqDict.TryGetValue("conv-m15", out var m15))
            {
                if (m15.Status == EquipmentStatuses.Off || m15.Status == EquipmentStatuses.Alarm)
                {
                    foreach (var eq in allEquipment)
                    {
                        if (eq.Id != "conv-m15" && eq.Type != "Bunker")
                        {
                            StopEquipment(eq);
                        }
                    }
                }
            }
        }

        private void StopEquipment(Equipment eq)
        {
            eq.Status = EquipmentStatuses.Off;
            eq.Load = 0;
            if (eq.Type == "Crusher")
            {
                eq.Temperature = 20;
            }
        }
    }
}