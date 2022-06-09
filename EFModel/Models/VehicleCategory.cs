using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class VehicleCategory
    {
        public VehicleCategory()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool DumpLoad { get; set; }
        public bool TailLift { get; set; }
        public int ExtraDoors { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
