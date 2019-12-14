﻿using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using PropertyManagement.Domain;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Snackbar Snackbar;

        public MainWindow()
        {
            InitializeComponent();

            // How to use the snackbar
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
            }).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the
                //, so need to be on the dispatcher
                MainSnackbar.MessageQueue.Enqueue("Welcome to your personal property management!");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue);
            Snackbar = MainSnackbar;
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new MessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        /// <summary>
        /// Handles click on NavigationMenuItem in navigation drawer
        /// </summary>
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                PickUserControlToDisplay(((NavigationMenuItem)((ListBox)sender).SelectedItem).Content.ToString());
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }
            
            MenuToggleButton.IsChecked = false;            
        }

        /// <summary>
        /// Picks correct user control to display by user control name
        /// </summary>
        /// <param name="userControlName">The name of the user control</param>
        private void PickUserControlToDisplay(string userControlName)
        {
            var view = (MainWindowViewModel) DataContext;

            switch (userControlName.Split('.').Last())
            {
                case "Home":
                    view.DisplayHome();
                    break;
                case "PropertyDetail":
                    view.DisplayPropertyData();
                    break;
            }
            
        }
    }
}
