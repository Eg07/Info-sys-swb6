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
            InfoSysDbContext.G3OperatingCosts
                .Include(i => i.Unit)
                .ThenInclude(i => i.G3Lease).ToList()
                .ForEach(operatingCost => OperatingCosts.Add(new OperatingCostDisplayContainer(operatingCost, $"{operatingCost?.Unit?.G3Lease?.First()?.Tenant?.FirstName} {operatingCost?.Unit?.G3Lease?.First()?.Tenant?.LastName}")));
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
            try
            {
                foreach (var cost in OperatingCosts.ToList())
                {
                    if (cost.Tenant == "") continue;
                    var lease = InfoSysDbContext.G3Tenant.Include(i => i.G3Lease).ToList()
                        .FirstOrDefault(tenant => $"{tenant.FirstName} {tenant.LastName}" == cost.Tenant)?.G3Lease
                        .First();
                    if (lease == null) continue;
                    var updateItem = InfoSysDbContext.G3OperatingCosts.First(dbCost => dbCost.Id == cost.Id);

                    // update database and display object
                    cost.UpdateBaseInformation(lease);
                    updateItem.Unit = lease.Unit;
                    updateItem.Property = lease.Unit.Property;

                    InfoSysDbContext.G3OperatingCosts.Update(updateItem);
                    InfoSysDbContext.SaveChanges();
                }

                Snackbar.Enqueue("Update successful");
            }
            catch (Exception)
            {
                Snackbar.Enqueue("This should not have happened.");
            }
        }
    }
}
