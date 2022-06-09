using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel.Models
{
    public partial class CarRentalContext
    {
        public Manufacturer? GetManufacturer(string mname)
        {
            return Manufacturers.FirstOrDefault(m => m.Name == mname);
        }
        public EngineType? GetEngineType(string etype)
        {
            return EngineTypes.FirstOrDefault(e => e.Name == etype);
        }

        public Accessory? GetAccessory(string aname)
        {
            return Accessories.FirstOrDefault(m => m.Name == aname);
        }
        public TransmissionType? GetTransmission(string tname)
        {
            return TransmissionTypes.FirstOrDefault(t => t.Name == tname);
        }

        public VehicleCategory? GetVehicleCategory(string cname)
        {
            return VehicleCategories.FirstOrDefault(m => m.Name == cname);
        }

        public IQueryable<Accessory> GetVehicoleAccessories(Vehicle v)
        {
            return VehicleAccessories
                .Where(a => a.VehicleId == v.Id)
                .Select(va => va.Accessory);
        }

        public Vehicle? BuildVehicle(string name, string category, string manufacturer, string transmission, string engine, IEnumerable<(string, int)> accessories = null)
        {
            var vehicle = new Vehicle()
            {
                Name = name,
                CategoryNavigation = GetVehicleCategory(category),
                ManufacturerNavigation = GetManufacturer(manufacturer),
                TransmissionNavigation = GetTransmission(transmission),
                EngineNavigation = GetEngineType(engine),
            };

            foreach (var a in accessories)
            {
                vehicle.VehicleAccessories.Add(new VehicleAccessory()
                {
                    Accessory = GetAccessory(a.Item1),
                    Status = a.Item2
                });
            }
            
            return vehicle;
        }
    }
}
