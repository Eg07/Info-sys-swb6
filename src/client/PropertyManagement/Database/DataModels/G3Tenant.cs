using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class G3Tenant
    {
        public G3Tenant()
        {
            G3BankAccount = new HashSet<G3BankAccount>();
            G3Lease = new HashSet<G3Lease>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<G3BankAccount> G3BankAccount { get; set; }
        public virtual ICollection<G3Lease> G3Lease { get; set; }
    }
}
