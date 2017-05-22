using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SynWebCRM.Data.EF.Migrations
{
    [DbContext(typeof(CRMModel))]
    [Migration("20170504215221_testPG")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("SynWebCRM.Contract.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Creator");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<bool>("NeedsAttention");

                    b.Property<string>("Phone")
                        .HasMaxLength(100);

                    b.Property<int>("Source");

                    b.Property<string>("VkId")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Deal", b =>
                {
                    b.Property<int>("DealId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Creator");

                    b.Property<int>("CustomerId");

                    b.Property<int>("DealStateId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<bool>("NeedsAttention");

                    b.Property<decimal?>("Profit");

                    b.Property<int?>("ServiceTypeId")
                        .IsRequired();

                    b.Property<decimal?>("Sum");

                    b.Property<int>("Type");

                    b.HasKey("DealId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DealStateId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("Deal");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.DealState", b =>
                {
                    b.Property<int>("DealStateId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCompleted");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.HasKey("DealStateId");

                    b.ToTable("DealState");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Creator");

                    b.Property<string>("Function")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("EmployeeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Estimate", b =>
                {
                    b.Property<int>("EstimateId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Creator");

                    b.Property<int>("DealId");

                    b.Property<int>("Discount");

                    b.Property<Guid>("Guid");

                    b.Property<decimal>("HourlyRate");

                    b.Property<decimal>("MonthlyTotal");

                    b.Property<bool>("RequisitesVisible");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<decimal>("Total");

                    b.HasKey("EstimateId");

                    b.HasIndex("DealId");

                    b.ToTable("Estimate");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.EstimateItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<double?>("DevelopmentHours");

                    b.Property<int>("EstimateId");

                    b.Property<bool>("IsOptional");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("PerMonth");

                    b.Property<decimal>("Price");

                    b.Property<int>("SortOrder");

                    b.HasKey("ItemId");

                    b.HasIndex("EstimateId");

                    b.ToTable("EstimateItem");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("EventId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Note", b =>
                {
                    b.Property<int>("NoteId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Creator");

                    b.Property<string>("Text");

                    b.HasKey("NoteId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.ServiceType", b =>
                {
                    b.Property<int>("ServiceTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ServiceTypeId");

                    b.ToTable("ServiceType");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Website", b =>
                {
                    b.Property<int>("WebsiteId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Creator");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("DomainEndingDate");

                    b.Property<DateTime?>("HostingEndingDate");

                    b.Property<decimal?>("HostingPrice");

                    b.Property<bool>("IsActive");

                    b.Property<int>("OwnerId");

                    b.HasKey("WebsiteId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Website");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Deal", b =>
                {
                    b.HasOne("SynWebCRM.Contract.Models.Customer", "Customer")
                        .WithMany("Deals")
                        .HasForeignKey("CustomerId");

                    b.HasOne("SynWebCRM.Contract.Models.DealState", "DealState")
                        .WithMany("Deals")
                        .HasForeignKey("DealStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SynWebCRM.Contract.Models.ServiceType", "ServiceType")
                        .WithMany("Deals")
                        .HasForeignKey("ServiceTypeId");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Estimate", b =>
                {
                    b.HasOne("SynWebCRM.Contract.Models.Deal", "Deal")
                        .WithMany("Estimates")
                        .HasForeignKey("DealId");
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.EstimateItem", b =>
                {
                    b.HasOne("SynWebCRM.Contract.Models.Estimate")
                        .WithMany("Items")
                        .HasForeignKey("EstimateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Note", b =>
                {
                    b.HasOne("SynWebCRM.Contract.Models.Customer", "Customer")
                        .WithMany("Notes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SynWebCRM.Contract.Models.Deal", "Deal")
                        .WithMany("Notes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Website", b =>
                {
                    b.HasOne("SynWebCRM.Contract.Models.Customer", "Customer")
                        .WithMany("Websites")
                        .HasForeignKey("OwnerId");
                });
        }
    }
}
