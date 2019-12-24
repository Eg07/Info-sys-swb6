using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group4ServiceChargeSettlement
    {
        public int Haus { get; set; }
        public double? WasserAllgStrom { get; set; }
        public double? WasserAllgStromPerson { get; set; }
        public double? Heizung { get; set; }
        public double? HeizungPerson { get; set; }
        public double? Straßenreinigung { get; set; }
        public double? StraßenreinigungQm { get; set; }
        public double? Grundsteuer { get; set; }
        public double? GrundsteuerQm { get; set; }
        public double? Versicherung { get; set; }
        public double? VersicherungQm { get; set; }
        public double? Gartenpflege { get; set; }
        public double? GartenpflegeQm { get; set; }
        public double? Summe { get; set; }
        public double? SummeNichtInMiete { get; set; }
        public double? Vorauszahlung { get; set; }
        public double? Nachzahlung { get; set; }
    }
}
