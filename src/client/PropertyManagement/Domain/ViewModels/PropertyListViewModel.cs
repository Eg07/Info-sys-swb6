using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.Domain.ViewModels
{
    public class PropertyListViewModel : ViewModelBase
    {
        public ObservableCollection<G3Property> Properties { get; set; }

        public PropertyListViewModel()
        {
            Properties = new ObservableCollection<G3Property>(InfoSysDbContext.G3Property.Include("Adress").ToList());
            //Properties.First().Adress.Street
        }
    }
}
