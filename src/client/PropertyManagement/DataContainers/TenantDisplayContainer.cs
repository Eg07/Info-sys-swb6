using System.Linq;
using System.Text;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.DataContainers
{
    public class TenantDisplayContainer : G3Tenant
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => _isSelected = value;
        }

        public string Iban { get; set; }
        public G3Address Address { get; set; }

        public TenantDisplayContainer(G3Tenant tenant) : base(tenant)
        {
            Iban = GetIbansForTenant(tenant);
            Address = tenant.G3Lease.First().Unit.Property.Adress;
        }

        /// <summary>
        /// Constructs a string with all IBANs of a tenant
        /// </summary>
        /// <param name="tenant">tenant for which to find the associated IBANs</param>
        /// <returns>String containing all IBANs of the tenant</returns>
        private string GetIbansForTenant(G3Tenant tenant)
        {
            // ReSharper disable once IdentifierTypo
            var ibans = new StringBuilder();
            var accounts = tenant.G3BankAccount.ToList();

            ibans.Append($"{accounts.First().Iban}");
            accounts.Remove(accounts.First());
            accounts.ForEach(account => ibans.Append($",{account.Iban}"));

            return ibans.ToString();
        }
    }
}
