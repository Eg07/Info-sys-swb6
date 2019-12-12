using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.Domain
{
    /// <summary>
    /// Interaction logic for PaletteSelector.xaml
    /// </summary>
    public partial class PaletteSelector : UserControl
    {
        public PaletteSelector()
        {
            InitializeComponent();
            DataContext = new PaletteSelectorViewModel();
        }
    }
}
