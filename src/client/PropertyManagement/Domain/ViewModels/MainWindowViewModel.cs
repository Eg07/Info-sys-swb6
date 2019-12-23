﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;
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
        public readonly ApplicationDbContext DbContext;

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
        
        // commands for loading the different user views
        public ICommand LoadHomeCommand { get; private set; }
        public ICommand LoadPropertyDataCommand { get; private set; }
        public ICommand LoadTenantManagementCommand { get; private set; }
        public ICommand LoadTransactionsCommand { get; private set; }
        
        // view model change bound to methods
        public void DisplayHome() => CurrentViewModel = (HomeViewModel)_userControls.Values.OfType<Home>().First().DataContext;
        public void DisplayTenantManagement() => CurrentViewModel = (TenantManagementViewModel)_userControls.Values.OfType<TenantManagement>().First().DataContext;
        public void DisplayPropertyData() => CurrentViewModel = (PropertyDataViewModel)_userControls.Values.OfType<PropertyData>().First().DataContext;
        public void DisplayTransactions() => CurrentViewModel = (TransactionsViewModel)_userControls.Values.OfType<Transactions>().First().DataContext;

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
            
            RegisterCommands();
            GenerateNavigationDrawer();
            DisplayHome();

            // TODO: move to own method
            // try to establish connection with database
            DbContext = new ApplicationDbContext();
            DbContext.Database.EnsureCreated();
            
            // TODO: remove later
            //CreateSampleDataSet();
            ExecuteSampleQuery();
        }

        private void GenerateNavigationDrawer()
        {
            _userControls.Add("Home", new Home());
            _userControls.Add("TenantManagement", new TenantManagement());
            _userControls.Add("PropertyData", new PropertyData());
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

        /// <summary>
        /// Creates some sample data for property table
        /// </summary>
        private void CreateSampleDataSet()
        {
            var house1 = new PropertyDataModel { city = "Stuttgart", house_nr = 7, state = "Baden-Wuerttemberg", street = "Königsstrasse", zip = 70104 };
            var house2 = new PropertyDataModel { city = "Karlsruhe", house_nr = 23, state = "Baden-Wuerttemberg", street = "Kronenstrasse", zip = 73021 };
            var house3 = new PropertyDataModel { city = "Bonn", house_nr = 68, state = "Nordrhein-Westfalen", street = "Breite Strasse", zip = 53111 };
            var house4 = new PropertyDataModel { city = "Tollwitz", house_nr = 6, state = "Sachsen-Anhalt", street = "Gewuerzstrasse", zip = 06231 };
            DbContext.Properties.Add(house1);
            DbContext.Properties.Add(house2);
            DbContext.Properties.Add(house3);
            DbContext.Properties.Add(house4);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Performs a SELECT * over the Properties table
        /// </summary>
        private void ExecuteSampleQuery()
        {
            DbContext.Properties.ToList().ForEach(property => Debug.WriteLine($"id: {property.id}, street: {property.street} {property.house_nr}, zipcode: {property.zip}, city: {property.city}, state: {property.state}"));
        }
    }
}