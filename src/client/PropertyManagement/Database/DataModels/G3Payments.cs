using System;

// ReSharper disable VirtualMemberCallInConstructor
namespace PropertyManagement.Database.DataModels
{
    public class G3Payments
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Association { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ValutaDate { get; set; }
        public int LeaseId { get; set; }
        public string Iban { get; set; }

        public virtual G3BankAccount IbanNavigation { get; set; }
        public virtual G3Lease Lease { get; set; }

        public G3Payments() { }

        public G3Payments(G3Payments payment)
        {
            Id = payment.Id;
            Description = payment.Description;
            Association = payment.Association;
            Type = payment.Type;
            Amount = payment.Amount;
            BookingDate = payment.BookingDate;
            ValutaDate = payment.ValutaDate;
            LeaseId = payment.LeaseId;
            Iban = payment.Iban;
            IbanNavigation = payment.IbanNavigation;
            Lease = payment.Lease;
        }
    }
}
