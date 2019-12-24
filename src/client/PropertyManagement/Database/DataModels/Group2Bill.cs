using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2Bill
    {
        public Group2Bill()
        {
            Group2Oc = new HashSet<Group2Oc>();
        }

        public int BillId { get; set; }
        public double? Rent { get; set; }
        public double? CD { get; set; }
        public double? OcPu { get; set; }
        public int? UnitId { get; set; }
        public int? TId { get; set; }

        public virtual Group2Transactions T { get; set; }
        public virtual Group2Unit Unit { get; set; }
        public virtual ICollection<Group2Oc> Group2Oc { get; set; }
    }
}
