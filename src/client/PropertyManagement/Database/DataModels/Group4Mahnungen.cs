using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4Mahnungen
    {
        public int ApartmentId { get; set; }
        public int? Mietmonate { get; set; }
        public double? Erwartet { get; set; }
        public double? Eingegangen { get; set; }
        public double? Differenz { get; set; }
        public string Hinweis { get; set; }
    }
}
