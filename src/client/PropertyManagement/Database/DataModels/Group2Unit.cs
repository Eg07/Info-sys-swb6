using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2Unit
    {
        public Group2Unit()
        {
            Group2Bill = new HashSet<Group2Bill>();
            Group2Tenants = new HashSet<Group2Tenants>();
        }

        public int UnitId { get; set; }
        public int NumberOfPeople { get; set; }
        public int Rooms { get; set; }
        public int Size { get; set; }
        public int PropertyId { get; set; }

        public virtual Group2Propertys Property { get; set; }
        public virtual ICollection<Group2Bill> Group2Bill { get; set; }
        public virtual ICollection<Group2Tenants> Group2Tenants { get; set; }
    }
}
