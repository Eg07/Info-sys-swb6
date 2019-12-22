using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PropertyManagement.DataContainers
{
    public class SelectableItem
    {
        private bool _isSelected;
        private string _name;
        private string _description;
        private uint _id;
        private double _numeric;
        private string _food;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public uint Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public double Numeric
        {
            get => _numeric;
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_numeric == value) return;
                _numeric = value;
                OnPropertyChanged();
            }
        }

        public string Food
        {
            get => _food;
            set
            {
                if (_food == value) return;
                _food = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
