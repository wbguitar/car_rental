using Xunit;
using EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EFModel.Models.Tests
{
    public class CarRentalContextTests: IDisposable
    {
        CarRentalContext ctx;
        public CarRentalContextTests()
        {
            ctx = new CarRentalContext();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        [Fact()]
        public void GetManufacturerTest()
        {
            Manufacturer? m = null;
            var doGet = new Func<string, Manufacturer?>(x => ctx.GetManufacturer(x));
            m = doGet("BMW");
            Assert.NotNull(m);
            Assert.IsType<Manufacturer>(m);
            Assert.IsType<int>(m.Id);
            Assert.IsType<string>(m.Name);

            m = doGet("Toyota");
            Assert.NotNull(m);
            Assert.IsType<Manufacturer>(m);
            Assert.IsType<int>(m.Id);
            Assert.IsType<string>(m.Name);

            Assert.Throws<NullReferenceException>(() => doGet("").Id);
        }

        [Fact()]
        public void GetEngineTypeTest()
        {
            EngineType? e = null;
            var doGet = new Func<string, EngineType?>(x => ctx.GetEngineType(x));
            e = doGet("Petrol"); 
            Assert.NotNull(e);
            Assert.IsType<EngineType>(e);
            Assert.IsType<int>(e.Id);
            Assert.IsType<string>(e.Name);

            e = doGet("Diesel");
            Assert.NotNull(e);
            Assert.IsType<EngineType>(e);
            Assert.IsType<int>(e.Id);
            Assert.IsType<string>(e.Name);

            Assert.Throws<NullReferenceException>(() => doGet("").Id);
        }

        [Fact()]
        public void GetAccessoryTest()
        {
            Accessory? a = null;
            var doGet = new Func<string, Accessory?>(x => ctx.GetAccessory(x));
            a = doGet("GPS");
            Assert.NotNull(a);
            Assert.IsType<Accessory>(a);
            Assert.IsType<int>(a.Id);
            Assert.IsType<string>(a.Name);

            a = doGet("Radio");
            Assert.NotNull(a);
            Assert.IsType<Accessory>(a);
            Assert.IsType<int>(a.Id);
            Assert.IsType<string>(a.Name);

            Assert.Throws<NullReferenceException>(() => doGet("").Id);
        }

        [Fact()]
        public void GetTransmissionTest()
        {
            TransmissionType? t = null;
            var doGet = new Func<string, TransmissionType?>(x => ctx.GetTransmission(x));
            t = doGet("Auto");
            Assert.NotNull(t);
            Assert.IsType<TransmissionType>(t);
            Assert.IsType<int>(t.Id);
            Assert.IsType<string>(t.Name);

            t = doGet("Manual");
            Assert.NotNull(t);
            Assert.IsType<TransmissionType>(t);
            Assert.IsType<int>(t.Id);
            Assert.IsType<string>(t.Name);

            Assert.Throws<NullReferenceException>(() => doGet("").Id);
        }

        [Fact()]
        public void GetVehicleCategoryTest()
        {
            VehicleCategory? c = null;
            var doGet = new Func<string, VehicleCategory?>(x => ctx.GetVehicleCategory(x));
            c = doGet("Bus");
            Assert.NotNull(c);
            Assert.IsType<VehicleCategory>(c);
            Assert.IsType<int>(c.Id);
            Assert.IsType<string>(c.Name);

            c = doGet("Car");
            Assert.NotNull(c);
            Assert.IsType<VehicleCategory>(c);
            Assert.IsType<int>(c.Id);
            Assert.IsType<string>(c.Name);

            Assert.Throws<NullReferenceException>(() => doGet("").Id);
        }

        [Fact()]
        public void GetVehicoleAccessoriesTest()
        {
            var v = ctx.Vehicles.First();
            var accessories = ctx.GetVehicoleAccessories(v);
            Assert.NotEmpty(accessories);
            Assert.NotEmpty(accessories
                .Select(a => a.Name)
                .Intersect(ctx.Accessories.Select(x => x.Name)));
            foreach (var a in accessories)
            {
                Assert.NotNull(a);
                Assert.NotNull(a.Id);
                Assert.IsType<int>(a.Id);
                Assert.IsType<string>(a.Name);
                Assert.NotNull(a.Name);

            }
        }

        [Fact()]
        public void BuildVehicleTest()
        {
            string name = "BMW Van", category = "Van", manufacturer = "BMW", transmission = "Auto", engine = "Diesel";
            var vehicle = ctx.BuildVehicle(name, category, manufacturer, transmission, engine, new[] { ("GPS", 4), ("Radio", 5) });
            Assert.NotNull(vehicle);
            Assert.Equal(vehicle.Name.Trim(), name);
            Assert.Equal(vehicle.CategoryNavigation.Name.Trim(), category);
            Assert.Equal(vehicle.ManufacturerNavigation.Name.Trim(), manufacturer);
            Assert.Equal(vehicle.TransmissionNavigation.Name.Trim(), transmission);
            Assert.Equal(vehicle.EngineNavigation.Name.Trim(), engine);

            vehicle = ctx.BuildVehicle(name, "FAIL", "FAIL", "FAIL", "FAIL", new[] { ("", 4)});
            Assert.Null(vehicle.CategoryNavigation);
            Assert.Null(vehicle.ManufacturerNavigation);
            Assert.Null(vehicle.TransmissionNavigation);
            Assert.Null(vehicle.EngineNavigation);
            Assert.Null(vehicle.VehicleAccessories.First().Accessory);
        }

        [Fact()]
        public void MaintenanceTest()
        {
            var v = ctx.Vehicles.First();
            ctx.SendToMaintenance(v.Name, DateTime.Now + TimeSpan.FromDays(7));
            // cannot send same vehicle in maintenance
            Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => ctx.SendToMaintenance(v.Name, DateTime.Now + TimeSpan.FromDays(2)));
            // vehicle should be found in maintenances
            Assert.NotEmpty(ctx.Maintenances.Where(m => m.Vehicle == v.Id));
            ctx.RemoveFromMaintenances(v.Name);
            // vehicle is no more under maintenances
            Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => ctx.RemoveFromMaintenances(v.Name));
        }

        [Fact()]
        public void GetAvailableVehiclesTest()
        {
            var from = DateTime.Now - 3*TimeSpan.FromDays(365);
            var to = DateTime.Now + 3*TimeSpan.FromDays(365);

            var categories = new[] { "Bus", "Van" };
            var accessories = new[] { "GPS" };
            var engines = new[] { "Petrol" };
            var vehicles = ctx.GetAvailableVehicles(from, to, categories: categories, accessories: accessories, engines: engines);
            Assert.NotEmpty(vehicles);

            var v = vehicles.FirstOrDefault();
            Assert.NotNull(v);
            Assert.Contains<string>(v.GetCategory(ctx).Name.Trim(), categories);
            Assert.Contains<string>(v.GetEngine(ctx).Name.Trim(), engines);
            Assert.Contains<string>(v.GetTransmission(ctx).Name.Trim(), ctx.TransmissionTypes.Select(t => t.Name));
            Assert.Contains<string>(v.GetManufacturer(ctx).Name.Trim(), ctx.Manufacturers.Select(m => m.Name));

            // check that selected vehicle has a non empty list of accessories
            Assert.NotEmpty(v.GetAcessories(ctx)
                .Select(a => a.Item2.Name)
                .Intersect(ctx.Accessories.Select(a => a.Name)));
        }

        [Fact]
        public void RentVehicleTest()
        {
            var c = ctx.Customers.FirstOrDefault();
            var v = ctx.Vehicles.FirstOrDefault();
            var f = DateTime.Now + TimeSpan.FromDays(5);
            var t = DateTime.Now + TimeSpan.FromDays(10);
            ctx.RentVehicle(c.Name, v.Name, f, t);
            Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => ctx.RentVehicle(c.Name, v.Name, f, t)); // already unrent
            ctx.UnrentVehicle(v.Name);

            ctx.SendToMaintenance(v.Name, DateTime.Now + TimeSpan.FromDays(4));
            f = DateTime.Now + TimeSpan.FromDays(5);
            t = DateTime.Now + TimeSpan.FromDays(10);
            var exc= Record.Exception(() => ctx.RentVehicle(c.Name, v.Name, f, t));
            Assert.Null(exc);// ok, out of the maintenance)
            ctx.UnrentVehicle(v.Name);

            f = DateTime.Now + TimeSpan.FromDays(2);
            t = DateTime.Now + TimeSpan.FromDays(8);
            Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => ctx.RentVehicle(c.Name, v.Name, f, t)); // ok, in maintenance
        }

        [Fact]
        void toJSONTest()
        {
            foreach (var v in ctx.Vehicles.ToArray())
            {
                var json = v.toJSON(ctx);
                var obj = JsonConvert.DeserializeObject(v.toJSON(ctx)); // test json parse
                Assert.NotNull(obj);
            }
        }
    }
}
