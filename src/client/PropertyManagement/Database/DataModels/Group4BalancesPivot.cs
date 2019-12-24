using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4BalancesPivot
    {
        public int? BalYear { get; set; }
        public double? Mayer { get; set; }
        public double? Mueller { get; set; }
        public double? Haug { get; set; }
        public double? Barni { get; set; }
        public int SortColumn { get; set; }
    }
}
