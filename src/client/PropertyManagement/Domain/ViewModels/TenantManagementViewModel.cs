using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database.DataModels;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TenantManagementViewModel : ViewModelBase
    {
        public List<G3Tenant> TenantList { get; set; }
        public ObservableCollection<TenantDisplayContainer> Tenants { get; set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public TenantManagementViewModel()
        {
            UpdateCommand = new CommandImplementation(o => UpdateDatabaseData());
            DeleteCommand = new CommandImplementation(o => DeleteDatabaseEntry());

            PopulateDynamicData();
        }

        private void UpdateDatabaseData()
        {
            var idList = Tenants.Where(tenant => tenant.IsSelected).Select(tenant => tenant.Id).ToList();
            var updateList = TenantList.Where(tenant => idList.Contains(tenant.Id)).ToList();
            if (updateList.Count < 1) return;

            var status = true;
            updateList.ForEach(item =>
            {
                // if one item is false status will remain false (simple boolean and)
                status &= InfoSysDbContext.UpdateDatabaseEntry(item);
            });

            var itemsUpdated = GetIdsOfSelectedItems(updateList);
            Snackbar.Enqueue(status ? $"Tenant data successfully updated for Ids: {itemsUpdated}" : $"Update operation failed for Ids: {itemsUpdated}");
        }

        private void DeleteDatabaseEntry()
        {
            var idList = Tenants.Where(tenant => tenant.IsSelected).Select(tenant => tenant.Id).ToList();
            var deletionList = TenantList.Where(tenant => idList.Contains(tenant.Id)).ToList();
            if (deletionList.Count < 1) return;
            
            var status = true;
            deletionList.ForEach(item =>
            {
                // if one item is false status will remain false (simple boolean and)
                status &= InfoSysDbContext.DeleteDatabaseEntry(item);
            });

            var itemsDeleted = GetIdsOfSelectedItems(deletionList);
            Snackbar.Enqueue(status ? $"Tenant data successfully deleted for Ids: {itemsDeleted}" : $"Delete operation failed for Ids: {itemsDeleted}");
        }

        private string GetIdsOfSelectedItems(List<G3Tenant> items)
        {
            var ids = new StringBuilder(items.First().Id.ToString());
            items.Remove(items.First());
            items.ForEach(item => ids.Append($",{item.Id} "));

            return ids.ToString();
        }

        private void PopulateDynamicData()
        {
            // populate basic info
            TenantList = InfoSysDbContext.G3Tenant
                .Include(tenant => tenant.G3BankAccount)
                .Include(tenant => tenant.G3Lease)
                .ThenInclude(lease => lease.Unit)
                .ThenInclude(unit => unit.Property)
                .ThenInclude(property => property.Adress)
                .ToList();
            var displayContainerList = new List<TenantDisplayContainer>();

            TenantList.ForEach(tenant => displayContainerList.Add(new TenantDisplayContainer(tenant)));
            Tenants = new ObservableCollection<TenantDisplayContainer>(displayContainerList);
        }
    }
}
