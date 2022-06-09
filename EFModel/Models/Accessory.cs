using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class Accessory
    {
        public Accessory()
        {
            VehicleAccessories = new HashSet<VehicleAccessory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<VehicleAccessory> VehicleAccessories { get; set; }
    }
}
