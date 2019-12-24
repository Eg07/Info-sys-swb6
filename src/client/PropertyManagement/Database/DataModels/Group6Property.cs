using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6Property
    {
        public Group6Property()
        {
            Group6FlatForRent = new HashSet<Group6FlatForRent>();
            Group6UtilityInvoice = new HashSet<Group6UtilityInvoice>();
        }

        public int PropertyId { get; set; }
        public int OwnerId { get; set; }

        public virtual Group6Owner Owner { get; set; }
        public virtual ICollection<Group6FlatForRent> Group6FlatForRent { get; set; }
        public virtual ICollection<Group6UtilityInvoice> Group6UtilityInvoice { get; set; }
    }
}
