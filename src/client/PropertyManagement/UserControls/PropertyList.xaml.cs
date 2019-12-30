using System.Linq;
using System.Windows.Controls;
using PropertyManagement.Database.DataModels;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyList.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PropertyList : UserControl
    {
        public static MainWindowViewModel NavigationContext { get; set; }

        public PropertyList()
        {
            InitializeComponent();
            DataContext = new PropertyListViewModel();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedProperty = (G3Property)((ListViewItem)sender).DataContext;
            PropertyDataViewModel.Id = selectedProperty.Id;
            NavigationContext.DisplayPropertyData();
        }
    }
}
