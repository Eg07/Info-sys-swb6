using System;
using System.Linq;
using System.Text;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.DataContainers
{
    public class UnitDisplayContainer : G3Unit
    {
        public double MonthlyRent { get; set; }
        public string TenantNames { get; set; }

        public UnitDisplayContainer(G3Unit unit) : base(unit)
        {
            MonthlyRent = unit.G3Lease.Sum(lease => lease.Cost);
            TenantNames = GetTenantsForUnit(unit);
        }

        /// <summary>
        /// Constructs a string with all tenants of a unit
        /// </summary>
        /// <param name="unit">Unit for which to find the tenants</param>
        /// <returns>String containing all names of the tenants</returns>
        private string GetTenantsForUnit(G3Unit unit)
        {
            var tenants = new StringBuilder();
            var leases = unit.G3Lease.ToList();
            
            tenants.Append($"{leases.First().Tenant.FirstName} {leases.First().Tenant.LastName}");
            leases.Remove(leases.First());
            leases.ForEach(lease => tenants.Append($",{lease.Tenant.FirstName} {lease.Tenant.LastName}"));
            
            return tenants.ToString();
        }
    }
}
