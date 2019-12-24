using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgOperatingCosts
    {
        public Grp1FbpgOperatingCosts()
        {
            Grp1FbpgTransaction = new HashSet<Grp1FbpgTransaction>();
        }

        public Guid PkId { get; set; }
        public string InvoiceItem { get; set; }
        public decimal Amount { get; set; }
        public string AllocationType { get; set; }
        public Guid FkProperty { get; set; }

        public virtual Grp1FbpgProperty FkPropertyNavigation { get; set; }
        public virtual ICollection<Grp1FbpgTransaction> Grp1FbpgTransaction { get; set; }
    }
}
