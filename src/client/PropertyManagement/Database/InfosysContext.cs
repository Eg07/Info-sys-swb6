using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Database.DataModels;

// ReSharper disable UnusedMember.Local
namespace PropertyManagement.Database
{
    public class InfosysContext : DbContext
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

        /// <summary>
        /// Resetes the identity seed of a table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="seed">The value to reset the seed to.</param>
        public void ResetIdentitySeed(string tableName, int seed = 0)
        {
            Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT ('[dbo].[{tableName}]', RESEED, {seed})");
        }

        private void CreateCompleteDataSet()
        {
            CreateAddressData();
            CreateOwnerData();
            CreatePropertyData();
            CreateTenantData();
            CreateLeaseData();
        }

        /// <summary>
        /// Creates some sample data for address table
        /// </summary>
        private void CreateAddressData()
        {
            // references should be added later
            var addressExample1 = new G3Address
            {
                City = "Esslingen",
                HouseNr = 101,
                State = "Baden-Württemberg",
                Street = "Flandernstraße",
                Zip = 73728,
                G3Property = null,
                G3Owner = null
            };
            var addressExample2 = new G3Address
            {
                City = "Esslingen",
                HouseNr = 2,
                State = "Baden-Württemberg",
                Street = "Bahnhofstraße",
                Zip = 73733,
                G3Property = null,
                G3Owner = null
            };
            // Add address
            G3Address.Add(addressExample1);
            G3Address.Add(addressExample2);
            SaveChanges();
        }

        private void CreateOwnerData()
        {
            var ownerExample1 = new G3Owner()
            {
                Adressid = 1,
                FirstName = "Dream",
                LastName = "House",
                G3Property = null
            };

            G3Owner.Add(ownerExample1);
            SaveChanges();
        }

        private void CreatePropertyData()
        {
            var propertyExample1 = new G3Property()
            {
                AdressId = 2,
                OwnerId = 1,
                Adress = null,
                G3OperatingCosts = null,
                G3Unit = null,
                Owner = null
            };

            G3Property.Add(propertyExample1);
            SaveChanges();
        }

        private void CreateTenantData()
        {
            var tenants = new List<G3BankAccount>
            {
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Bella",
                        LastName = "Huber"
                    },
                    Iban = "DE16500105174657255572"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Naomi",
                        LastName = "Mayer"
                    },
                    Iban = "DE08500105177413815781"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Fabian",
                        LastName = "Müller"
                    },
                    Iban = "DE20500105179486279347"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Franziska",
                        LastName = "Häfele"
                    },
                    Iban = "DE60500105178948554471"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Olaf",
                        LastName = "Haug"
                    },
                    Iban = "DE60500105177453325176"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Brenda",
                        LastName = "Barni"
                    },
                    Iban = "DE14500105171982429198"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Ali",
                        LastName = "Strobel"
                    },
                    Iban = "DE78500105172496167788"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Todd",
                        LastName = "Koslowski"
                    },
                    Iban = "DE04500105171615323857"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Aedan",
                        LastName = "Mann"
                    },
                    Iban = "DE20500105174979224981"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Kaidan",
                        LastName = "Riester"
                    },
                    Iban = "DE49500105174141644591"
                },
                new G3BankAccount()
                {
                    G3Payments = null,
                    Tenant = new G3Tenant()
                    {
                        FirstName = "Ibraheem",
                        LastName = "Stich"
                    },
                    Iban = "DE45500105172395899288"
                }
            };

            AddRange(tenants);
            SaveChanges();
        }

        private void CreateLeaseData()
        {
            var leases = new List<G3Lease>
            {
                new G3Lease()
                {
                    Cost = 310,
                    UtilitiesCost = 50,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2021, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Häfele"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 2,
                        Area = 51.7,
                        Floor = 1,
                        ResidentNr = 1,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 350,
                    UtilitiesCost = 62.50,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2022, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Haug"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 5,
                        Area = 91.2,
                        Floor = 2,
                        ResidentNr = 2,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 400,
                    UtilitiesCost = 88.30,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2024, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Koslowski"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 4,
                        Area = 55.4,
                        Floor = 3,
                        ResidentNr = 2,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 200,
                    UtilitiesCost = 30,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2022, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Barni"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 3,
                        Area = 49.9,
                        Floor = 2,
                        ResidentNr = 3,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 300,
                    UtilitiesCost = 50,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2023, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Mann"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 4,
                        Area = 82.6,
                        Floor = 4,
                        ResidentNr = 4,
                        PropertyId = 1
                    },
                },
                new G3Lease()
                {
                    Cost = 330,
                    UtilitiesCost = 40,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2021, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Riester"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 5,
                        Area = 53.5,
                        Floor = 4,
                        ResidentNr = 2,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 340,
                    UtilitiesCost = 40,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2024, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Stich"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 2,
                        Area = 56.7,
                        Floor = 5,
                        ResidentNr = 1,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 400,
                    UtilitiesCost = 80,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2022, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Mayer"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 4,
                        Area = 51.2,
                        Floor = 0,
                        ResidentNr = 2,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 380,
                    UtilitiesCost = 45,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2023, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Huber"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 4,
                        Area = 51.2,
                        Floor = 0,
                        ResidentNr = 2,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 400,
                    UtilitiesCost = 60,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2024, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Strobel"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 3,
                        Area = 53.8,
                        Floor = 3,
                        ResidentNr = 1,
                        PropertyId = 1
                    }
                },
                new G3Lease()
                {
                    Cost = 350,
                    UtilitiesCost = 40,
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2021, 12, 31),
                    Tenant = G3Tenant.First(tenant => tenant.LastName == "Müller"),
                    Unit = new G3Unit()
                    {
                        RoomsNr = 3,
                        Area = 62.7,
                        Floor = 1,
                        ResidentNr = 4,
                        PropertyId = 1
                    },
                },
        };

            G3Lease.AddRange(leases);
            SaveChanges();
        }

        /// <summary>
        /// Remove all addresses from the table
        /// </summary>
        public void DeleteSampleAddressDataSet<T>(DbSet<T> set) where T : class
        {
            try
            {
                set.ToList().ForEach(entry => set.Remove(entry));
                SaveChanges();
            }
            catch (DbUpdateException e)
            {
                // TODO: if used by front end print out error message (snackbar)
                Debug.WriteLine(e);
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

                entity.Property(e => e.Association)
                    .IsRequired()
                    .HasColumnName("association")
                    .HasMaxLength(100)
                    .IsUnicode(false);

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
                    .HasConstraintName("FK_142");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.G3OperatingCosts)
                    .HasForeignKey(d => d.UnitId)
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

                entity.Property(e => e.Association)
                    .IsRequired()
                    .HasColumnName("association")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BookingDate)
                    .HasColumnName("bookingDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Iban)
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
                    .HasConstraintName("FK_165");

                entity.HasOne(d => d.Lease)
                    .WithMany(p => p.G3Payments)
                    .HasForeignKey(d => d.LeaseId)
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
        }
    }
}
