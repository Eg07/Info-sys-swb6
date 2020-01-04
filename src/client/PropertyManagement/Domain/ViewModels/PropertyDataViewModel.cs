using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
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
        public ICommand DeleteCommand { get; private set; }

        public PropertyDataViewModel()
        {
            UpdateCommand = new CommandImplementation(o => UpdateDatabaseData());
            DeleteCommand = new CommandImplementation(o => DeleteDatabaseEntry());

            PopulateDynamicData();
        }

        private void UpdateDatabaseData()
        {
            try
            {
                InfoSysDbContext.G3Property.Update(Property);
                InfoSysDbContext.SaveChanges();
                Snackbar.Enqueue("Property data successfully updated");
            }
            catch (Exception e)
            {
                Snackbar.Enqueue("Update operation failed.");
                Debug.WriteLine(e);
            }
        }

        private void DeleteDatabaseEntry()
        {
            try
            {
                InfoSysDbContext.Remove(Property);
                InfoSysDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Snackbar.Enqueue("Delete operation failed.");
                Debug.WriteLine(e);
            }
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
