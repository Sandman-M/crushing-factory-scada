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

                foreach (var eq in allEquipment)
                {
                    if (_random.NextDouble() < 0.1)
                    {
                        eq.Status = EquipmentStatuses.Off;
                        eq.Load = 0;
                        eq.Temperature = 20;
                    }
                    else
                    {
                        if (_random.NextDouble() < 0.15)
                        {
                            eq.Status = EquipmentStatuses.Idle;
                            eq.Load = 0;
                            eq.Temperature = _random.Next(30, 50);
                        }
                        else
                        {
                            eq.Load = _random.Next(40, 101);
                            eq.Temperature = _random.Next(50, 96);

                            if (eq.Temperature >= 90 || eq.Load >= 98)
                            {
                                eq.Status = EquipmentStatuses.Alarm;
                            }
                            else if (eq.Temperature >= 80 || eq.Load >= 85)
                            {
                                eq.Status = EquipmentStatuses.Warning;
                            }
                            else
                            {
                                eq.Status = EquipmentStatuses.Run;
                            }
                        }
                    }

                    _stateStore.AddOrUpdate(eq);
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
