using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PropertyManagement.UserControls;

// ReSharper disable MemberCanBePrivate.Global
namespace PropertyManagement.Domain.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // The menu items in the navigation drawer
        public NavigationMenuItem[] MenuItems { get; set; }

        private readonly SortedDictionary<string, UserControl> _userControls = new SortedDictionary<string, UserControl>();

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
        public ICommand LoadTenantManagementCommand { get; private set; }
        public ICommand LoadTransactionsCommand { get; private set; }


        public void DisplayHome() => CurrentViewModel = (HomeViewModel)_userControls.Values.OfType<Home>().First().DataContext;

        public void DisplayTenantManagement() => CurrentViewModel = (TenantManagementViewModel)_userControls.Values.OfType<TenantManagement>().First().DataContext;
        public void DisplayPropertyData() => CurrentViewModel = (PropertyDataViewModel)_userControls.Values.OfType<PropertyDetail>().First().DataContext;
        public void DisplayTransactions() => CurrentViewModel = (TransactionsViewModel)_userControls.Values.OfType<Transactions>().First().DataContext;

        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));
            
            RegisterCommands();
            GenerateNavigationDrawer();
            DisplayHome();
        }

        private void GenerateNavigationDrawer()
        {
            _userControls.Add("Home", new Home());
            _userControls.Add("TenantManagement", new TenantManagement());
            _userControls.Add("PropertyData", new PropertyDetail());
            _userControls.Add("Transactions", new Transactions());
            MenuItems = new NavigationMenuItem[_userControls.Count];

            // add the UserControls to the navigation drawer
            for (var i = 0; i < _userControls.Count; i++)
                MenuItems[i] = new NavigationMenuItem(_userControls.Keys.ElementAt(i), _userControls.Values.ElementAt(i));
        }

        /// <summary>
        /// Hook up Commands to associated methods
        /// </summary>
        private void RegisterCommands()
        {
            LoadHomeCommand = new CommandImplementation(o => DisplayHome());
            LoadPropertyDataCommand = new CommandImplementation(o => DisplayPropertyData());
            LoadTenantManagementCommand = new CommandImplementation(o => DisplayTenantManagement());
            LoadTransactionsCommand = new CommandImplementation(o => DisplayTransactions());
        }
    }
}