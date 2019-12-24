using System;
using System.Collections.Generic;

namespace PropertyManagement.Database.DataModels
{
    public class G3Lease
    {
        public G3Lease()
        {
            G3MonthlyPayment = new HashSet<G3MonthlyPayment>();
        }

        public int Id { get; set; }
        public double Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UnitId { get; set; }
        public int TenantId { get; set; }

        public virtual G3Tenant Tenant { get; set; }
        public virtual G3Unit Unit { get; set; }
        public virtual ICollection<G3MonthlyPayment> G3MonthlyPayment { get; set; }
    }
}
