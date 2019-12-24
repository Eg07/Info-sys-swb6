using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2Propertys
    {
        public Group2Propertys()
        {
            Group2PropertyOwner = new HashSet<Group2PropertyOwner>();
            Group2Unit = new HashSet<Group2Unit>();
        }

        public int PropertyId { get; set; }
        public string Address { get; set; }
        public int NumberOfUnits { get; set; }

        public virtual ICollection<Group2PropertyOwner> Group2PropertyOwner { get; set; }
        public virtual ICollection<Group2Unit> Group2Unit { get; set; }
    }
}
