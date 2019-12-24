using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgTenant
    {
        public Grp1FbpgTenant()
        {
            Grp1FbpgBill = new HashSet<Grp1FbpgBill>();
            Grp1FbpgTransaction = new HashSet<Grp1FbpgTransaction>();
        }

        public DateTime MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public Guid FkUnit { get; set; }
        public Guid PkId { get; set; }

        public virtual Grp1FbpgUnit FkUnitNavigation { get; set; }
        public virtual Grp1FbpgPerson Pk { get; set; }
        public virtual ICollection<Grp1FbpgBill> Grp1FbpgBill { get; set; }
        public virtual ICollection<Grp1FbpgTransaction> Grp1FbpgTransaction { get; set; }
    }
}
