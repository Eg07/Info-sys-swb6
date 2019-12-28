using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.Domain.ViewModels
{
    public class PropertyDataViewModel : ViewModelBase
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public ObservableCollection<G3Unit> PropertyUnits { get; }

        public static InfosysContext InfoSysDbContext;
        private bool? _isAllItems3Selected;
        
        public PropertyDataViewModel()
        {
            PropertyUnits = new ObservableCollection<G3Unit>(InfoSysDbContext.G3Unit.ToList());

            var property = InfoSysDbContext.G3Property.Include("Adress").First();
            Street = property.Adress.Street;
            City = property.Adress.City;
            Zipcode = property.Adress.Zip;
            State = property.Adress.State;
        }

        public bool? IsAllItems3Selected
        {
            get => _isAllItems3Selected;
            set
            {
                if (_isAllItems3Selected == value) return;

                _isAllItems3Selected = value;

                //if (_isAllItems3Selected.HasValue)
                //    SelectAll(_isAllItems3Selected.Value, PropertyUnits);

                OnPropertyChanged("IsAllItems3Selected");
            }
        }

        //private static void SelectAll(bool select, IEnumerable<G3Unit> models)
        //{
        //    foreach (var model in models)
        //        model.IsSelected = select;
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected new virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
