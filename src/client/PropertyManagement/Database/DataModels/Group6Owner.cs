using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6Owner
    {
        public Group6Owner()
        {
            Group6Property = new HashSet<Group6Property>();
        }

        public int OwnerId { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
        public DateTime Dob { get; set; }

        public virtual ICollection<Group6Property> Group6Property { get; set; }
    }
}
