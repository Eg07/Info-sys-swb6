using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6Transactions
    {
        public DateTime ValutaDate { get; set; }
        public string Usage { get; set; }
        public string Payer { get; set; }
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public int FlatId { get; set; }
        public int? TenantId { get; set; }

        public virtual Group6FlatForRent Flat { get; set; }
        public virtual Group6Tenant Tenant { get; set; }
    }
}
