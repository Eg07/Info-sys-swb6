using System;
using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace PropertyManagement.Domain.ViewModels
{
    public class PaletteSelectorViewModel
    {
        public IEnumerable<Swatch> Swatches { get; }

        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
        }

        public ICommand ToggleBaseCommand { get; } = new CommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark) => ModifyTheme(theme => theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light));

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }
    }
}
