using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class G3Unit
    {
        public G3Unit()
        {
            G3Lease = new HashSet<G3Lease>();
            G3Service = new HashSet<G3Service>();
        }

        public int Id { get; set; }
        public double RoomsNr { get; set; }
        public double Area { get; set; }
        public int Floor { get; set; }
        public int PropertyId { get; set; }
        public int ResidentNr { get; set; }

        public virtual G3Property Property { get; set; }
        public virtual ICollection<G3Lease> G3Lease { get; set; }
        public virtual ICollection<G3Service> G3Service { get; set; }
    }
}
