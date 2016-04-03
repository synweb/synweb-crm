namespace SynWebCRM.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<EstimateItem> EstimateItems { get; set; }
        public virtual DbSet<Website> Websites { get; set; }
        public virtual DbSet<DealState> DealStates { get; set; }
        public virtual DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
                

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.VkId)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Deals)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Estimates)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Websites)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<DealState>()
            //     .HasMany(x => x.Deals)
            //     .WithRequired(x => x.DealState)
            //     .HasForeignKey(x => x.DealStateId)
            //     .WillCascadeOnDelete(false);
        }
    }
}
