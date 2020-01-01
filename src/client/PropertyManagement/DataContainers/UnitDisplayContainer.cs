using System;
using System.Collections.Generic;
using System.Text;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.DataContainers
{
    public class UnitDisplayContainer : G3Unit
    {
        public double MonthlyRent { get; set; }
        public string TenantName { get; set; }

        public UnitDisplayContainer()
        {

        }
    }
}
