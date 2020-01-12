using System.Collections.ObjectModel;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        public ObservableCollection<TenantDisplayContainer> Tenants { get; set; }

        public TransactionsViewModel()
        {

        }
    }
}
