using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje základní informace o pojištění.
	/// Tato třída zahrnuje obecné vlastnosti platné pro všechny typy pojištění, 
	/// jako je cena, typ pojištění, platnost a související entity.
	/// </summary>
	public class Insurance : IValidatableObject
	{
		/// <summary>
		/// Reprezentuje základní informace o pojištění.
		/// Tato třída zahrnuje obecné vlastnosti platné pro všechny typy pojištění,
		/// jako je cena, typ pojištění, platnost a související entity.
		/// 
		/// ID každého pojištění je unikátní a generováno pomocí uložených procedur v databázi.
		/// - Pro pojištění vozidel (CarInsurance) se používá procedura <c>GenerateCarInsuranceId</c>.
		/// - Pro pojištění nemovitostí (HomeInsurance) se používá procedura <c>GenerateHomeInsuranceId</c>.
		/// Tato ID jsou následně ukládána do vlastnosti <see cref="Id"/>.
		/// </summary>
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Cena pojištění.
		/// Hodnota musí být kladná.
		/// </summary>
		[Display(Name = "Cena")]
		[Range(0, double.MaxValue, ErrorMessage = "Cena musí být kladná.")]
		[Precision(18, 2)]
		public decimal Price { get; set; }

		/// <summary>
		/// Typ pojištění (např. nemovitost nebo vozidlo).
		/// </summary>
		[Display(Name = "Typ pojištění")]
		[Required(ErrorMessage = "Vyberte typ pojištění.")]
		public InsuranceType Type { get; set; }

		/// <summary>
		/// Datum, od kterého je pojištění platné.
		/// </summary>
		[Display(Name = "Platnost od")]
		[Required(ErrorMessage = "Datum začátku platnosti je povinné.")]
		[DataType(DataType.Date)]
		public DateTime ValidityFrom { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Datum, do kterého je pojištění platné.
		/// </summary>
		[Display(Name = "Platnost do")]
		[Required(ErrorMessage = "Datum konce platnosti je povinné.")]
		[DataType(DataType.Date)]
		public DateTime? ValidityUntil { get; set; }

		/// <summary>
		/// Identifikátor pojištěného, ke kterému je toto pojištění přiřazeno.
		/// </summary>
		[Required(ErrorMessage = "Pojištěný je povinný.")]
		public string InsuredId { get; set; } = string.Empty;

		/// <summary>
		/// Entita pojištěného uživatele.
		/// </summary>
		[Display(Name = "Pojištěný")]
		public Insured? Insured { get; set; }

		/// <summary>
		/// Identifikátor pojistníka (pojišťovny), která toto pojištění spravuje.
		/// </summary>
		[Required(ErrorMessage = "Pojistník je povinný.")]
		public string InsurerId { get; set; } = string.Empty;

		/// <summary>
		/// Entita pojistníka (pojišťovny).
		/// </summary>
		[Display(Name = "Pojistník")]
		public Insurer? Insurer { get; set; }

		/// <summary>
		/// Interní kolekce záznamů škod spojených s tímto pojištěním.
		/// Tato kolekce je pouze pro čtení a nelze ji přímo upravovat zvenčí.
		/// </summary>
		private readonly List<DamageRecord> _damageRecords = new List<DamageRecord>();

		/// <summary>
		/// Veřejná pouze pro čtení kolekce záznamů škod spojených s tímto pojištěním.
		/// </summary>
		public IReadOnlyCollection<DamageRecord> DamageRecords => _damageRecords.AsReadOnly();

		/// <summary>
		/// Kontroluje logiku platnosti pojištění.
		/// </summary>
		/// <param name="validationContext">Kontext validace.</param>
		/// <returns>Seznam výsledků validace.</returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ValidityUntil.HasValue && ValidityFrom > ValidityUntil.Value)
			{
				yield return new ValidationResult(
					"Datum konce platnosti musí být pozdější než datum začátku platnosti.",
					new[] { nameof(ValidityUntil) });
			}

			if (ValidityFrom.Date < DateTime.UtcNow.Date)
			{
				yield return new ValidationResult(
					"Datum začátku platnosti nesmí být v minulosti.",
					new[] { nameof(ValidityFrom) });
			}
		}
	}

	/// <summary>
	/// Typy pojištění.
	/// Definuje, zda se jedná o pojištění nemovitosti nebo vozidla.
	/// </summary>
	public enum InsuranceType
	{
		[Display(Name = "Pojištění nemovitosti")]
		HomeInsurance,

		[Display(Name = "Pojištění vozu")]
		CarInsurance
	}
}
