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
        /// <summary>
        /// Search for a <see cref="Manufacturer"/> from the given manufacturer name
        /// </summary>
        /// <param name="mname">Name of the manufacturer to find</param>
        /// <returns>The manufacturer object or null if not found</returns>
        public Manufacturer? GetManufacturer(string mname)
        {
            return Manufacturers.FirstOrDefault(m => m.Name == mname);
        }
        /// <summary>
        /// Search for a <see cref="EngineType"/> from the given engine name
        /// </summary>
        /// <param name="etype">Name of the engine to find</param>
        /// <returns>The manufacturer object or null if not found</returns>
        public EngineType? GetEngineType(string etype)
        {
            return EngineTypes.FirstOrDefault(e => e.Name == etype);
        }
        /// <summary>
        /// Search for a <see cref="Accessory"/> from the given accessory name
        /// </summary>
        /// <param name="aname">Name of the accessory to find</param>
        /// <returns>The accessory object or null if not found</returns>
        public Accessory? GetAccessory(string aname)
        {
            return Accessories.FirstOrDefault(m => m.Name == aname);
        }
        /// <summary>
        /// Search for a <see cref="TransmissionType"/> from the given transmission name
        /// </summary>
        /// <param name="tname">Name of the transmission to find</param>
        /// <returns>The transmission object or null if not found</returns>
        public TransmissionType? GetTransmission(string tname)
        {
            return TransmissionTypes.FirstOrDefault(t => t.Name == tname);
        }
        /// <summary>
        /// Search for a <see cref="VehicleCategory"/> from the given category name
        /// </summary>
        /// <param name="cname">Name of the category to find</param>
        /// <returns>The category object or null if not found</returns>
        public VehicleCategory? GetVehicleCategory(string cname)
        {
            return VehicleCategories.FirstOrDefault(m => m.Name == cname);
        }
        /// <summary>
        /// Gets a list of the <see cref="Accessory"/> from the given vehicle object
        /// </summary>
        /// <param name="v">Vehicle object for which to get accessories</param>
        /// <returns></returns>
        public IEnumerable<Accessory> GetVehicoleAccessories(Vehicle v)
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
            //exec sp_VehicleRequest @dtfrom, @dtto, @minStatus, @accessories, @manufacturers, @engines, @categories, @transmissions
            var query = $"exec sp_VehicleRequest '{from}', '{to}', {minStatus}, '{accs}', '{mans}', '{engs}', '{cats}', '{trans}'";
            var vids = Vehicles.FromSqlRaw(query).ToList().Select(v => v.Id);
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
        /// Gets the <see cref="VehicleCategory"/> for a given vehicle
        /// </summary>
        /// <param name="v">Vehicle object for which to get the category</param>
        /// <param name="ctx">Database context</param>
        /// <returns>The category of the given vehicle <paramref name="v"/></returns>
        public static VehicleCategory? GetCategory(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.VehicleCategories
                .Where(vc => vc.Id == v.Category)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="EngineType"/> for a given vehicle
        /// </summary>
        /// <param name="v">Vehicle object for which to get the category</param>
        /// <param name="ctx">Database context</param>
        /// <returns>The engine of the given vehicle <paramref name="v"/></returns>
        public static EngineType? GetEngine(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.EngineTypes
                .Where(vc => vc.Id == v.Engine)
                .FirstOrDefault();
        }
        /// <summary>
        /// Gets the <see cref="Manufacturer"/> for a given vehicle
        /// </summary>
        /// <param name="v">Vehicle object for which to get the category</param>
        /// <param name="ctx">Database context</param>
        /// <returns>The manufacturer for the given vehicle <paramref name="v"/></returns>
        public static Manufacturer? GetManufacturer(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.Manufacturers
                .Where(vc => vc.Id == v.Manufacturer)
                .FirstOrDefault();
        }
        /// <summary>
        /// Gets the <see cref="TransmissionType"/> for a given vehicle
        /// </summary>
        /// <param name="v">Vehicle object for which to get the category</param>
        /// <param name="ctx">Database context</param>
        /// <returns>The transmission of the given vehicle <paramref name="v"/></returns>
        public static TransmissionType? GetTransmission(this Vehicle v, CarRentalContext ctx)
        {
            return ctx.TransmissionTypes
                .Where(vc => vc.Id == v.Transmission)
                .FirstOrDefault();
        }
        /// <summary>
        /// Gets the <see cref="VehicleAccessory"/> and related status for a given vehicle
        /// </summary>
        /// <param name="v">Vehicle object for which to get the category</param>
        /// <param name="ctx">Database context</param>
        /// <returns>A <see cref="Tuple"/> with the <see cref="VehicleAccessory"/> and <see cref="Accessory"/> for the given vehicle <paramref name="v"/></returns>
        public static IEnumerable<(VehicleAccessory, Accessory)>? GetAcessories(this Vehicle v, CarRentalContext ctx)
        {
            var vas = ctx.VehicleAccessories
                .Where(va => va.VehicleId == v.Id)
                .ToList();
            foreach (var va in vas)
            {
                yield return new(va, ctx.Accessories.First((x) => x.Id == va.AccessoryId));
            }
        }

        /// <summary>
        /// Converts <see cref="Vehicle"/> object to JSON string
        /// </summary>
        /// <param name="v"><see cref="Vehicle"/> object to convert</param>
        /// <param name="ctx">Database context</param>
        /// <returns>JSON string representation of the vehicle <paramref name="v"/></returns>
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
    }
}
