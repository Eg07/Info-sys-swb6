using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement
{
    /// <summary>
    /// Interaction logic for PropertyDetail.xaml
    /// </summary>
    public partial class PropertyDetail : UserControl
    {
        public PropertyDetail()
        {
            InitializeComponent();
            DataContext = new PropertyDetailViewModel();
        }
    }
}
