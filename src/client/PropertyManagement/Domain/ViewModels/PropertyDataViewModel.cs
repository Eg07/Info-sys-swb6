using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class PropertyDataViewModel : ViewModelBase
    {
        private ObservableCollection<SelectableItem> _items3;
        private bool? _isAllItems3Selected;

        public PropertyDataViewModel()
        {
            _items3 = CreateData();

            Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                _items3[2].Description = "Whatever";
            });
        }

        public bool? IsAllItems3Selected
        {
            get => _isAllItems3Selected;
            set
            {
                if (_isAllItems3Selected == value) return;

                _isAllItems3Selected = value;

                if (_isAllItems3Selected.HasValue)
                    SelectAll(_isAllItems3Selected.Value, Items3);

                OnPropertyChanged("IsAllItems3Selected");
            }
        }

        private static void SelectAll(bool select, IEnumerable<SelectableItem> models)
        {
            foreach (var model in models)
                model.IsSelected = select;
        }

        private static ObservableCollection<SelectableItem> CreateData()
        {
            return new ObservableCollection<SelectableItem>
            {
                new SelectableItem
                {
                    Id = 1,
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit"
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

        public ObservableCollection<SelectableItem> Items3 => _items3;

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected new virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
