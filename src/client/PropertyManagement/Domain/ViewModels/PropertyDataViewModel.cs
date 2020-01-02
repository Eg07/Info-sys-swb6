﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database;
using PropertyManagement.DataContainers;

namespace PropertyManagement.Domain.ViewModels
{
    public class PropertyDataViewModel : ViewModelBase
    {
        public static int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public ObservableCollection<UnitDisplayContainer> PropertyUnits { get; }

        public static InfosysContext InfoSysDbContext;

        public PropertyDataViewModel()
        {
            // TODO: include more properties
            var property = InfoSysDbContext.G3Property
                .Include(i => i.Adress)
                .Include(i => i.G3Unit)
                .ThenInclude(i => i.G3Lease)
                .ThenInclude(i => i.Tenant)
                .SingleOrDefault(prop => prop.Id == Id);
            if(property == null)
                return;

            // populate basic info
            Street = property.Adress.Street;
            City = property.Adress.City;
            Zipcode = property.Adress.Zip;
            State = property.Adress.State;

            var displayContainerList = new List<UnitDisplayContainer>();
            property.G3Unit.ToList().ForEach(unit => displayContainerList.Add(new UnitDisplayContainer(unit)));
            PropertyUnits = new ObservableCollection<UnitDisplayContainer>(displayContainerList);
        }
    }
}
