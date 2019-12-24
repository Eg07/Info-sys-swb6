using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4BalancesBuilding
    {
        public int? BuildingId { get; set; }
        public int? Level { get; set; }
        public string AddressExtension { get; set; }
        public double? Rent { get; set; }
        public int? PersonId { get; set; }
        public string Lastname { get; set; }
        public double Value { get; set; }
        public int? BalYear { get; set; }
        public int? BalMonth { get; set; }
        public double? NkAnteil { get; set; }
    }
}
