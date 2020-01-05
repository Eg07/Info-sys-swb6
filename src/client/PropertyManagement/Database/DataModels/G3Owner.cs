using System.Collections.Generic;

// ReSharper disable All
namespace PropertyManagement.Database.DataModels
{
    public class G3Owner
    {
        public G3Owner()
        {
            G3Property = new HashSet<G3Property>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Adressid { get; set; }

        public virtual G3Address Adress { get; set; }
        public virtual ICollection<G3Property> G3Property { get; set; }
    }
}
