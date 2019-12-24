using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class Group6UtilityInvoice
    {
        public int UtilityInvoiceId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int PropertyId { get; set; }
        public int FlatId { get; set; }

        public virtual Group6FlatForRent Flat { get; set; }
        public virtual Group6Property Property { get; set; }
    }
}
