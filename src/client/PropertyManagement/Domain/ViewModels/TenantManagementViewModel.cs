using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TenantManagementViewModel : ViewModelBase
    {
        public ObservableCollection<TenantDisplayContainer> Tenants { get; set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public TenantManagementViewModel()
        {
            PopulateDynamicData();
        }

        private void PopulateDynamicData()
        {
            // populate basic info
            var tenants = InfoSysDbContext.G3Tenant
                .Include(tenant => tenant.G3BankAccount)
                .Include(tenant => tenant.G3Lease)
                .ThenInclude(lease => lease.Unit)
                .ThenInclude(unit => unit.Property)
                .ThenInclude(property => property.Adress)
                .ToList();
            var displayContainerList = new List<TenantDisplayContainer>();

            tenants.ForEach(tenant => displayContainerList.Add(new TenantDisplayContainer(tenant)));
            Tenants = new ObservableCollection<TenantDisplayContainer>(displayContainerList);
        }
    }
}
