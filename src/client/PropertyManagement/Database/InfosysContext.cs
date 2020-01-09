using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database.DataModels;

namespace PropertyManagement.Database
{
    public partial class InfosysContext : DbContext
    {
        public virtual DbSet<G3Address> G3Address { get; set; }
        public virtual DbSet<G3BankAccount> G3BankAccount { get; set; }
        public virtual DbSet<G3Lease> G3Lease { get; set; }
        public virtual DbSet<G3OperatingCosts> G3OperatingCosts { get; set; }
        public virtual DbSet<G3Owner> G3Owner { get; set; }
        public virtual DbSet<G3Payments> G3Payments { get; set; }
        public virtual DbSet<G3Property> G3Property { get; set; }
        public virtual DbSet<G3Tenant> G3Tenant { get; set; }
        public virtual DbSet<G3Unit> G3Unit { get; set; }

        public InfosysContext()
        {
        }

        public InfosysContext(DbContextOptions<InfosysContext> options)
            : base(options)
        {
        }

        public bool UpdateDatabaseEntry<T>(T entity) where T : class
        {
            try
            {
                Update(entity);
                SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public bool DeleteDatabaseEntry<T>(T entity) where T : class
        {
            try
            {
                Remove(entity);
                SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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

                entity.Property(e => e.UtilitiesCost).HasColumnName("utilitiesCost");

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

            modelBuilder.Entity<G3OperatingCosts>(entity =>
            {
                entity.ToTable("G3_Operating_Costs", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BookingDate)
                    .HasColumnName("bookingDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionKey).HasColumnName("distributionKey");

                entity.Property(e => e.PropertyId).HasColumnName("propertyId");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId).HasColumnName("unitId");

                entity.Property(e => e.ValutaDate)
                    .HasColumnName("valutaDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.G3OperatingCosts)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_142");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.G3OperatingCosts)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_134");
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

            modelBuilder.Entity<G3Payments>(entity =>
            {
                entity.ToTable("G3_Payments", "dbo");

                entity.HasIndex(e => e.LeaseId)
                    .HasName("fkIdx_106");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BookingDate)
                    .HasColumnName("bookingDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnName("IBAN")
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.LeaseId).HasColumnName("leaseId");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValutaDate)
                    .HasColumnName("valutaDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IbanNavigation)
                    .WithMany(p => p.G3Payments)
                    .HasForeignKey(d => d.Iban)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_165");

                entity.HasOne(d => d.Lease)
                    .WithMany(p => p.G3Payments)
                    .HasForeignKey(d => d.LeaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_106");
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
