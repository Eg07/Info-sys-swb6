using System.Collections.ObjectModel;
using System.Linq;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        public ObservableCollection<OperatingCostDisplayContainer> OperatingCosts { get; set; } = new ObservableCollection<OperatingCostDisplayContainer>();
        public ObservableCollection<PaymentDisplayContainer> Payments { get; set; } = new ObservableCollection<PaymentDisplayContainer>();


        public TransactionsViewModel()
        {
            InfoSysDbContext.G3OperatingCosts.ToList().ForEach(operatingCost => OperatingCosts.Add(new OperatingCostDisplayContainer(operatingCost)));
            InfoSysDbContext.G3Payments.ToList().ForEach(payment => Payments.Add(new PaymentDisplayContainer(payment)));

        }
    }
}
