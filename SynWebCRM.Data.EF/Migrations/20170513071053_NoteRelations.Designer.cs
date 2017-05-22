using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SynWebCRM.Data.EF.Migrations
{
    [DbContext(typeof(CRMModel))]
    [Migration("20170513071053_NoteRelations")]
    partial class NoteRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

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
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Creator");

                    b.Property<int?>("CustomerId");

                    b.Property<int?>("DealId");

                    b.Property<string>("Text");

                    b.HasKey("NoteId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DealId");

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

            modelBuilder.Entity("SynWebCRM.Web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SynWebCRM.Web.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SynWebCRM.Web.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SynWebCRM.Web.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("SynWebCRM.Contract.Models.Estimate", "Estimate")
                        .WithMany("Items")
                        .HasForeignKey("EstimateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SynWebCRM.Contract.Models.Note", b =>
                {
                    b.HasOne("SynWebCRM.Contract.Models.Customer", "Customer")
                        .WithMany("Notes")
                        .HasForeignKey("CustomerId");

                    b.HasOne("SynWebCRM.Contract.Models.Deal", "Deal")
                        .WithMany("Notes")
                        .HasForeignKey("DealId");
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
