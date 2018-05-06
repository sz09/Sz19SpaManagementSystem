namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SpaManagementEntities : DbContext
    {
        private string _defaultConnection;
        public SpaManagementEntities()
            : base("name=SpaManagementEntities")
        {
            this._defaultConnection = base.Database.Connection.ConnectionString;
        }
        public SpaManagementEntities(string connectionString)
            : base(connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountFor> AccountFors { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<BedName> BedNames { get; set; }
        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<ContactFor> ContactFors { get; set; }
        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<DescriptionFor> DescriptionFors { get; set; }
        public virtual DbSet<DescriptionForType> DescriptionForTypes { get; set; }
        public virtual DbSet<DetailsBill> DetailsBills { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EAddress> EAddresses { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<NotificationDetail> NotificationDetails { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceDesciption> ServiceDesciptions { get; set; }
        public virtual DbSet<ServiceName> ServiceNames { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockName> StockNames { get; set; }
        public virtual DbSet<StockPackage> StockPackages { get; set; }
        public virtual DbSet<StockPackageDetail> StockPackageDetails { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.ForCode)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.ForAccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountFor>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.AccountFor)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.ContactInformations)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bed>()
                .Property(e => e.BedCode)
                .IsFixedLength();

            modelBuilder.Entity<Bed>()
                .HasMany(e => e.BedNames)
                .WithRequired(e => e.Bed)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bed>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Bed)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bills>()
                .Property(e => e.TotalCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bills>()
                .HasMany(e => e.DetailsBills)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactFor>()
                .Property(e => e.StaffOrCustomer)
                .IsFixedLength();

            modelBuilder.Entity<ContactFor>()
                .HasMany(e => e.ContactInformations)
                .WithRequired(e => e.ContactFor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactType>()
                .HasMany(e => e.ContactInformations)
                .WithRequired(e => e.ContactType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Provinces)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerCode)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DescriptionFor>()
                .HasMany(e => e.Descriptions)
                .WithRequired(e => e.DescriptionFor1)
                .HasForeignKey(e => e.DescriptionFor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DescriptionForType>()
                .Property(e => e.TypeName)
                .IsFixedLength();

            modelBuilder.Entity<DescriptionForType>()
                .HasMany(e => e.ServiceDesciptions)
                .WithRequired(e => e.DescriptionForType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EAddress>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<EAddress>()
                .Property(e => e.NumberPhone)
                .IsFixedLength();

            modelBuilder.Entity<EAddress>()
                .HasMany(e => e.ContactInformations)
                .WithRequired(e => e.EAddress)
                .HasForeignKey(e => e.EAdressId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Descriptions)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.ServiceNames)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasMany(e => e.NotificationDetails)
                .WithRequired(e => e.Notification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Salary>()
                .Property(e => e.TotalSalary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .Property(e => e.ServiceCode)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.DetailsBills)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceDesciptions)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceNames)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffCode)
                .IsFixedLength();

            modelBuilder.Entity<Staff>()
                .Property(e => e.IdentifierNumber)
                .IsFixedLength();

            modelBuilder.Entity<Staff>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.DetailsBills)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Salaries)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StockNames)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StockPackageDetails)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StockPackage>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<StockPackage>()
                .HasMany(e => e.StockPackageDetails)
                .WithRequired(e => e.StockPackage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Unit1)
                .HasForeignKey(e => e.Unit)
                .WillCascadeOnDelete(false);
        }
    }
}
