using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PropertyManagement.Database.DataModels
{
    public class PropertyDataModel
    {
        [Key]
        public int id { get; set; }
        public int house_nr { get; set; }
        public int zip { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }
}
