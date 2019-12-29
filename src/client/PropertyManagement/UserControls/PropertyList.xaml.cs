using System.Linq;
using System.Windows.Controls;
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
            var test = (ListViewItem) sender;
            var test2 = test.DataContext;
            NavigationContext.DisplayPropertyData();
        }
    }
}
