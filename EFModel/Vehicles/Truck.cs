using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel.Vehicles
{
    public class Truck: BaseVehicle
    {
        public bool? DumpLoad { get; set; }
        public bool? TailLift { get; set; }
    }
}
