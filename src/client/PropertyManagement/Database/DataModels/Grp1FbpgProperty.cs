using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgProperty
    {
        public Grp1FbpgProperty()
        {
            Grp1FbpgOperatingCosts = new HashSet<Grp1FbpgOperatingCosts>();
            Grp1FbpgUnit = new HashSet<Grp1FbpgUnit>();
        }

        public Guid PkId { get; set; }
        public string Address { get; set; }
        public Guid PkId1 { get; set; }

        public virtual Grp1FbpgOwner PkId1Navigation { get; set; }
        public virtual ICollection<Grp1FbpgOperatingCosts> Grp1FbpgOperatingCosts { get; set; }
        public virtual ICollection<Grp1FbpgUnit> Grp1FbpgUnit { get; set; }
    }
}
