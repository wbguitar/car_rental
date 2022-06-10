using EFModel.DataMng;
using EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace EFModelTests
{
    public class RentalContextTests : IDisposable
    {
        public RentalContextTests()
        {
        }

        void DbCreation()
        {
            using (var ctx = new CarRentalContext())
            {
                // clear all
                ctx.Accessories.RemoveRange(ctx.Accessories);
                ctx.EngineTypes.RemoveRange(ctx.EngineTypes);
                ctx.Manufacturers.RemoveRange(ctx.Manufacturers);
                ctx.TransmissionTypes.RemoveRange(ctx.TransmissionTypes);
                ctx.VehicleCategories.RemoveRange(ctx.VehicleCategories);

                ctx.Maintenances.RemoveRange(ctx.Maintenances);
                ctx.VehicleAccessories.RemoveRange(ctx.VehicleAccessories);
                ctx.Vehicles.RemoveRange(ctx.Vehicles);
                ctx.SaveChanges();

                // Adding DB items
                ctx.Accessories.AddRange(new List<Accessory>() {
                    new Accessory() { Name = "GPS"},
                    new Accessory() { Name = "AirConditioned"},
                    new Accessory() { Name = "Radio"},
                });
                ctx.EngineTypes.AddRange(new List<EngineType>() {
                    new EngineType() { Name = "Petrol"},
                    new EngineType() { Name = "Diesel"},
                    new EngineType() { Name = "Electric"},
                });
                ctx.VehicleCategories.AddRange(new List<VehicleCategory>() {
                    new VehicleCategory() { Name = "Car"},
                    new VehicleCategory() { Name = "Van"},
                    new VehicleCategory() { Name = "Bus", ExtraDoors = 3},
                    new VehicleCategory() { Name = "Truck", DumpLoad = true, TailLift = true},
                });
                
                // Adding manufacturers
                new List<string>{
                    "Toyota",
                    "Volkswagen",
                    "BMW",
                    "Mercedes",
                    "Ford",
                    "Fiat",
                }.ForEach(m => ctx.Manufacturers.Add(new Manufacturer() { Name = m }));


                // Adding transmissions types
                ctx.TransmissionTypes.AddRange(new List<TransmissionType>() {
                    new TransmissionType() { Name = "Auto"},
                    new TransmissionType() { Name = "Manual"},
                });
                ctx.SaveChanges();


                // Adding vehicles
                var vehicle = ctx.BuildVehicle("BMW Van", "Van", "BMW", "Auto", "Diesel", new[] { ("GPS", 4), ("Radio", 5) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                vehicle = ctx.BuildVehicle("Volkswagen Van", "Van", "Volkswagen", "Manual", "Petrol", new[] { ("GPS", 4), ("AirConditioned", 3) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                vehicle = ctx.BuildVehicle("Ford Van", "Van", "Ford", "Manual", "Petrol", new[] { ("Radio", 5), ("AirConditioned", 4) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();



                vehicle = ctx.BuildVehicle("Mercedes Bus", "Bus", "Mercedes", "Manual", "Petrol", new[] { ("GPS", 5), ("AirConditioned", 2) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                vehicle = ctx.BuildVehicle("Fiat Bus", "Bus", "Mercedes", "Auto", "Diesel", new[] { ("Radio", 4), ("AirConditioned", 3) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();



                vehicle = ctx.BuildVehicle("Mercedes Truck", "Truck", "Mercedes", "Manual", "Petrol", new[] { ("GPS", 5), ("AirConditioned", 2) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                vehicle = ctx.BuildVehicle("BMW Truck", "Truck", "BMW", "Auto", "Diesel", new[] { ("GPS", 3), ("Radio", 4), ("AirConditioned", 3) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();


                vehicle = ctx.BuildVehicle("Toyota Car", "Car", "Toyota", "Auto", "Petrol", new[] { ("GPS", 1), ("AirConditioned", 4) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                vehicle = ctx.BuildVehicle("Ford Car", "Car", "Ford", "Manual", "Diesel", new[] { ("GPS", 5), ("AirConditioned", 5), ("Radio", 4) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                vehicle = ctx.BuildVehicle("Mercedes Car", "Car", "Mercedes", "Auto", "Diesel", new[] { ("GPS", 5), ("AirConditioned", 5), ("Radio", 5) });
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => ctx.RemoveFromMaintenances("Mercedes Car"));
                ctx.SendToMaintenance("Mercedes Car", DateTime.Now + TimeSpan.FromDays(7));
                Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => ctx.SendToMaintenance("Mercedes Car", DateTime.Now + TimeSpan.FromDays(7)));
                ctx.RemoveFromMaintenances("Mercedes Car");
            }
        }

        [Fact]
        public void DbContextTest()
        {
            DbCreation();
            using (var ctx = new CarRentalContext())
            {
                Assert.NotEmpty(ctx.Vehicles);
            }
        }
        public void Dispose()
        {
        }

    }
}
