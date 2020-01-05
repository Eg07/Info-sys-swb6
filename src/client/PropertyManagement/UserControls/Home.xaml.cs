using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class Home : UserControl
    {

        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    }
}
