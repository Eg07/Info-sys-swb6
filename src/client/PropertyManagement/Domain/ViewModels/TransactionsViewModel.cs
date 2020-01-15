using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static List<G3Tenant> AllTenants { get; set; } = InfoSysDbContext.G3Tenant.Include(i => i.G3Lease).ThenInclude(i => i.Tenant).ThenInclude(i => i.G3BankAccount).ToList();

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteTransactionsCommand { get; set; }

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
            DeleteTransactionsCommand = new CommandImplementation(o => DeleteTransactions());
        }

        private void DeleteTransactions()
        {
            InfoSysDbContext.RemoveRange(InfoSysDbContext.G3OperatingCosts.ToList());
            InfoSysDbContext.RemoveRange(InfoSysDbContext.G3Payments.ToList());
            InfoSysDbContext.SaveChanges();
            OperatingCosts.Clear();
            Payments.Clear();

            Snackbar.Enqueue("Successfully deleted all transactions");
        }

        private void SaveChanges()
        {
            UpdateOperatingCosts();
            UpdatePayments();
            Snackbar.Enqueue("Update successful");
        }

        private void UpdateOperatingCosts()
        {
            foreach (var cost in OperatingCosts)
            {
                try
                {
                    var lease = AllTenants.FirstOrDefault(tenant => $"{tenant.FirstName} {tenant.LastName}" == cost.Tenant)?.G3Lease.First();
                    var updateItem = InfoSysDbContext.G3OperatingCosts.First(dbCost => dbCost.Id == cost.Id);
                    if (lease != null)
                    {
                        cost.UpdateBaseInformation(lease);
                        updateItem.Unit = lease.Unit;
                        updateItem.Property = lease.Unit.Property;
                    }
                    
                    updateItem.DistributionKey = (int) cost.DistributionKeyDisplay;
                    InfoSysDbContext.G3OperatingCosts.Update(updateItem);
                    InfoSysDbContext.SaveChanges();
                }
                catch
                {
                    // ignore
                }
            }
        }

        private void UpdatePayments()
        {
            foreach (var payment in Payments)
            {
                try
                {
                    // skip if conditions apply
                    if (payment.Tenant == "") continue;

                    var lease = AllTenants
                        .FirstOrDefault(tenant => $"{tenant.FirstName} {tenant.LastName}" == payment.Tenant)?.G3Lease
                        .First();
                    if (lease == null) continue;
                    var updateItem = InfoSysDbContext.G3Payments.First(dbCost => dbCost.Id == payment.Id);

                    // update database and display object
                    payment.UpdateBaseInformation(lease);
                    updateItem.Lease = lease;
                    updateItem.IbanNavigation = lease.Tenant.G3BankAccount.First();

                    InfoSysDbContext.Update(updateItem);
                    InfoSysDbContext.SaveChanges();
                }
                catch
                {
                    // ignore
                }
                
            }
        }
    }
}
