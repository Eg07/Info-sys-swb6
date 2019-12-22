using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyList.xaml
    /// </summary>
    public partial class PropertyList : UserControl
    {
        public PropertyList()
        {
            InitializeComponent();
            DataContext = new PropertyListViewModel();
        }
    }
}
