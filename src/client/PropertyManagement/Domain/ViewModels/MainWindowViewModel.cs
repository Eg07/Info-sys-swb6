using System;
using MaterialDesignThemes.Wpf;

namespace PropertyManagement.Domain.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

            MenuItems = new[]
            {
                new MenuItem("Home", new Home()),
                new MenuItem("Second Item", new Home()),
            };
        }

        public MenuItem[] MenuItems { get; }
    }
}