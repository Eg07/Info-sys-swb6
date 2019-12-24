using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class G3Property
    {
        public G3Property()
        {
            G3Service = new HashSet<G3Service>();
            G3Unit = new HashSet<G3Unit>();
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int AdressId { get; set; }

        public virtual G3Address Adress { get; set; }
        public virtual G3Owner Owner { get; set; }
        public virtual ICollection<G3Service> G3Service { get; set; }
        public virtual ICollection<G3Unit> G3Unit { get; set; }
    }
}
