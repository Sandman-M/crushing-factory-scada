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

            AddOrUpdate(new Equipment { Id = "kkd-11", Name = "Дробарка ККД-11", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "krd-12", Name = "Дробарка КРД-12", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "krd-13", Name = "Дробарка КРД-13", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });

            AddOrUpdate(new Equipment { Id = "ksd-3", Name = "Дробарка КСД-3", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "ksd-4", Name = "Дробарка КСД-4", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "ksd-5", Name = "Дробарка КСД-5", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "ksd-6", Name = "Дробарка КСД-6", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });

            AddOrUpdate(new Equipment { Id = "kmd-3", Name = "Дробарка КМД-3", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "kmd-4", Name = "Дробарка КМД-4", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "kmd-5", Name = "Дробарка КМД-5", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });
            AddOrUpdate(new Equipment { Id = "kmd-6", Name = "Дробарка КМД-6", Type = "Crusher", Status = EquipmentStatuses.Off, Temperature = 20, Load = 0 });

            AddOrUpdate(new Equipment { Id = "conv-k1-m1", Name = "Конвеєр К1-M1", Type = "Conveyor" ,Status = EquipmentStatuses.Off });
            AddOrUpdate(new Equipment { Id = "conv-k2-m2", Name = "Конвеєр К2-M2", Type = "Conveyor" ,Status = EquipmentStatuses.Off });
            AddOrUpdate(new Equipment { Id = "conv-m15", Name = "Конвеєр М-15", Type = "Conveyor" ,Status = EquipmentStatuses.Off });

            AddOrUpdate(new Equipment { Id = "bn-kkd11", Name = "Бункер ККД 11", Type = "Bunker", Status = EquipmentStatuses.Off, Level = 50 });
            AddOrUpdate(new Equipment { Id = "bn-krd1213", Name = "Бункер КРД 12-13", Type = "Bunker", Status = EquipmentStatuses.Off, Level = 50 });
            AddOrUpdate(new Equipment { Id = "bn-krd1213(2)", Name = "Бункер КРД 12-13(2)", Type = "Bunker", Status = EquipmentStatuses.Off, Level = 50 });
        }

        public void AddOrUpdate(Equipment eq)
        {
            _equipmentItems[eq.Id] = eq;
        }

        public IEnumerable<Equipment> GetAll() => _equipmentItems.Values;
    }
}
