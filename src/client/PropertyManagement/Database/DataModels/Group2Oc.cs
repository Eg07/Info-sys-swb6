using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2Oc
    {
        public int OcId { get; set; }
        public int? Oc { get; set; }
        public string OcType { get; set; }
        public int? BillId { get; set; }
        public int? TId { get; set; }

        public virtual Group2Bill Bill { get; set; }
        public virtual Group2Transactions T { get; set; }
    }
}
