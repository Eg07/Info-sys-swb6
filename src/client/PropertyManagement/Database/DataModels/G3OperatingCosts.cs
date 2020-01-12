using System;

// ReSharper disable VirtualMemberCallInConstructor
namespace PropertyManagement.Database.DataModels
{
    public class G3OperatingCosts
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Association { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ValutaDate { get; set; }
        public int? UnitId { get; set; }
        public int DistributionKey { get; set; }
        public int? PropertyId { get; set; }

        public virtual G3Property Property { get; set; }
        public virtual G3Unit Unit { get; set; }

        public G3OperatingCosts() { }

        public G3OperatingCosts(G3OperatingCosts operatingCost)
        {
            Id = operatingCost.Id;
            Description = operatingCost.Description;
            Association = operatingCost.Association;
            Type = operatingCost.Type;
            Amount = operatingCost.Amount;
            BookingDate = operatingCost.BookingDate;
            ValutaDate = operatingCost.ValutaDate;
            UnitId = operatingCost.UnitId;
            DistributionKey = operatingCost.DistributionKey;
            PropertyId = operatingCost.UnitId;
            Property = operatingCost.Property;
            Unit = operatingCost.Unit;
        }

    }
}
