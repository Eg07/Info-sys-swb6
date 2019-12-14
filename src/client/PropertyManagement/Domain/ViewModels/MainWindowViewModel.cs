using System;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
// ReSharper disable MemberCanBePrivate.Global

namespace PropertyManagement.Domain.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // The menu items in the navigation drawer
        public MenuItem[] MenuItems { get; }

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
        public ICommand LoadPropertyDetailCommand { get; private set; }

        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));
            
            // load home on startup
            DisplayHome();

            // Hook up Commands to associated methods
            LoadHomeCommand = new CommandImplementation(o => DisplayHome());
            LoadPropertyDetailCommand = new CommandImplementation(o => DisplayPropertyDetail());

            MenuItems = new[]
            {
                new MenuItem("Home", new Home()),
                new MenuItem("Property Data", new PropertyDetail()),
            };
        }

        public void DisplayHome() => CurrentViewModel = new HomeViewModel();
        public void DisplayPropertyDetail() => CurrentViewModel = new PropertyDetailViewModel();
    }
}