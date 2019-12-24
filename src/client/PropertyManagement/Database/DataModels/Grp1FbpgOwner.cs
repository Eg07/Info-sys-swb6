using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgOwner
    {
        public Grp1FbpgOwner()
        {
            Grp1FbpgProperty = new HashSet<Grp1FbpgProperty>();
        }

        public string Address { get; set; }
        public Guid PkId { get; set; }

        public virtual Grp1FbpgPerson Pk { get; set; }
        public virtual ICollection<Grp1FbpgProperty> Grp1FbpgProperty { get; set; }
    }
}
