using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4BuildingCostParam
    {
        public int Haus { get; set; }
        public string Straße { get; set; }
        public int Hausnr { get; set; }
        public int Plz { get; set; }
        public string Ort { get; set; }
        public int? Wohnfläche { get; set; }
        public int? Personen { get; set; }
        public int? Apartments { get; set; }
        public int? Vermietet { get; set; }
    }
}
