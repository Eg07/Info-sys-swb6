using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Grp1FbpgPerson
    {
        public Guid PkId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }

        public virtual Grp1FbpgOwner Grp1FbpgOwner { get; set; }
        public virtual Grp1FbpgTenant Grp1FbpgTenant { get; set; }
    }
}
