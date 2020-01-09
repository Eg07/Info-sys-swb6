﻿using System;

namespace PropertyManagement.Database.DataModels
{
    public partial class G3Payments
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ValutaDate { get; set; }
        public int LeaseId { get; set; }
        public string Iban { get; set; }

        public virtual G3BankAccount IbanNavigation { get; set; }
        public virtual G3Lease Lease { get; set; }
    }
}
