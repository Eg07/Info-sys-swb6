using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.Database.DataModels;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class PropertyDataViewModel : ViewModelBase
    {
        public static int Id { get; set; }
        public G3Property Property { get; set; }
        public ObservableCollection<UnitDisplayContainer> PropertyUnits { get; set; }

        public ICommand UpdateCommand { get; private set; }
        public static InfosysContext InfoSysDbContext;


        public PropertyDataViewModel()
        {
            UpdateCommand = new CommandImplementation(o => UpdateDatabaseData());

            PopulateDynamicData();
        }

        public void UpdateDatabaseData()
        {
            InfoSysDbContext.G3Property.Update(Property);
            InfoSysDbContext.SaveChangesAsync();
        }

        private void PopulateDynamicData()
        {
            Property = InfoSysDbContext.G3Property
                .Include(i => i.Adress)
                .Include(i => i.G3Unit)
                .ThenInclude(i => i.G3Lease)
                .ThenInclude(i => i.Tenant)
                .SingleOrDefault(prop => prop.Id == Id);
            if (Property == null)
                return;

            // populate basic info
            var displayContainerList = new List<UnitDisplayContainer>();
            Property.G3Unit.ToList().ForEach(unit => displayContainerList.Add(new UnitDisplayContainer(unit)));
            PropertyUnits = new ObservableCollection<UnitDisplayContainer>(displayContainerList);
        }
    }
}
