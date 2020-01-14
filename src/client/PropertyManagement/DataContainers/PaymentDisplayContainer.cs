using System.Linq;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.DataContainers
{
    public class PaymentDisplayContainer : G3Payments
    {
        public string Tenant { get; set; }

        public PaymentDisplayContainer(G3Payments payment, string tenant) : base(payment)
        {
            Tenant = tenant;
        }

        public void UpdateBaseInformation(G3Lease lease)
        {
            Lease = lease;
            Iban = lease.Tenant.G3BankAccount.First().Iban;
        }
    }
}
