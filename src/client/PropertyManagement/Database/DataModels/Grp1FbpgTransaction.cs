using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgTransaction
    {
        public Guid PkReferenceNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ValueDate { get; set; }
        public string Recipient { get; set; }
        public string Purpose { get; set; }
        public string PostingText { get; set; }
        public Guid FkBill { get; set; }
        public decimal Amount { get; set; }
        public Guid FkOperatingCosts { get; set; }
        public Guid PkId { get; set; }

        public virtual Grp1FbpgBill FkBillNavigation { get; set; }
        public virtual Grp1FbpgOperatingCosts FkOperatingCostsNavigation { get; set; }
        public virtual Grp1FbpgTenant Pk { get; set; }
    }
}
