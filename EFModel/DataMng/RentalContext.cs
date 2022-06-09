using EFModel.Vehicles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel.DataMng
{
    public class RentalContext : DbContext
    {
        public DbSet<BaseVehicle> Vehicles { get; set; }
    }
}
