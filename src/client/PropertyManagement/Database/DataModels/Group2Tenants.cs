using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2Tenants
    {
        public Group2Tenants()
        {
            Group2Transactions = new HashSet<Group2Transactions>();
        }

        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
        public int PhoneNo { get; set; }
        public int UnitId { get; set; }

        public virtual Group2Unit Unit { get; set; }
        public virtual ICollection<Group2Transactions> Group2Transactions { get; set; }
    }
}
