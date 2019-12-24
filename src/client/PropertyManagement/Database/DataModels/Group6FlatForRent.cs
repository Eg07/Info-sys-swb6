using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6FlatForRent
    {
        public Group6FlatForRent()
        {
            Group6Transactions = new HashSet<Group6Transactions>();
            Group6UtilityInvoice = new HashSet<Group6UtilityInvoice>();
        }

        public int FlatId { get; set; }
        public int NrOfRooms { get; set; }
        public int? TenantId { get; set; }
        public int Space { get; set; }
        public decimal Price { get; set; }
        public int? NrOfResidents { get; set; }
        public int PropertyId { get; set; }
        public string Floor { get; set; }

        public virtual Group6Property Property { get; set; }
        public virtual Group6Tenant Tenant { get; set; }
        public virtual ICollection<Group6Transactions> Group6Transactions { get; set; }
        public virtual ICollection<Group6UtilityInvoice> Group6UtilityInvoice { get; set; }
    }
}
