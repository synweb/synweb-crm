using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;
using SynWebCRM.Data.EF.Models;

namespace SynWebCRM.Data.EF
{
    public partial class CRMModel : IdentityDbContext<ApplicationUser>, IStorageContext
    {
        
        public CRMModel(DbContextOptions<CRMModel> options) : base(options)
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
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.VkId)
                .IsUnicode(false);

            modelBuilder.Entity<Estimate>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Estimate)
                .HasForeignKey(x => x.EstimateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Deals)
                .WithOne(e => e.Customer)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            modelBuilder.Entity<Deal>()
                .HasMany(e => e.Estimates)
                .WithOne(e => e.Deal)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Websites)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.OwnerId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);


            modelBuilder.Entity<ServiceType>()
                .HasMany(e => e.Deals)
                .WithOne(e => e.ServiceType)
                .HasForeignKey(e => e.ServiceTypeId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany<Note>(s => s.Notes)
                .WithOne(c => c.Customer)
                .HasForeignKey("CustomerId");

            modelBuilder.Entity<Deal>()
                        .HasMany<Note>(s => s.Notes)
                        .WithOne(c => c.Deal)
                        .HasForeignKey("DealId");
        }
    }
}
