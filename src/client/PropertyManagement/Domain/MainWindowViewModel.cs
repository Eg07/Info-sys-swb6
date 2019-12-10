using System.Configuration;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using System;

namespace PropertyManagement.Domain
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