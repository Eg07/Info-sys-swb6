using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4RentVsBalance
    {
        public int Haus { get; set; }
        public string Name { get; set; }
        public int? Monat { get; set; }
        public double? Haben { get; set; }
        public double? Soll { get; set; }
        public double? SaldoMonat { get; set; }
        public double? SaldoAkkumuliert { get; set; }
    }
}
