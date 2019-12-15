using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyDetail.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PropertyDetail : UserControl
    {
        public PropertyDetail()
        {
            InitializeComponent();
            DataContext = new PropertyDataViewModel();
        }
    }
}
