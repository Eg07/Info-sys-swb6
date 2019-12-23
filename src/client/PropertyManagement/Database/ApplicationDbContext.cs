using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UnitModel> Units { get; set; }
        public DbSet<PropertyDataModel> Properties { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=propertymanagementdb.cq72eczgx04v.eu-central-1.rds.amazonaws.com;Database=PropertyManagement;Persist Security Info=True;User ID=robin;Password=7HqZACddw3V4hlWGItm9");
        }
    }
}
