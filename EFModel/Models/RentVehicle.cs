using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class RentVehicle
    {
        public int Vehicle { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }

        public virtual Vehicle VehicleNavigation { get; set; } = null!;
    }
}
