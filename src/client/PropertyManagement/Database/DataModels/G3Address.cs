using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public class G3Address
    {
        public G3Address()
        {
            G3Owner = new HashSet<G3Owner>();
            G3Property = new HashSet<G3Property>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int HouseNr { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }

        public virtual ICollection<G3Owner> G3Owner { get; set; }
        public virtual ICollection<G3Property> G3Property { get; set; }
    }
}
