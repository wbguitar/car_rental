using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel.Vehicles
{
    public enum Transmission
    {
        Manual,
        Auto
    }

    public enum Engines
    {
        Petrol,
        Diesel,
        Electric
    }

    public enum Manufacturers
    {
        Toyota,
        Volkswagen,
        BMW,
        Mercedes,
        Ford,
        Fiat,
    }

    [Flags]
    public enum Accessorie
    {
        None = 0,
        AirConditioned = 1,
        Radio = 2,
        GPS = 4,
    }

    public abstract class BaseVehicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Manufacturers Manufacturer { get; set; }
        public Transmission Transmission { get; set; }
        public Engines Engine { get; set; }
        public Accessorie Accessories { get; set; } = Accessorie.AirConditioned | Accessorie.Radio;
    }
}
