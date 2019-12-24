using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PropertyManagement.Database.DataModels
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
        public virtual DbSet<Group2Bill> Group2Bill { get; set; }
        public virtual DbSet<Group2Oc> Group2Oc { get; set; }
        public virtual DbSet<Group2PropertyOwner> Group2PropertyOwner { get; set; }
        public virtual DbSet<Group2Propertys> Group2Propertys { get; set; }
        public virtual DbSet<Group2Tenants> Group2Tenants { get; set; }
        public virtual DbSet<Group2Transactions> Group2Transactions { get; set; }
        public virtual DbSet<Group2Unit> Group2Unit { get; set; }
        public virtual DbSet<Group4BalancesAllMonths> Group4BalancesAllMonths { get; set; }
        public virtual DbSet<Group4BalancesBuilding> Group4BalancesBuilding { get; set; }
        public virtual DbSet<Group4BalancesPivot> Group4BalancesPivot { get; set; }
        public virtual DbSet<Group4BalancesPivotApartmentInfo> Group4BalancesPivotApartmentInfo { get; set; }
        public virtual DbSet<Group4BuildingCostParam> Group4BuildingCostParam { get; set; }
        public virtual DbSet<Group4BuildingsWithBalances> Group4BuildingsWithBalances { get; set; }
        public virtual DbSet<Group4Mahnungen> Group4Mahnungen { get; set; }
        public virtual DbSet<Group4RentVsBalance> Group4RentVsBalance { get; set; }
        public virtual DbSet<Group4ServiceChargeSettlement> Group4ServiceChargeSettlement { get; set; }
        public virtual DbSet<Group6FlatForRent> Group6FlatForRent { get; set; }
        public virtual DbSet<Group6Lease> Group6Lease { get; set; }
        public virtual DbSet<Group6Owner> Group6Owner { get; set; }
        public virtual DbSet<Group6Property> Group6Property { get; set; }
        public virtual DbSet<Group6Tenant> Group6Tenant { get; set; }
        public virtual DbSet<Group6Transactions> Group6Transactions { get; set; }
        public virtual DbSet<Group6UtilityInvoice> Group6UtilityInvoice { get; set; }
        public virtual DbSet<Grp1FbpgBill> Grp1FbpgBill { get; set; }
        public virtual DbSet<Grp1FbpgOperatingCosts> Grp1FbpgOperatingCosts { get; set; }
        public virtual DbSet<Grp1FbpgOwner> Grp1FbpgOwner { get; set; }
        public virtual DbSet<Grp1FbpgPerson> Grp1FbpgPerson { get; set; }
        public virtual DbSet<Grp1FbpgProperty> Grp1FbpgProperty { get; set; }
        public virtual DbSet<Grp1FbpgTenant> Grp1FbpgTenant { get; set; }
        public virtual DbSet<Grp1FbpgTransaction> Grp1FbpgTransaction { get; set; }
        public virtual DbSet<Grp1FbpgUnit> Grp1FbpgUnit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=134.108.190.89;Initial Catalog=Infosys;Persist Security Info=True;User ID=wkb6;Password=wkb6");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "db_owner");

            modelBuilder.Entity<G3Address>(entity =>
            {
                entity.ToTable("G3_address", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.TenantId)
                    .HasName("fkIdx_114");

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

                entity.HasIndex(e => e.TenantId)
                    .HasName("fkIdx_92");

                entity.HasIndex(e => e.UnitId)
                    .HasName("fkIdx_75");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.Iban)
                    .HasName("fkIdx_165");

                entity.HasIndex(e => e.MonthlyPaymnetId)
                    .HasName("fkIdx_168");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.LeaseId)
                    .HasName("fkIdx_137");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.Adressid)
                    .HasName("fkIdx_57");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.AdressId)
                    .HasName("fkIdx_63");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("fkIdx_60");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.PropertyId)
                    .HasName("fkIdx_142");

                entity.HasIndex(e => e.UnitId)
                    .HasName("fkIdx_134");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

                entity.HasIndex(e => e.PropertyId)
                    .HasName("fkIdx_66");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

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

            modelBuilder.Entity<Group2Bill>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__group2_B__11F2FC4A0EFF9050");

                entity.ToTable("group2_Bill");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CD).HasColumnName("c_d");

                entity.Property(e => e.Rent).HasColumnName("rent");

                entity.Property(e => e.TId).HasColumnName("tID");

                entity.Property(e => e.UnitId).HasColumnName("unitID");

                entity.HasOne(d => d.T)
                    .WithMany(p => p.Group2Bill)
                    .HasForeignKey(d => d.TId)
                    .HasConstraintName("FK__group2_Bill__tID__3F7D091C");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Group2Bill)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__group2_Bi__unitI__3E88E4E3");
            });

            modelBuilder.Entity<Group2Oc>(entity =>
            {
                entity.HasKey(e => e.OcId)
                    .HasName("PK__group2_O__59A0EFCA438ED74E");

                entity.ToTable("group2_Oc");

                entity.Property(e => e.OcId)
                    .HasColumnName("OcID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.OcType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TId).HasColumnName("tID");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.Group2Oc)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("FK__group2_Oc__BillI__425975C7");

                entity.HasOne(d => d.T)
                    .WithMany(p => p.Group2Oc)
                    .HasForeignKey(d => d.TId)
                    .HasConstraintName("FK__group2_Oc__tID__434D9A00");
            });

            modelBuilder.Entity<Group2PropertyOwner>(entity =>
            {
                entity.HasKey(e => e.OwnerId)
                    .HasName("PK_PropertyOwner");

                entity.ToTable("group2_PropertyOwner");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("fkIdx_48");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("ownerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo).HasColumnName("phoneNo");

                entity.Property(e => e.PropertyId).HasColumnName("propertyID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Group2PropertyOwner)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OwnerProperty");
            });

            modelBuilder.Entity<Group2Propertys>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK_Propertys");

                entity.ToTable("group2_Propertys");

                entity.Property(e => e.PropertyId)
                    .HasColumnName("propertyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfUnits).HasColumnName("numberOfUnits");
            });

            modelBuilder.Entity<Group2Tenants>(entity =>
            {
                entity.HasKey(e => e.TenantId)
                    .HasName("PK_Tenants");

                entity.ToTable("group2_Tenants");

                entity.HasIndex(e => e.UnitId)
                    .HasName("fkIdx_38");

                entity.Property(e => e.TenantId)
                    .HasColumnName("tenantID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .IsRequired()
                    .HasColumnName("birthday")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo).HasColumnName("phoneNo");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId).HasColumnName("unitID");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Group2Tenants)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tenantUnit");
            });

            modelBuilder.Entity<Group2Transactions>(entity =>
            {
                entity.HasKey(e => e.TId)
                    .HasName("PK_Transactions");

                entity.ToTable("group2_Transactions");

                entity.HasIndex(e => e.TenantId)
                    .HasName("fkIdx_96");

                entity.Property(e => e.TId)
                    .HasColumnName("tID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BegunstigsterZahlungspflichtiger)
                    .IsRequired()
                    .HasColumnName("begunstigster_zahlungspflichtiger")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Betrag).HasColumnName("betrag");

                entity.Property(e => e.BuchungsTag)
                    .IsRequired()
                    .HasColumnName("buchungsTag")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BuchungsText)
                    .IsRequired()
                    .HasColumnName("buchungsText")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantId).HasColumnName("tenantID");

                entity.Property(e => e.ValutaDatum)
                    .IsRequired()
                    .HasColumnName("valutaDatum")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Group2Transactions)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transTenants");
            });

            modelBuilder.Entity<Group2Unit>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("PK_Unit");

                entity.ToTable("group2_Unit");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("fkIdx_28");

                entity.Property(e => e.UnitId)
                    .HasColumnName("unitID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NumberOfPeople).HasColumnName("numberOfPeople");

                entity.Property(e => e.PropertyId).HasColumnName("propertyID");

                entity.Property(e => e.Rooms).HasColumnName("rooms");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Group2Unit)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_28");
            });

            modelBuilder.Entity<Group4BalancesAllMonths>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_Balances_All_Months");

                entity.Property(e => e.ApartmentId).HasColumnName("apartment_ID");

                entity.Property(e => e.BuildingId).HasColumnName("building_ID");

                entity.Property(e => e.RentalContractId).HasColumnName("rental_contract_ID");
            });

            modelBuilder.Entity<Group4BalancesBuilding>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_Balances_Building");

                entity.Property(e => e.AddressExtension)
                    .HasColumnName("address_extension")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BalMonth).HasColumnName("bal_month");

                entity.Property(e => e.BalYear).HasColumnName("bal_year");

                entity.Property(e => e.BuildingId).HasColumnName("building_ID");

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.NkAnteil).HasColumnName("nk_anteil");

                entity.Property(e => e.PersonId).HasColumnName("person_ID");

                entity.Property(e => e.Rent).HasColumnName("rent");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Group4BalancesPivot>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_Balances_Pivot");

                entity.Property(e => e.BalYear).HasColumnName("bal_year");

                entity.Property(e => e.SortColumn).HasColumnName("sort_column");
            });

            modelBuilder.Entity<Group4BalancesPivotApartmentInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_Balances_Pivot_Apartment_Info");

                entity.Property(e => e.Barni)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Haug)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mayer)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mueller)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group4BuildingCostParam>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_Building_Cost_Param");

                entity.Property(e => e.Ort)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Straße)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group4BuildingsWithBalances>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_BuildingsWithBalances");

                entity.Property(e => e.BuildingId)
                    .HasColumnName("building_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("city_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SquareMeters).HasColumnName("square_meters");

                entity.Property(e => e.SquareMetersOutside).HasColumnName("square_meters_outside");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasColumnName("street_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StreetNumber).HasColumnName("street_number");

                entity.Property(e => e.StreetNumberExtension)
                    .IsRequired()
                    .HasColumnName("street_number_extension")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode).HasColumnName("zipcode");
            });

            modelBuilder.Entity<Group4Mahnungen>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_Mahnungen");

                entity.Property(e => e.ApartmentId).HasColumnName("Apartment ID");

                entity.Property(e => e.Hinweis)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group4RentVsBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_RentVsBalance");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SaldoAkkumuliert).HasColumnName("Saldo akkumuliert");

                entity.Property(e => e.SaldoMonat).HasColumnName("Saldo Monat");
            });

            modelBuilder.Entity<Group4ServiceChargeSettlement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Group4_ServiceChargeSettlement");

                entity.Property(e => e.GartenpflegeQm).HasColumnName("Gartenpflege / qm");

                entity.Property(e => e.GrundsteuerQm).HasColumnName("Grundsteuer / qm");

                entity.Property(e => e.HeizungPerson).HasColumnName("Heizung / Person");

                entity.Property(e => e.StraßenreinigungQm).HasColumnName("Straßenreinigung / qm");

                entity.Property(e => e.SummeNichtInMiete).HasColumnName("Summe nicht in Miete");

                entity.Property(e => e.VersicherungQm).HasColumnName("Versicherung / qm");

                entity.Property(e => e.WasserAllgStrom).HasColumnName("Wasser+Allg.Strom");

                entity.Property(e => e.WasserAllgStromPerson).HasColumnName("Wasser+Allg.Strom / Person");
            });

            modelBuilder.Entity<Group6FlatForRent>(entity =>
            {
                entity.HasKey(e => e.FlatId)
                    .HasName("PK_flatForRent");

                entity.ToTable("group6_flatForRent");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("fkIdx_41");

                entity.HasIndex(e => e.TenantId)
                    .HasName("fkIdx_34");

                entity.Property(e => e.FlatId)
                    .HasColumnName("flat_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Floor)
                    .IsRequired()
                    .HasColumnName("floor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NrOfResidents).HasColumnName("nrOfResidents");

                entity.Property(e => e.NrOfRooms).HasColumnName("nrOfRooms");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.Space).HasColumnName("space");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Group6FlatForRent)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_41");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Group6FlatForRent)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_34");
            });

            modelBuilder.Entity<Group6Lease>(entity =>
            {
                entity.HasKey(e => e.LeaseId)
                    .HasName("PK_lease");

                entity.ToTable("group6_lease");

                entity.HasIndex(e => e.TenantId)
                    .HasName("fkIdx_48");

                entity.Property(e => e.LeaseId)
                    .HasColumnName("lease_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Deposit).HasColumnName("deposit");

                entity.Property(e => e.PaymentStatus).HasColumnName("paymentStatus");

                entity.Property(e => e.RentEnd)
                    .HasColumnName("rentEnd")
                    .HasColumnType("date");

                entity.Property(e => e.RentStart)
                    .HasColumnName("rentStart")
                    .HasColumnType("date");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Group6Lease)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_48");
            });

            modelBuilder.Entity<Group6Owner>(entity =>
            {
                entity.HasKey(e => e.OwnerId)
                    .HasName("PK_owner");

                entity.ToTable("group6_owner");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("owner_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Prename)
                    .IsRequired()
                    .HasColumnName("prename")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group6Property>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK_property");

                entity.ToTable("group6_property");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("fkIdx_24");

                entity.Property(e => e.PropertyId)
                    .HasColumnName("property_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Group6Property)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_24");
            });

            modelBuilder.Entity<Group6Tenant>(entity =>
            {
                entity.HasKey(e => e.TenantId)
                    .HasName("PK_tenant");

                entity.ToTable("group6_tenant");

                entity.Property(e => e.TenantId)
                    .HasColumnName("tenant_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Prename)
                    .IsRequired()
                    .HasColumnName("prename")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group6Transactions>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("group6_transactions");

                entity.HasIndex(e => e.FlatId)
                    .HasName("fkIdx_61");

                entity.HasIndex(e => e.TenantId)
                    .HasName("fkIdx_64");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.FlatId).HasColumnName("flat_id");

                entity.Property(e => e.Payer)
                    .IsRequired()
                    .HasColumnName("payer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Receiver)
                    .IsRequired()
                    .HasColumnName("receiver")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.Usage)
                    .IsRequired()
                    .HasColumnName("usage")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValutaDate)
                    .HasColumnName("valutaDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Flat)
                    .WithMany(p => p.Group6Transactions)
                    .HasForeignKey(d => d.FlatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_61");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Group6Transactions)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_64");
            });

            modelBuilder.Entity<Group6UtilityInvoice>(entity =>
            {
                entity.HasKey(e => e.UtilityInvoiceId)
                    .HasName("PK_utilityInvoice");

                entity.ToTable("group6_utilityInvoice");

                entity.HasIndex(e => e.FlatId)
                    .HasName("fkIdx_79");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("fkIdx_76");

                entity.Property(e => e.UtilityInvoiceId)
                    .HasColumnName("utility_invoice_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("money");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlatId).HasColumnName("flat_id");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.HasOne(d => d.Flat)
                    .WithMany(p => p.Group6UtilityInvoice)
                    .HasForeignKey(d => d.FlatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_79");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Group6UtilityInvoice)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_76");
            });

            modelBuilder.Entity<Grp1FbpgBill>(entity =>
            {
                entity.HasKey(e => e.PkInvoiceNumber)
                    .HasName("PK_grp1_Bill");

                entity.ToTable("grp1_FBPG_Bill");

                entity.HasIndex(e => e.FkUnit)
                    .HasName("fkIdx_147");

                entity.HasIndex(e => e.PkId)
                    .HasName("fkIdx_295");

                entity.Property(e => e.PkInvoiceNumber)
                    .HasColumnName("PK_InvoiceNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DueDate)
                    .HasColumnName("dueDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkUnit).HasColumnName("FK_Unit");

                entity.Property(e => e.InvoiceItem)
                    .IsRequired()
                    .HasColumnName("invoiceItem")
                    .HasColumnType("text");

                entity.Property(e => e.PayedAmount)
                    .HasColumnName("payedAmount")
                    .HasColumnType("money");

                entity.Property(e => e.PkId).HasColumnName("PK_ID");

                entity.HasOne(d => d.FkUnitNavigation)
                    .WithMany(p => p.Grp1FbpgBill)
                    .HasForeignKey(d => d.FkUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Unit_Bill");

                entity.HasOne(d => d.Pk)
                    .WithMany(p => p.Grp1FbpgBill)
                    .HasForeignKey(d => d.PkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tenant_Bill");
            });

            modelBuilder.Entity<Grp1FbpgOperatingCosts>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK_grp1_OperatingCosts");

                entity.ToTable("grp1_FBPG_OperatingCosts");

                entity.HasIndex(e => e.FkProperty)
                    .HasName("fkIdx_150");

                entity.Property(e => e.PkId)
                    .HasColumnName("PK_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AllocationType)
                    .IsRequired()
                    .HasColumnName("allocationType")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.FkProperty).HasColumnName("FK_Property");

                entity.Property(e => e.InvoiceItem)
                    .IsRequired()
                    .HasColumnName("invoiceItem")
                    .HasColumnType("text");

                entity.HasOne(d => d.FkPropertyNavigation)
                    .WithMany(p => p.Grp1FbpgOperatingCosts)
                    .HasForeignKey(d => d.FkProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_OperatingCosts");
            });

            modelBuilder.Entity<Grp1FbpgOwner>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK_grp1_Owner");

                entity.ToTable("grp1_FBPG_Owner");

                entity.HasIndex(e => e.PkId)
                    .HasName("fkIdx_313");

                entity.Property(e => e.PkId)
                    .HasColumnName("PK_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pk)
                    .WithOne(p => p.Grp1FbpgOwner)
                    .HasForeignKey<Grp1FbpgOwner>(d => d.PkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Person_Owner");
            });

            modelBuilder.Entity<Grp1FbpgPerson>(entity =>
            {
                entity.HasKey(e => e.PkId);

                entity.ToTable("grp1_FBPG_Person");

                entity.Property(e => e.PkId)
                    .HasColumnName("PK_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasColumnName("telephoneNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grp1FbpgProperty>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK_grp1_property");

                entity.ToTable("grp1_FBPG_Property");

                entity.HasIndex(e => e.PkId1)
                    .HasName("fkIdx_300");

                entity.Property(e => e.PkId)
                    .HasColumnName("PK_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PkId1).HasColumnName("PK_ID_1");

                entity.HasOne(d => d.PkId1Navigation)
                    .WithMany(p => p.Grp1FbpgProperty)
                    .HasForeignKey(d => d.PkId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Owner_Property");
            });

            modelBuilder.Entity<Grp1FbpgTenant>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK_grp1_Tenant");

                entity.ToTable("grp1_FBPG_Tenant");

                entity.HasIndex(e => e.FkUnit)
                    .HasName("fkIdx_159");

                entity.HasIndex(e => e.PkId)
                    .HasName("fkIdx_317");

                entity.Property(e => e.PkId)
                    .HasColumnName("PK_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkUnit).HasColumnName("FK_Unit");

                entity.Property(e => e.MoveInDate)
                    .HasColumnName("moveInDate")
                    .HasColumnType("date");

                entity.Property(e => e.MoveOutDate)
                    .HasColumnName("moveOutDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.FkUnitNavigation)
                    .WithMany(p => p.Grp1FbpgTenant)
                    .HasForeignKey(d => d.FkUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Unit_Tenant");

                entity.HasOne(d => d.Pk)
                    .WithOne(p => p.Grp1FbpgTenant)
                    .HasForeignKey<Grp1FbpgTenant>(d => d.PkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Person_Tenant");
            });

            modelBuilder.Entity<Grp1FbpgTransaction>(entity =>
            {
                entity.HasKey(e => e.PkReferenceNumber)
                    .HasName("PK_grp1_Transaction");

                entity.ToTable("grp1_FBPG_Transaction");

                entity.HasIndex(e => e.FkBill)
                    .HasName("fkIdx_192");

                entity.HasIndex(e => e.FkOperatingCosts)
                    .HasName("fkIdx_308");

                entity.HasIndex(e => e.PkId)
                    .HasName("fkIdx_305");

                entity.Property(e => e.PkReferenceNumber)
                    .HasColumnName("PK_ReferenceNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.BookingDate)
                    .HasColumnName("bookingDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkBill).HasColumnName("FK_Bill");

                entity.Property(e => e.FkOperatingCosts).HasColumnName("FK_OperatingCosts");

                entity.Property(e => e.PkId).HasColumnName("PK_ID");

                entity.Property(e => e.PostingText)
                    .IsRequired()
                    .HasColumnName("postingText")
                    .HasColumnType("text");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasColumnName("purpose")
                    .HasColumnType("text");

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasColumnName("recipient")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ValueDate)
                    .HasColumnName("valueDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.FkBillNavigation)
                    .WithMany(p => p.Grp1FbpgTransaction)
                    .HasForeignKey(d => d.FkBill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Transaction");

                entity.HasOne(d => d.FkOperatingCostsNavigation)
                    .WithMany(p => p.Grp1FbpgTransaction)
                    .HasForeignKey(d => d.FkOperatingCosts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperatingCosts_Transaction");

                entity.HasOne(d => d.Pk)
                    .WithMany(p => p.Grp1FbpgTransaction)
                    .HasForeignKey(d => d.PkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tenant_Transaction");
            });

            modelBuilder.Entity<Grp1FbpgUnit>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK_grp1_Unit");

                entity.ToTable("grp1_FBPG_Unit");

                entity.HasIndex(e => e.FkProperty)
                    .HasName("fkIdx_116");

                entity.Property(e => e.PkId)
                    .HasColumnName("PK_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkProperty).HasColumnName("FK_Property");

                entity.Property(e => e.Floor)
                    .IsRequired()
                    .HasColumnName("floor")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Rent).HasColumnName("rent");

                entity.Property(e => e.Rooms).HasColumnName("rooms");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.FkPropertyNavigation)
                    .WithMany(p => p.Grp1FbpgUnit)
                    .HasForeignKey(d => d.FkProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_Unit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
