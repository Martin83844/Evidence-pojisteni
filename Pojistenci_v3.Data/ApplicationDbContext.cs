using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data
{
	/// <summary>
	/// Databázový kontext aplikace spravující entity a tabulky databáze.
	/// </summary>
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		/// <summary>
		/// Inicializuje novou instanci databázového kontextu.
		/// </summary>
		/// <param name="options">Možnosti konfigurace databázového kontextu.</param>
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Tabulka pojistitelů.
		/// </summary>
		public DbSet<Insurer> Insurer { get; set; } = default!;

		/// <summary>
		/// Tabulka pojištěných osob.
		/// </summary>
		public DbSet<Insured> Insured { get; set; } = default!;

		/// <summary>
		/// Tabulka obecných pojištění.
		/// </summary>
		public DbSet<Insurance> Insurance { get; set; } = default!;

		/// <summary>
		/// Tabulka pojištění vozidel.
		/// </summary>
		public DbSet<CarInsurance> CarInsurance { get; set; } = default!;

		/// <summary>
		/// Tabulka pojištění domácností.
		/// </summary>
		public DbSet<HomeInsurance> HomeInsurances { get; set; } = default!;

		/// <summary>
		/// Tabulka záznamů o škodách.
		/// </summary>
		public DbSet<DamageRecord> DamageRecord { get; set; } = default!;

		/// <summary>
		/// Tabulka záznamů o škodách na pojištění domácností.
		/// </summary>
		public DbSet<HomeInsuranceDamageRecord> HomeInsuranceDamageRecords { get; set; } = default!;

		/// <summary>
		/// Tabulka záznamů o nehodách vozidel.
		/// </summary>
		public DbSet<CarInsuranceAccidentRecord> CarInsuranceAccidentRecords { get; set; } = default!;

		/// <summary>
		/// Konfiguruje model a vztahy mezi entitami.
		/// </summary>
		/// <param name="modelBuilder">Builder pro konfiguraci modelu.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Mapování tabulek
			modelBuilder.Entity<Insured>().ToTable("Insureds");
			modelBuilder.Entity<Insurer>().ToTable("Insurers");
			modelBuilder.Entity<Insurance>().ToTable("Insurances");
			modelBuilder.Entity<CarInsurance>().ToTable("CarInsurances");
			modelBuilder.Entity<HomeInsurance>().ToTable("HomeInsurances");
			modelBuilder.Entity<DamageRecord>().ToTable("DamageRecords");
			modelBuilder.Entity<CarInsuranceAccidentRecord>().ToTable("CarInsuranceAccidentRecords");
			modelBuilder.Entity<HomeInsuranceDamageRecord>().ToTable("HomeInsuranceDamageRecords");

			// Vztah DamageRecord -> Insurance
			modelBuilder.Entity<DamageRecord>()
				.HasOne(d => d.Insurance)
				.WithMany(i => i.DamageRecords)
				.HasForeignKey(d => d.InsuranceId)
				.OnDelete(DeleteBehavior.Cascade);

			// Mapování dědičnosti TPT
			modelBuilder.Entity<CarInsuranceAccidentRecord>()
				.HasBaseType<DamageRecord>();

			modelBuilder.Entity<HomeInsuranceDamageRecord>()
				.HasBaseType<DamageRecord>();

			// Konfigurace vlastnosti CustomerNumber pro Insured
			modelBuilder.Entity<Insured>()
				.Property(i => i.CustomerNumber)
				.HasDefaultValueSql("NEXT VALUE FOR CustomerNumberSequence")
				.IsRequired();
		}
	}
}