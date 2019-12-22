using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagement.Database.DataModels
{
    public class UnitModel
    {
        [Key]
        public int id { get; set; }
        public float rooms_nr { get; set; }
        public float area { get; set; }
        public int floor { get; set; }
        public int propertyId { get; set; }
        public int serviceId { get; set; }
        public int resident_Nr { get; set; }
    }
}
