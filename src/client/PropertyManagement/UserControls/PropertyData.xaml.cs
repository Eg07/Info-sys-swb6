using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyData.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PropertyData : UserControl
    {
        public PropertyData()
        {
            InitializeComponent();
            DataContext = new PropertyDataViewModel();
        }
    }
}
