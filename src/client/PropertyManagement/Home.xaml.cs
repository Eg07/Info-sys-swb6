using System.Windows.Controls;
using System.Windows.Input;
using PropertyManagement.Domain;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {

        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    }
}
