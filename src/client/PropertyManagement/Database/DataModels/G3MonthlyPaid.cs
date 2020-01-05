using System;

// ReSharper disable All
namespace PropertyManagement.Database.DataModels
{
    public class G3MonthlyPaid
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public string Iban { get; set; }
        public int MonthlyPaymnetId { get; set; }

        public virtual G3BankAccount IbanNavigation { get; set; }
        public virtual G3MonthlyPayment MonthlyPaymnet { get; set; }
    }
}
