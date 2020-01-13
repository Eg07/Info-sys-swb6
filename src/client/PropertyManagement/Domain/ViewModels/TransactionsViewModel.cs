using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        public ObservableCollection<OperatingCostDisplayContainer> OperatingCosts { get; set; } = new ObservableCollection<OperatingCostDisplayContainer>();
        public ObservableCollection<PaymentDisplayContainer> Payments { get; set; } = new ObservableCollection<PaymentDisplayContainer>();
        public static IEnumerable<string> Tenants { get; set; } = InfoSysDbContext.G3Tenant.Include(i => i.G3Lease).ThenInclude(i => i.Tenant).Select(tenant => $"{tenant.FirstName} {tenant.LastName}").ToList();
        public static IEnumerable<DistributionKey> DistributionKeys { get; set; }

        public ICommand SaveCommand { get; set; }

        public TransactionsViewModel()
        {
            InfoSysDbContext.G3OperatingCosts.ToList().ForEach(operatingCost => OperatingCosts.Add(new OperatingCostDisplayContainer(operatingCost, $"")));
            InfoSysDbContext.G3Payments.ToList().ForEach(payment => Payments.Add(new PaymentDisplayContainer(payment, $"{payment.Lease?.Tenant.FirstName} {payment.Lease?.Tenant.LastName}")));

            DistributionKeys = new List<DistributionKey>()
            {
                DistributionKey.DistributeByArea,
                DistributionKey.DistributeByResidents
            };

            SaveCommand = new CommandImplementation(o => SaveChanges());
        }

        private void SaveChanges()
        {
            OperatingCosts.ToList().ForEach(cost =>
            {
                try
                {
                    if (cost.Id == 2)
                    {
                        cost.UpdateBaseInformation(InfoSysDbContext);
                        InfoSysDbContext.Update<G3OperatingCosts>(cost);
                        InfoSysDbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
            //Payments.ToList().ForEach(payment => InfoSysDbContext.Update<G3Payments>(payment));

            Snackbar.Enqueue("Update successful");
        }
    }
}
