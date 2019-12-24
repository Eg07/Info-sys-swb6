using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4BalancesAllMonths
    {
        public int BuildingId { get; set; }
        public int ApartmentId { get; set; }
        public int RentalContractId { get; set; }
        public int? Monat { get; set; }
        public double? Soll { get; set; }
        public double? Haben { get; set; }
    }
}
