using FactoryApi.Models;
using System.Collections.Concurrent;

namespace FactoryApi.Services
{
    public class EquipmentStateStore
    {
        private readonly ConcurrentDictionary<string, Equipment> _equipmentItems;

        public EquipmentStateStore()
        {
            _equipmentItems = new ConcurrentDictionary<string, Equipment>();

            AddOrUpdate(new Equipment { Id = "kkd-11", Name = "Дробарка ККД-11", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "krd-12", Name = "Дробарка КРД-12", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "krd-13", Name = "Дробарка КРД-13", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });

            AddOrUpdate(new Equipment { Id = "ksd-3", Name = "Дробарка КСД-3", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "ksd-4", Name = "Дробарка КСД-4", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "ksd-5", Name = "Дробарка КСД-5", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "ksd-6", Name = "Дробарка КСД-6", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });

            AddOrUpdate(new Equipment { Id = "kmd-3", Name = "Дробарка КМД-3", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "kmd-4", Name = "Дробарка КМД-4", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "kmd-5", Name = "Дробарка КМД-5", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "kmd-6", Name = "Дробарка КМД-6", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });

            AddOrUpdate(new Equipment { Id = "conv-k1-m1", Name = "Конвеєр К1-M1", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "conv-k2-m2", Name = "Конвеєр К2-M2", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            //AddOrUpdate(new Equipment { Id = "conv-m1", Name = "Конвеєр М-1", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            //AddOrUpdate(new Equipment { Id = "conv-m2", Name = "Конвеєр М-2", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "conv-m15", Name = "Конвеєр М-15", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
        }

        public void AddOrUpdate(Equipment eq)
        {
            _equipmentItems[eq.Id] = eq;
        }

        public IEnumerable<Equipment> GetAll() => _equipmentItems.Values;
    }
}
