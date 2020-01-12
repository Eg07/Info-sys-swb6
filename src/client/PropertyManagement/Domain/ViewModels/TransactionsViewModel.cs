using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        public ObservableCollection<OperatingCostDisplayContainer> OperatingCosts { get; set; } = new ObservableCollection<OperatingCostDisplayContainer>();
        public ObservableCollection<PaymentDisplayContainer> Payments { get; set; } = new ObservableCollection<PaymentDisplayContainer>();
        public static IEnumerable<string> Tenants { get; set; } = InfoSysDbContext.G3Tenant.Include(i => i.G3Lease).ThenInclude(i => i.Tenant).Select(tenant => $"{tenant.FirstName} {tenant.LastName}").ToList();
        public static IEnumerable<DistributionKey> DistributionKeys { get; set; }

        public TransactionsViewModel()
        {
            InfoSysDbContext.G3OperatingCosts.ToList().ForEach(operatingCost => OperatingCosts.Add(new OperatingCostDisplayContainer(operatingCost, $"")));
            InfoSysDbContext.G3Payments.ToList().ForEach(payment => Payments.Add(new PaymentDisplayContainer(payment, $"{payment.Lease?.Tenant.FirstName} {payment.Lease?.Tenant.LastName}")));

            DistributionKeys = new List<DistributionKey>()
            {
                DistributionKey.DistributeByArea,
                DistributionKey.DistributeByResidents
            };
        }
    }
}
