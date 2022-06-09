using System;
using System.Collections.Generic;

namespace EFModel.Models
{
    public partial class TransmissionType
    {
        public TransmissionType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
