using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.DataContainers
{
    public class OperatingCostDisplayContainer : G3OperatingCosts
    {
        private DistributionKey _distributionKeyDisplay;

        public DistributionKey DistributionKeyDisplay
        {
            get => _distributionKeyDisplay;
            set
            {
                _distributionKeyDisplay = value;
                DistributionKey = (int) value;
            }
        }
        public string Tenant { get; set; }


        public OperatingCostDisplayContainer(G3OperatingCosts operatingCost, string tenant) : base(operatingCost)
        {
            DistributionKeyDisplay = (DistributionKey) DistributionKey;
            Tenant = tenant;
        }

        public void UpdateBaseInformation(InfosysContext context)
        {
            var wow = Tenant;
            var newTenant = context.G3Tenant.Include(i => i.G3Lease).FirstOrDefault(tenant => $"{tenant.FirstName} {tenant.LastName}" == Tenant);
            var lease = newTenant.G3Lease.First();
            UnitId = lease.UnitId;
            PropertyId = lease.Unit.PropertyId;
        }
    }
}
