using System;

// ReSharper disable All
namespace PropertyManagement.Database.DataModels
{
    public class G3Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime DueDate { get; set; }
        public int UnitId { get; set; }
        public int DistributionKey { get; set; }
        public int PropertyId { get; set; }

        public virtual G3Property Property { get; set; }
        public virtual G3Unit Unit { get; set; }
    }
}
