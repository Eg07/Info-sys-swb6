using System.Collections.Generic;

// ReSharper disable VirtualMemberCallInConstructor
namespace PropertyManagement.Database.DataModels
{
    public class G3Property
    {
        public G3Property()
        {
            G3OperatingCosts = new HashSet<G3OperatingCosts>();
            G3Unit = new HashSet<G3Unit>();
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int AdressId { get; set; }

        public virtual G3Address Adress { get; set; }
        public virtual G3Owner Owner { get; set; }
        public virtual ICollection<G3OperatingCosts> G3OperatingCosts { get; set; }
        public virtual ICollection<G3Unit> G3Unit { get; set; }
    }
}
