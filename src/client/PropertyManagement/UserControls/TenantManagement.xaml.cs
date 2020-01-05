using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for TenantManagement.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class TenantManagement : UserControl
    {
        public TenantManagement()
        {
            InitializeComponent();
            DataContext = new TenantManagementViewModel();
        }
    }
}
