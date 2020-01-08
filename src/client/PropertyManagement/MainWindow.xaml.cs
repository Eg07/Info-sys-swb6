using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using PropertyManagement.DataContainers;
using PropertyManagement.Domain;
using PropertyManagement.Domain.UiElements;
using PropertyManagement.Domain.ViewModels;

namespace PropertyManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : Window
    {
        public static Snackbar Snackbar;

        public MainWindow()
        {
            InitializeComponent();
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

            if (userControlName.Split('.').Last() == "Home")
                view.DisplayHome();
            else if (userControlName.Split('.').Last() == "PropertyList")
                view.DisplayPropertyList();
            else if (userControlName.Split('.').Last() == "Transactions")
                view.DisplayTransactions();
            else if (userControlName.Split('.').Last() == "TenantManagement")
                view.DisplayTenantManagement();
        }
    }
}
