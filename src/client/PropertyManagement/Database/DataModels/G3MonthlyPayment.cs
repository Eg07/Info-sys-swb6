using System;
using System.Collections.Generic;

// ReSharper disable All
namespace PropertyManagement.Database.DataModels
{
    public class G3MonthlyPayment
    {
        public G3MonthlyPayment()
        {
            G3MonthlyPaid = new HashSet<G3MonthlyPaid>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int LeaseId { get; set; }
        public double TargetAmount { get; set; }

        public virtual G3Lease Lease { get; set; }
        public virtual ICollection<G3MonthlyPaid> G3MonthlyPaid { get; set; }
    }
}
