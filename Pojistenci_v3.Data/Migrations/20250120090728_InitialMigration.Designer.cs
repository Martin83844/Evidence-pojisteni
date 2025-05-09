﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pojistenci_v3.Data;

#nullable disable

namespace Pojistenci_v3.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250120090728_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.DamageRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("ApprovedCompensation")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EstimatedDamageCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InsuranceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceId");

                    b.ToTable("DamageRecords", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insurance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InsuredId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InsurerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidityFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidityUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InsuredId");

                    b.HasIndex("InsurerId");

                    b.ToTable("Insurances", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insured", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("City")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR CustomerNumberSequence");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Insured");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insurer", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("City")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Insurer");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.CarInsuranceAccidentRecord", b =>
                {
                    b.HasBaseType("Pojistenci_v3.Data.Models.DamageRecord");

                    b.Property<string>("DamagedParts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherPartiesInvolved")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CarInsuranceAccidentRecords", (string)null);
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.HomeInsuranceDamageRecord", b =>
                {
                    b.HasBaseType("Pojistenci_v3.Data.Models.DamageRecord");

                    b.Property<string>("DamagedPart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeInsuranceId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("HomeInsuranceId");

                    b.ToTable("HomeInsuranceDamageRecords", (string)null);
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.CarInsurance", b =>
                {
                    b.HasBaseType("Pojistenci_v3.Data.Models.Insurance");

                    b.Property<int>("CarInsuranceType")
                        .HasColumnType("int");

                    b.Property<int>("EngineSize")
                        .HasColumnType("int");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsageType")
                        .HasColumnType("int");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfManufacture")
                        .HasColumnType("int");

                    b.ToTable("CarInsurances", (string)null);
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.HomeInsurance", b =>
                {
                    b.HasBaseType("Pojistenci_v3.Data.Models.Insurance");

                    b.Property<int>("HomeInsuranceType")
                        .HasColumnType("int");

                    b.Property<string>("OwnerContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyArea")
                        .HasColumnType("int");

                    b.Property<int>("PropertyType")
                        .HasColumnType("int");

                    b.Property<decimal>("PropertyValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("YearBuilt")
                        .HasColumnType("int");

                    b.ToTable("HomeInsurances", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.DamageRecord", b =>
                {
                    b.HasOne("Pojistenci_v3.Data.Models.Insurance", "Insurance")
                        .WithMany("DamageRecords")
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Insurance");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insurance", b =>
                {
                    b.HasOne("Pojistenci_v3.Data.Models.Insured", "Insured")
                        .WithMany("Insurances")
                        .HasForeignKey("InsuredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pojistenci_v3.Data.Models.Insurer", "Insurer")
                        .WithMany("ManagedInsurances")
                        .HasForeignKey("InsurerId");

                    b.Navigation("Insured");

                    b.Navigation("Insurer");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.CarInsuranceAccidentRecord", b =>
                {
                    b.HasOne("Pojistenci_v3.Data.Models.DamageRecord", null)
                        .WithOne()
                        .HasForeignKey("Pojistenci_v3.Data.Models.CarInsuranceAccidentRecord", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.HomeInsuranceDamageRecord", b =>
                {
                    b.HasOne("Pojistenci_v3.Data.Models.HomeInsurance", null)
                        .WithMany("DamageHistory")
                        .HasForeignKey("HomeInsuranceId");

                    b.HasOne("Pojistenci_v3.Data.Models.DamageRecord", null)
                        .WithOne()
                        .HasForeignKey("Pojistenci_v3.Data.Models.HomeInsuranceDamageRecord", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.CarInsurance", b =>
                {
                    b.HasOne("Pojistenci_v3.Data.Models.Insurance", null)
                        .WithOne()
                        .HasForeignKey("Pojistenci_v3.Data.Models.CarInsurance", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.HomeInsurance", b =>
                {
                    b.HasOne("Pojistenci_v3.Data.Models.Insurance", null)
                        .WithOne()
                        .HasForeignKey("Pojistenci_v3.Data.Models.HomeInsurance", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insurance", b =>
                {
                    b.Navigation("DamageRecords");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insured", b =>
                {
                    b.Navigation("Insurances");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.Insurer", b =>
                {
                    b.Navigation("ManagedInsurances");
                });

            modelBuilder.Entity("Pojistenci_v3.Data.Models.HomeInsurance", b =>
                {
                    b.Navigation("DamageHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
