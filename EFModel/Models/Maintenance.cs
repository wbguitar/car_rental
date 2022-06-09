using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class Maintenance
    {
        public int Vehicle { get; set; }
        public DateTime MaintenanceEnd { get; set; }

        public virtual Vehicle VehicleNavigation { get; set; } = null!;
    }
}
