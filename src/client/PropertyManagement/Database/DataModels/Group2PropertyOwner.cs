using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group2PropertyOwner
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public int PhoneNo { get; set; }
        public int PropertyId { get; set; }

        public virtual Group2Propertys Property { get; set; }
    }
}
