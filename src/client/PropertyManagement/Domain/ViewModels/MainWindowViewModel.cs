using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PropertyManagement.Database;
using PropertyManagement.DataContainers;
using PropertyManagement.UserControls;

// ReSharper disable MemberCanBePrivate.Global
namespace PropertyManagement.Domain.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The database context for accessing persisted data.
        /// </summary>
        public readonly InfosysContext DbContext;

        /// <summary>
        /// The menu items in the navigation drawer
        /// </summary>
        public NavigationMenuItem[] MenuItems { get; set; }

        /// <summary>
        /// ViewModel that is currently bound to the ContentControl
        /// </summary>
        private ViewModelBase _currentViewModel;

        /// <summary>
        /// ViewModel that is currently bound to the ContentControl
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        #region Navigation

        // commands for loading the different user views
        public ICommand LoadHomeCommand { get; private set; }
        public ICommand LoadPropertyListCommand { get; private set; }
        public ICommand LoadPropertyDataCommand { get; private set; }
        public ICommand LoadTenantManagementCommand { get; private set; }
        public ICommand LoadTransactionsCommand { get; private set; }

        // view model change bound to methods
        public void DisplayHome() => CurrentViewModel = (HomeViewModel)_userControls.Values.OfType<Home>().First().DataContext;
        public void DisplayTenantManagement() => CurrentViewModel = (TenantManagementViewModel)_userControls.Values.OfType<TenantManagement>().First().DataContext;
        public void DisplayPropertyList() => CurrentViewModel = (PropertyListViewModel)_userControls.Values.OfType<PropertyList>().First().DataContext;
        public void DisplayPropertyData() => CurrentViewModel = (PropertyDataViewModel)_userControls.Values.OfType<PropertyData>().First().DataContext;
        public void DisplayTransactions() => CurrentViewModel = (TransactionsViewModel)_userControls.Values.OfType<Transactions>().First().DataContext;

        #endregion
        
        /// <summary>
        /// Complete set of our user views
        /// </summary>
        private readonly SortedDictionary<string, UserControl> _userControls = new SortedDictionary<string, UserControl>();

        /// <summary>
        /// Default Constructor for the Main View Model
        /// </summary>
        /// <param name="snackbarMessageQueue"></param>
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

            // TODO: move to own method
            // try to establish connection with database
            DbContext = new InfosysContext();
            snackbarMessageQueue.Enqueue("✔️ Database connection successfully established");
            //DbContext.Database.EnsureCreated();


            // TODO: assign on base
            // assign DbContext to ViewModels
            PropertyDataViewModel.InfoSysDbContext = DbContext;
            PropertyListViewModel.InfoSysDbContext = DbContext;

            // commands to switch views
            RegisterCommands();
            // generate main navigation drawer visible on every view
            GenerateNavigationDrawer();
            // go to home view on startup
            DisplayHome();
        }

        private void GenerateNavigationDrawer()
        {
            _userControls.Add("Home", new Home());
            _userControls.Add("Tenant Management", new TenantManagement());
            _userControls.Add("Managed Properties", new PropertyList());
            _userControls.Add("Transactions", new Transactions());
            MenuItems = new NavigationMenuItem[_userControls.Count];
            
            // add the UserControls to the navigation drawer
            for (var i = 0; i < _userControls.Count; i++)
                MenuItems[i] = new NavigationMenuItem(_userControls.Keys.ElementAt(i), _userControls.Values.ElementAt(i));

            // add controls not shown in drawer
            _userControls.Add("PropertyData", new PropertyData());

            // assign related data
            PropertyList.NavigationContext = this;
        }

        /// <summary>
        /// Hook up Commands to associated methods
        /// </summary>
        private void RegisterCommands()
        {
            LoadHomeCommand = new CommandImplementation(o => DisplayHome());
            LoadPropertyListCommand = new CommandImplementation(o => DisplayPropertyList());
            LoadPropertyDataCommand = new CommandImplementation(o => DisplayPropertyData());
            LoadTenantManagementCommand = new CommandImplementation(o => DisplayTenantManagement());
            LoadTransactionsCommand = new CommandImplementation(o => DisplayTransactions());
        }
    }
}