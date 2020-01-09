using System;

namespace PropertyManagement.Database.DataModels
{
    public partial class G3OperatingCosts
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ValutaDate { get; set; }
        public int UnitId { get; set; }
        public int DistributionKey { get; set; }
        public int PropertyId { get; set; }

        public virtual G3Property Property { get; set; }
        public virtual G3Unit Unit { get; set; }
    }
}
