using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.DataContainers
{
    public class OperatingCostDisplayContainer : G3OperatingCosts
    {
        public DistributionKey DistributionKeyDisplay { get; set; }
        public string Tenant { get; set; }

        public OperatingCostDisplayContainer(G3OperatingCosts operatingCost, string tenant) : base(operatingCost)
        {
            DistributionKeyDisplay = (DistributionKey) DistributionKey;
            Tenant = tenant;
        }
    }
}
