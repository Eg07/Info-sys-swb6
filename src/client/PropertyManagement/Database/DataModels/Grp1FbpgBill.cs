using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgBill
    {
        public Grp1FbpgBill()
        {
            Grp1FbpgTransaction = new HashSet<Grp1FbpgTransaction>();
        }

        public Guid PkInvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal PayedAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoiceItem { get; set; }
        public Guid FkUnit { get; set; }
        public Guid PkId { get; set; }

        public virtual Grp1FbpgUnit FkUnitNavigation { get; set; }
        public virtual Grp1FbpgTenant Pk { get; set; }
        public virtual ICollection<Grp1FbpgTransaction> Grp1FbpgTransaction { get; set; }
    }
}
