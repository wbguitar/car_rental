using EFModel.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarRentalService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(ILogger<VehiclesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Manufacturers")]
        public IEnumerable<string> GetManufacturers()
        {
            using (var ctx = new CarRentalContext())
            {
                return ctx.Manufacturers
                    .Select(m => m.Name)
                    .ToList();
            }
        }

        [HttpGet("Engines")]
        public IEnumerable<string> GetEngines()
        {
            using (var ctx = new CarRentalContext())
            {
                return ctx.EngineTypes
                    .Select(e => e.Name)
                    .ToList();
            }
        }

        [HttpGet("Transmissions")]
        public IEnumerable<string> GetTransmissions()
        {
            using (var ctx = new CarRentalContext())
            {
                return ctx.TransmissionTypes
                    .Select(t => t.Name)
                    .ToList();
            }
        }

        [HttpGet("Accessories")]
        public IEnumerable<string> GetAccessories()
        {
            using (var ctx = new CarRentalContext())
            {
                return ctx.Accessories
                    .Select(a => a.Name)
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<dynamic> GetAllVehicles()
        {
            using (var ctx = new CarRentalContext())
            {
                foreach (var v in ctx.Vehicles.ToList())
                {
                    yield return v.CreateVehicle(ctx);
                }
            }
        }

        [HttpGet("Id/{id}")]
        public dynamic GetVehicleById(int id)
        {
            using (var ctx = new CarRentalContext())
            {
                var v = ctx.Vehicles.Where(v => v.Id == id).FirstOrDefault();
                if (v == null)
                    return new { errorMessage = $"Vehicle with id={id} doesn't exists" };
                var obj = v.CreateVehicle(ctx);
                return obj;

            }
        }

        [HttpGet("Name/{name}")]
        public dynamic GetVehicleByName(string name)
        {
            using (var ctx = new CarRentalContext())
            {
                var v = ctx.Vehicles.Where(v => v.Name == name).FirstOrDefault();
                if (v == null)
                    return new { errorMessage = $"Vehicle with name={name} doesn't exists" };

                return v.CreateVehicle(ctx);

            }
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly ILogger<RentController> _logger;

        public RentController(ILogger<RentController> logger)
        {
            _logger = logger;
        }

        //[HttpGet("Available/{from}/{to}/{manufacturers}/{transmissions}/{engines}/{categories}/{accessories}/")]
        [HttpGet]
        public IEnumerable<dynamic> GetAvailables(DateTime from, DateTime to, string? manufacturers = "", string? transmissions = "",
            string? engines = "", string? categories = "", string? accessories = "", bool? hasExtraDoors = null, bool? hasTailLift = null)
        {
            using (var ctx = new CarRentalContext())
            {
                var mans = string.IsNullOrEmpty(manufacturers) ? null : manufacturers.Split(' ');
                var trans = string.IsNullOrEmpty(transmissions) ? null : transmissions.Split(' ');
                var engs = string.IsNullOrEmpty(engines) ? null : engines.Split(' ');
                var cats = string.IsNullOrEmpty(categories) ? null : categories.Split(' ');
                var accs = string.IsNullOrEmpty(accessories) ? null : accessories.Split(' ');
                var vehicles = ctx.GetAvailableVehicles(from, to, minAccessoryStatus: 2, manufacturers: mans, transmissions: trans,
                    categories: cats, accessories: accs, hasTailLift: hasTailLift, hasExtraDoors: hasExtraDoors).ToList();
                foreach (var v in vehicles)
                {
                    yield return v.CreateVehicle(ctx);
                }
            }
        }

        [HttpGet("Available/Categories/{categories}/{from}/{to}")]
        public IEnumerable<dynamic> GetAvailablesByCategories(DateTime from, DateTime to, string categories = "")
        {
            using (var ctx = new CarRentalContext())
            {
                var cats = categories.Split(' ');
                var vehicles = ctx.GetAvailableVehicles(from, to, minAccessoryStatus: 2, categories: cats).ToList();
                foreach (var v in vehicles)
                {
                    yield return v.CreateVehicle(ctx);
                }
            }
        }

        [HttpGet("Available/Manufacturers/{manufacturers}/{from}/{to}")]
        public IEnumerable<dynamic> GetAvailablesByManufacturer(DateTime from, DateTime to, string manufacturers)
        {
            using (var ctx = new CarRentalContext())
            {
                var mans = manufacturers.Split(' ');
                var vehicles = ctx.GetAvailableVehicles(from, to, manufacturers: mans).ToList();
                foreach (var v in vehicles)
                {
                    yield return v.CreateVehicle(ctx);
                }
            }
        }

        [HttpPost("Add/{vehicle}/{customer}/{from}/{to}")]
        public dynamic PostRent(string vehicle, string customer, DateTime from, DateTime to)
        {
            using (var ctx = new CarRentalContext())
            {
                try
                {
                    var c = ctx.Customers.FirstOrDefault(c => c.Name == customer);
                    if (c == null)
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Customer {customer} not found");

                    var vehicles = ctx.GetAvailableVehicles(from, to).ToList();
                    var v = vehicles.FirstOrDefault(v => v.Name == vehicle);
                    if (v == null)
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Vehicle {vehicle} not found");

                    ctx.RentVehicle(customer, vehicle, from, to);
                    return StatusCode(StatusCodes.Status201Created);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpDelete("Remove/{vehicle}")]
        public dynamic DeleteRent(string vehicle)
        {
            using (var ctx = new CarRentalContext())
            {
                try
                {
                    ctx.UnrentVehicle(vehicle);
                    return StatusCode(StatusCodes.Status202Accepted);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(ILogger<MaintenanceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<dynamic> GetUnderMaintenance()
        {
            using (var ctx = new CarRentalContext())
            {
                return ctx.Maintenances
                    .Select(m => ctx.Vehicles.First(v => v.Id == m.Vehicle))
                    .ToList();
            }
        }

        [HttpPost]
        public dynamic PostUnderMaintenance(string vehicleName, DateTime untill)
        {
            try
            {
                using (var ctx = new CarRentalContext())
                {
                    ctx.SendToMaintenance(vehicleName, untill);
                    var v = ctx.Vehicles.First(v => v.Name == vehicleName);

                    return new
                    {
                        status = StatusCode(StatusCodes.Status201Created),
                        underMaintenance = v.CreateVehicle(ctx)
                    };
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        public dynamic DeleteMaintenance(string vehicleName)
        {
            try
            {
                using (var ctx = new CarRentalContext())
                {
                    ctx.RemoveFromMaintenances(vehicleName);
                    var v = ctx.Vehicles.FirstOrDefault(v => v.Name == vehicleName);
                    return new
                    {
                        status = StatusCode(StatusCodes.Status202Accepted),
                        underMaintenance = v.CreateVehicle(ctx)
                    };
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<dynamic> GetCustomers()
        {
            using (var ctx = new CarRentalContext())
            {
                return ctx.Customers.Select(c => new
                {
                    name = c.Name,
                    email = c.Email,
                    telephone = c.Telephone
                }).ToList();
            }
        }

        [HttpPost]
        public dynamic PostNewCustomer(string customer, string email = "", string telephone = "")
        {
            try
            {
                using (var ctx = new CarRentalContext())
                {
                    ctx.Customers.Add(new Customer()
                    {
                        Name = customer,
                        Email = email,
                        Telephone = telephone,
                    });
                    ctx.SaveChanges();
                    return new
                    {
                        status = StatusCode(StatusCodes.Status201Created),
                        customer = ctx.Customers.FirstOrDefault(c => c.Name == customer)
                    };
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        public dynamic DeleteCustomer(string customer)
        {
            try
            {
                using (var ctx = new CarRentalContext())
                {
                    var c = ctx.Customers.FirstOrDefault(c => c.Name == customer);
                    if (c == null)
                    {
                        return StatusCode(StatusCodes.Status204NoContent, $"Customer {customer} not found");
                    }

                    ctx.Customers.Remove(c);
                    ctx.SaveChanges();
                    return new
                    {
                        status = StatusCode(StatusCodes.Status202Accepted),
                        customerDeleted = customer
                    };
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
    public static class Extensions
    {
        public static dynamic CreateVehicle(this Vehicle v, CarRentalContext ctx)
        {
            return new
            {
                id = v.Id,
                name = v.Name,
                manufacturer = v.GetManufacturer(ctx).Name.Trim(),
                category = v.GetCategory(ctx).Name.Trim(),
                transmission = v.GetTransmission(ctx).Name.Trim(),
                engine = v.GetEngine(ctx).Name.Trim(),
                accessories = v.GetAcessories(ctx)
                            .Select(a =>
                            {
                                return new
                                {
                                    name = a.Item2.Name.Trim(),
                                    status = a.Item1.Status
                                };
                            }).ToList()
            };
        }
    }
}
