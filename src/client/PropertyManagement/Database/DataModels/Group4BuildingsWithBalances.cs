using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4BuildingsWithBalances
    {
        public int BuildingId { get; set; }
        public int Zipcode { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string StreetNumberExtension { get; set; }
        public int SquareMeters { get; set; }
        public int SquareMetersOutside { get; set; }
    }
}
