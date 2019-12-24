using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6Tenant
    {
        public Group6Tenant()
        {
            Group6FlatForRent = new HashSet<Group6FlatForRent>();
            Group6Lease = new HashSet<Group6Lease>();
            Group6Transactions = new HashSet<Group6Transactions>();
        }

        public int TenantId { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
        public DateTime Dob { get; set; }

        public virtual ICollection<Group6FlatForRent> Group6FlatForRent { get; set; }
        public virtual ICollection<Group6Lease> Group6Lease { get; set; }
        public virtual ICollection<Group6Transactions> Group6Transactions { get; set; }
    }
}
