﻿using System.Collections.Generic;

// ReSharper disable VirtualMemberCallInConstructor
namespace PropertyManagement.Database.DataModels
{
    public class G3BankAccount
    {
        public G3BankAccount()
        {
            G3Payments = new HashSet<G3Payments>();
        }

        public string Iban { get; set; }
        public int TenantId { get; set; }

        public virtual G3Tenant Tenant { get; set; }
        public virtual ICollection<G3Payments> G3Payments { get; set; }
    }
}
