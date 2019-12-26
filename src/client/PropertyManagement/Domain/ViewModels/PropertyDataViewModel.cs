using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class PropertyDataViewModel : ViewModelBase
    {
        public ObservableCollection<G3Unit> PropertyUnits { get; }
        public static InfosysContext InfoSysDbContext;
        private bool? _isAllItems3Selected;
        
        public PropertyDataViewModel()
        {
            PropertyUnits = new ObservableCollection<G3Unit>(InfoSysDbContext.G3Unit.ToList());
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

        private static ObservableCollection<SelectableItem> CreateData()
        {
            var randomData = InfoSysDbContext.G3Address.First();
            return new ObservableCollection<SelectableItem>
            {
                new SelectableItem
                {
                    Id = 1,
                    Name = randomData.City,
                    Description = randomData.State
                },
                new SelectableItem
                {
                    Id = 2,
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                },
                new SelectableItem
                {
                    Id = 3,
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                }
            };
        }

        

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected new virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
