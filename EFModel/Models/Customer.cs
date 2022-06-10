using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class Customer
    {
        public Customer()
        {
            RentVehicles = new HashSet<RentVehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Telephone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<RentVehicle> RentVehicles { get; set; }
    }
}
