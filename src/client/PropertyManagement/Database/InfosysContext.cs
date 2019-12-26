using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database.DataModels;

// ReSharper disable All
namespace PropertyManagement.Database
{
    public partial class InfosysContext : DbContext
    {
        public InfosysContext()
        {
        }

        public InfosysContext(DbContextOptions<InfosysContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Creates some sample data for address table
        /// </summary>
        public void CreateSampleAddressDataSet()
        {
            // references should be added later
            var addressExample1 = new G3Address
            {
                City = "Stuttgart",
                HouseNr = 6,
                State = "Baden-Württemberg",
                Street = "Königsstraße",
                Zip = 70173,
                G3Property = null,
                G3Owner = null
            };
            var addressExample2 = new G3Address
            {
                City = "Bonn",
                HouseNr = 68,
                State = "Nordrhein-Westfalen",
                Street = "Breite Straße",
                Zip = 53111,
                G3Property = null,
                G3Owner = null
            };
            // Add address
            G3Address.Add(addressExample1);
            G3Address.Add(addressExample2);
            SaveChanges();
        }

        /// <summary>
        /// Remove all addresses from the table
        /// </summary>
        public void DeleteSampleAddressDataSet()
        {
            G3Address.ToList().ForEach(item => G3Address.Remove(item));
            SaveChanges();
        }

        public virtual DbSet<G3Address> G3Address { get; set; }
        public virtual DbSet<G3BankAccount> G3BankAccount { get; set; }
        public virtual DbSet<G3Lease> G3Lease { get; set; }
        public virtual DbSet<G3MonthlyPaid> G3MonthlyPaid { get; set; }
        public virtual DbSet<G3MonthlyPayment> G3MonthlyPayment { get; set; }
        public virtual DbSet<G3Owner> G3Owner { get; set; }
        public virtual DbSet<G3Property> G3Property { get; set; }
        public virtual DbSet<G3Service> G3Service { get; set; }
        public virtual DbSet<G3Tenant> G3Tenant { get; set; }
        public virtual DbSet<G3Unit> G3Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=134.108.190.89;Initial Catalog=Infosys;Persist Security Info=True;User ID=wkb6;Password=wkb6");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "db_owner");

            modelBuilder.Entity<G3Address>(entity =>
            {
                entity.ToTable("G3_address", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HouseNr).HasColumnName("houseNr");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            modelBuilder.Entity<G3BankAccount>(entity =>
            {
                entity.HasKey(e => e.Iban)
                    .HasName("PK_Bank_Account");

                entity.ToTable("G3_Bank_Account", "dbo");

                entity.Property(e => e.Iban)
                    .HasColumnName("IBAN")
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.TenantId).HasColumnName("tenantId");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.G3BankAccount)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_114");
            });

            modelBuilder.Entity<G3Lease>(entity =>
            {
                entity.ToTable("G3_Lease", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.EndDate)
                    .HasColumnName("endDate")
                    .HasColumnType("date");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TenantId).HasColumnName("tenantId");

                entity.Property(e => e.UnitId).HasColumnName("unitId");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.G3Lease)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_92");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.G3Lease)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_75");
            });

            modelBuilder.Entity<G3MonthlyPaid>(entity =>
            {
                entity.ToTable("G3_Monthly_paid", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnName("IBAN")
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyPaymnetId).HasColumnName("monthlyPaymnetId");

                entity.Property(e => e.PaidDate)
                    .HasColumnName("paidDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IbanNavigation)
                    .WithMany(p => p.G3MonthlyPaid)
                    .HasForeignKey(d => d.Iban)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_165");

                entity.HasOne(d => d.MonthlyPaymnet)
                    .WithMany(p => p.G3MonthlyPaid)
                    .HasForeignKey(d => d.MonthlyPaymnetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_168");
            });

            modelBuilder.Entity<G3MonthlyPayment>(entity =>
            {
                entity.ToTable("G3_Monthly_payment", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.LeaseId).HasColumnName("leaseId");

                entity.Property(e => e.TargetAmount).HasColumnName("targetAmount");

                entity.HasOne(d => d.Lease)
                    .WithMany(p => p.G3MonthlyPayment)
                    .HasForeignKey(d => d.LeaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_137");
            });

            modelBuilder.Entity<G3Owner>(entity =>
            {
                entity.ToTable("G3_Owner", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adressid).HasColumnName("adressid");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Adress)
                    .WithMany(p => p.G3Owner)
                    .HasForeignKey(d => d.Adressid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_57");
            });

            modelBuilder.Entity<G3Property>(entity =>
            {
                entity.ToTable("G3_Property", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdressId).HasColumnName("adressId");

                entity.Property(e => e.OwnerId).HasColumnName("ownerId");

                entity.HasOne(d => d.Adress)
                    .WithMany(p => p.G3Property)
                    .HasForeignKey(d => d.AdressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_63");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.G3Property)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_60");
            });

            modelBuilder.Entity<G3Service>(entity =>
            {
                entity.ToTable("G3_Service", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.DistributionKey).HasColumnName("distributionKey");

                entity.Property(e => e.DueDate)
                    .HasColumnName("dueDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyId).HasColumnName("propertyId");

                entity.Property(e => e.UnitId).HasColumnName("unitId");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.G3Service)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_142");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.G3Service)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_134");
            });

            modelBuilder.Entity<G3Tenant>(entity =>
            {
                entity.ToTable("G3_Tenant", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<G3Unit>(entity =>
            {
                entity.ToTable("G3_Unit", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.PropertyId).HasColumnName("propertyId");

                entity.Property(e => e.ResidentNr).HasColumnName("residentNr");

                entity.Property(e => e.RoomsNr).HasColumnName("roomsNr");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.G3Unit)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_66");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
