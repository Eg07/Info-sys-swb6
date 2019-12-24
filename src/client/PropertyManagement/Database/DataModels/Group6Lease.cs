using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6Lease
    {
        public int LeaseId { get; set; }
        public int TenantId { get; set; }
        public DateTime RentStart { get; set; }
        public DateTime? RentEnd { get; set; }
        public bool? PaymentStatus { get; set; }
        public double? Deposit { get; set; }

        public virtual Group6Tenant Tenant { get; set; }
    }
}
