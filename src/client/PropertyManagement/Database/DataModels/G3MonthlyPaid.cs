using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public partial class G3MonthlyPaid
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
