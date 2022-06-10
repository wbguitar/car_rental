using Microsoft.EntityFrameworkCore;
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


        public void SendToMaintenance(string vehicleName, DateTime untill)
        {
            var releaseDate = untill.ToString("yyyy-MM-dd HH:mm:ss");
            var query = $"exec sp_sendToMaintenance '{vehicleName}', '{releaseDate}'";
            Vehicles.FromSqlRaw(query).ToList();
        }

        public void RemoveFromMaintenances(string vehicleName)
        {
            var query = $"exec sp_removeFromMaintenance '{vehicleName}'";
            Vehicles.FromSqlRaw(query).ToList();
        }

        public void RentVehicle(string customer, string vehicleName, DateTime dtFrom, DateTime dtTo)
        {
            var sfrom = dtFrom.ToString("yyyy-MM-dd HH:mm:ss");
            var sto = dtTo.ToString("yyyy-MM-dd HH:mm:ss");
            var query = $"exec sp_RentVehicle '{customer}', '{vehicleName}', '{sfrom}', '{sto}'";
            Vehicles.FromSqlRaw(query).ToList();
        }
        public void UnrentVehicle(string vehicleName)
        {
            var query = $"exec sp_UnrentVehicle '{vehicleName}'";
            Vehicles.FromSqlRaw(query).ToList();
        }

        public IEnumerable<Vehicle> GetAvailableVehicles(DateTime dtFrom, DateTime dtTo, IEnumerable<string> categories = null,
            IEnumerable<string> manufacturers = null, IEnumerable<string> transmissions = null,
            IEnumerable<string> engines = null, IEnumerable<string> accessories = null, int minStatus = 2)
        {
            //var underMaintenance = Maintenances.Where()
            //Vehicles.
            var from = dtFrom.ToString("yyyy-MM-dd HH:mm:ss");
            var to = dtTo.ToString("yyyy-MM-dd HH:mm:ss");
            var cats = string.Join(',', categories ?? new List<string>());
            var mans = string.Join(',', manufacturers ?? new List<string>());
            var trans = string.Join(',', transmissions ?? new List<string>());
            var accs = string.Join(',', accessories ?? new List<string>());
            var engs = string.Join(',', engines ?? new List<string>());
            var query = $"exec sp_VehicleRequest '{from}', '{to}', {minStatus}, '{accs}', '{mans}', '{engs}', '{cats}', '{trans}'";
            // run query and select vehicle ids
            var vids = Vehicles.FromSqlRaw(query).ToList().Select(v => v.Id);
            // return actual vehicles objects
            return Vehicles.Where(v => vids.Contains(v.Id));
        }

        public string toJSON(Vehicle v)
        {
            return v.toJSON(this);
        }
    }

    /// <summary>
    /// Extension methods for <see cref="CarRentalContext"/> related objects
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts <see cref="Vehicle"/> object to JSON string
        /// </summary>
        /// <param name="v"><see cref="Vehicle"/> object to convert</param>
        /// <param name="ctx">Database context</param>
        /// <returns>JSON string <see cref="Vehicle"/> representation</returns>
        public static string toJSON(this Vehicle v, CarRentalContext ctx)
        {
            var accessories = ctx.VehicleAccessories
                .Where(a => a.VehicleId == v.Id)
                .Select(a => $@"
    {{
        'Accessory': '{a.Accessory.Name}',
        'Status': {a.Status}
    }}").ToList();

            return $@"{{
'Id': '{v.Id}',
'Name': '{v.Name}',
'Category': '{ctx.VehicleCategories.First(c => c.Id == v.Category).Name}',
'Manufacturer': '{ctx.Manufacturers.First(m => m.Id == v.Manufacturer).Name}',
'Transmission': '{ctx.TransmissionTypes.First(t => t.Id == v.Transmission).Name}',
'EngineType': '{ctx.EngineTypes.First(e => e.Id == v.Engine).Name}',
'Accessories': [
    {String.Join(",\r\n", accessories)}
]
}}";
        }

        public static VehicleCategory? GetCategory(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.VehicleCategories
                .Where(vc => vc.Id == v.Category)
                .FirstOrDefault();
        }

        public static EngineType? GetEngine(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.EngineTypes
                .Where(vc => vc.Id == v.Engine)
                .FirstOrDefault();
        }

        public static Manufacturer? GetManufacturer(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.Manufacturers
                .Where(vc => vc.Id == v.Manufacturer)
                .FirstOrDefault();
        }

        public static TransmissionType? GetTransmission(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.TransmissionTypes
                .Where(vc => vc.Id == v.Transmission)
                .FirstOrDefault();
        }

        public static IEnumerable<Accessory>? GetAcessories(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.VehicleAccessories
                .Where(va => va.VehicleId == v.Id)
                .Select(va => va.Accessory);
        }
    }
}
