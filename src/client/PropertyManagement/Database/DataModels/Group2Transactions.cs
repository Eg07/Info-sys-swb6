using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2Transactions
    {
        public Group2Transactions()
        {
            Group2Bill = new HashSet<Group2Bill>();
            Group2Oc = new HashSet<Group2Oc>();
        }

        public int TId { get; set; }
        public string BuchungsTag { get; set; }
        public string ValutaDatum { get; set; }
        public string BuchungsText { get; set; }
        public string BegunstigsterZahlungspflichtiger { get; set; }
        public double? Betrag { get; set; }
        public int TenantId { get; set; }

        public virtual Group2Tenants Tenant { get; set; }
        public virtual ICollection<Group2Bill> Group2Bill { get; set; }
        public virtual ICollection<Group2Oc> Group2Oc { get; set; }
    }
}
