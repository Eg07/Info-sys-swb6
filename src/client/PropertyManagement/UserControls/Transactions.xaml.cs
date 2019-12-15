using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class Transactions : UserControl
    {
        public Transactions()
        {
            InitializeComponent();
            DataContext = new TransactionsViewModel();
        }
    }
}
