using System.Windows.Controls;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement.Domain.UiElements
{
    /// <summary>
    /// Interaction logic for PaletteSelector.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class PaletteSelector : UserControl
    {
        public PaletteSelector()
        {
            InitializeComponent();
            DataContext = new PaletteSelectorViewModel();
        }
    }
}
