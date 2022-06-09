using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class VehicleAccessory
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int AccessoryId { get; set; }
        public int Status { get; set; }

        public virtual Accessory Accessory { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
