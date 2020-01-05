using System.ComponentModel;
using MaterialDesignThemes.Wpf;
using PropertyManagement.Database;

namespace PropertyManagement.Domain.ViewModels
{
    /// <summary>
    /// All other view models inherit from this base to be able to notify
    /// in the case of a changed property
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The database context for accessing persisted data.
        /// </summary>
        protected static InfosysContext InfoSysDbContext;
        protected static ISnackbarMessageQueue Snackbar;
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected ViewModelBase()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
