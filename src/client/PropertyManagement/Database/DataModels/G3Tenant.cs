using System.Collections.Generic;

// ReSharper disable VirtualMemberCallInConstructor
namespace PropertyManagement.Database.DataModels
{
    public class G3Tenant
    {
        public G3Tenant()
        {
            G3BankAccount = new HashSet<G3BankAccount>();
            G3Lease = new HashSet<G3Lease>();
        }

        public G3Tenant(G3Tenant tenant)
        {
            Id = tenant.Id;
            FirstName = tenant.FirstName;
            LastName = tenant.LastName;
            G3BankAccount = tenant.G3BankAccount;
            G3Lease = tenant.G3Lease;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<G3BankAccount> G3BankAccount { get; set; }
        public virtual ICollection<G3Lease> G3Lease { get; set; }
    }
}
