using Microsoft.EntityFrameworkCore;
using SynWebCRM.DataMigrator.Models;

namespace SynWebCRM.DataMigrator.MSSQL
{

    public partial class Model : DbContext
    {
        private string _connectionString;

        public Model(string connectionString)
        {

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<EstimateItem> EstimateItems { get; set; }
        public virtual DbSet<Website> Websites { get; set; }
        public virtual DbSet<DealState> DealStates { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.VkId)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Deals)
                .WithOne(e => e.Customer);

            modelBuilder.Entity<Deal>()
                .HasMany(e => e.Estimates)
                .WithOne(e => e.Deal);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Websites)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.OwnerId);

            //modelBuilder.Entity<DealState>()
            //     .HasMany(x => x.Deals)
            //     .WithRequired(x => x.DealState)
            //     .HasForeignKey(x => x.DealStateId)
            //     .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Estimate>()
            //    .HasMany(e => e.Items)
            //    .WithRequired(x => x.Estimate)
            //    //.Map(x =>
            //    //{
            //    //    x.MapKey("EstimateId");
            //    //    x.ToTable("Estimate");
            //    //})
            //    //.WillCascadeOnDelete(true);
            //    .HasForeignKey(x => x.EstimateId);


            modelBuilder.Entity<ServiceType>()
                .HasMany(e => e.Deals)
                .WithOne(e => e.ServiceType)
                .HasForeignKey(e => e.ServiceTypeId);

            //modelBuilder.Entity<Customer>()
            //            .HasMany<Note>(s => s.Notes)
            //            .WithMany(c => c.Customers)
            //            .Map(cs =>
            //            {
            //                cs.MapLeftKey("CustomerId");
            //                cs.MapRightKey("NoteId");
            //                cs.ToTable("Note_Customer");
            //            });



            //modelBuilder.Entity<Deal>()
            //            .HasMany<Note>(s => s.Notes)
            //            .WithMany(c => c.Deals)
            //            .Map(cs =>
            //            {
            //                cs.MapLeftKey("DealId");
            //                cs.MapRightKey("NoteId");
            //                cs.ToTable("Note_Deal");
            //            });
        }
    }
}
