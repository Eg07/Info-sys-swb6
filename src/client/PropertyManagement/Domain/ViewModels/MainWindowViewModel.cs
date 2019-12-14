using System;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

// ReSharper disable MemberCanBePrivate.Global
namespace PropertyManagement.Domain.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // The menu items in the navigation drawer
        public NavigationMenuItem[] MenuItems { get; }

        // ViewModel that is currently bound to the ContentControl
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        public ICommand LoadHomeCommand { get; private set; }
        public ICommand LoadPropertyDataCommand { get; private set; }

        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));
            
            // load home on startup
            DisplayHome();

            // Hook up Commands to associated methods
            LoadHomeCommand = new CommandImplementation(o => DisplayHome());
            LoadPropertyDataCommand = new CommandImplementation(o => DisplayPropertyData());

            MenuItems = new[]
            {
                new NavigationMenuItem("Home", new UserControls.Home()),
                new NavigationMenuItem("Property Data", new UserControls.PropertyDetail()),
            };
        }

        public void DisplayHome() => CurrentViewModel = new HomeViewModel();
        public void DisplayPropertyData() => CurrentViewModel = new PropertyDetailViewModel();
    }
}