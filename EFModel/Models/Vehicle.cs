using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            VehicleAccessories = new HashSet<VehicleAccessory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Manufacturer { get; set; }
        public int Engine { get; set; }
        public int Transmission { get; set; }
        public int Category { get; set; }

        public virtual VehicleCategory CategoryNavigation { get; set; } = null!;
        public virtual EngineType EngineNavigation { get; set; } = null!;
        public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;
        public virtual TransmissionType TransmissionNavigation { get; set; } = null!;
        public virtual Maintenance Maintenance { get; set; } = null!;
        public virtual RentVehicle RentVehicle { get; set; } = null!;
        public virtual ICollection<VehicleAccessory> VehicleAccessories { get; set; }
    }
}
