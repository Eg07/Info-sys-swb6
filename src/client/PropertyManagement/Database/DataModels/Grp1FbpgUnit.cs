using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgUnit
    {
        public Grp1FbpgUnit()
        {
            Grp1FbpgBill = new HashSet<Grp1FbpgBill>();
            Grp1FbpgTenant = new HashSet<Grp1FbpgTenant>();
        }

        public Guid PkId { get; set; }
        public string Floor { get; set; }
        public byte Rooms { get; set; }
        public short Size { get; set; }
        public short Rent { get; set; }
        public Guid FkProperty { get; set; }

        public virtual Grp1FbpgProperty FkPropertyNavigation { get; set; }
        public virtual ICollection<Grp1FbpgBill> Grp1FbpgBill { get; set; }
        public virtual ICollection<Grp1FbpgTenant> Grp1FbpgTenant { get; set; }
    }
}
