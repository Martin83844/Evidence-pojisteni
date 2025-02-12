using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs
{
	/// <summary>
	/// DTO pro vytvoření pojištění nemovitosti.
	/// Obsahuje data potřebná pro inicializaci nového pojištění nemovitosti.
	/// </summary>
	public class CreateHomeInsuranceDTO
	{
		/// <summary>
		/// Adresa pojištěné nemovitosti.
		/// </summary>
		[Display(Name = "Adresa nemovitosti")]
		[Required(ErrorMessage = "Adresa nemovitosti je povinná.")]
		public string PropertyAddress { get; set; } = string.Empty;

		/// <summary>
		/// Typ nemovitosti (např. dům, byt, komerční budova).
		/// </summary>
		[Display(Name = "Typ nemovitosti")]
		[Required(ErrorMessage = "Typ nemovitosti je povinný.")]
		public string PropertyType { get; set; } = string.Empty;

		/// <summary>
		/// Rok výstavby nemovitosti.
		/// </summary>
		[Display(Name = "Rok výstavby")]
		[Required(ErrorMessage = "Rok výstavby je povinný.")]
		[Range(0, int.MaxValue, ErrorMessage = "Rok výstavby musí být nezáporné číslo.")]
		public int YearBuilt { get; set; }

		/// <summary>
		/// Hodnota nemovitosti v korunách.
		/// </summary>
		[Display(Name = "Hodnota nemovitosti (v Kč)")]
		[Required(ErrorMessage = "Hodnota nemovitosti je povinná.")]
		[Range(0, double.MaxValue, ErrorMessage = "Hodnota nemovitosti musí být nezáporné číslo.")]
		public decimal PropertyValue { get; set; }

		/// <summary>
		/// Plocha nemovitosti v metrech čtverečních.
		/// </summary>
		[Display(Name = "Plocha nemovitosti (m²)")]
		[Range(0, int.MaxValue, ErrorMessage = "Plocha nemovitosti musí být nezáporné číslo.")]
		public int PropertyArea { get; set; }

		/// <summary>
		/// Typ pojištění nemovitosti (např. proti požáru, krádeži).
		/// </summary>
		[Display(Name = "Typ pojištění nemovitosti")]
		[Required(ErrorMessage = "Typ pojištění nemovitosti je povinný.")]
		public string HomeInsuranceType { get; set; } = string.Empty;

		/// <summary>
		/// Jméno vlastníka nemovitosti.
		/// </summary>
		[Display(Name = "Jméno vlastníka")]
		[Required(ErrorMessage = "Jméno vlastníka je povinné.")]
		public string OwnerName { get; set; } = string.Empty;

		/// <summary>
		/// Kontakt na vlastníka nemovitosti.
		/// </summary>
		[Display(Name = "Kontakt na vlastníka")]
		[RegularExpression(@"^\+420\d{9}$", ErrorMessage = "Telefonní číslo musí být ve formátu +420 ddddddddd.")]
		[Required(ErrorMessage = "Kontakt na vlastníka je povinný.")]
		public string OwnerContact { get; set; } = string.Empty;

		/// <summary>
		/// Cena pojištění.
		/// Hodnota musí být větší než 0.
		/// </summary>
		[Required(ErrorMessage = "Cena je povinná.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Cena musí být větší než 0.")]
		public decimal Price { get; set; }

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
		/// Identifikátor pojištěného uživatele.
		/// </summary>
		[Required(ErrorMessage = "Pojištěný je povinný.")]
		public string? InsuredId { get; set; }

		/// <summary>
		/// Identifikátor pojistníka (pojišťovny).
		/// </summary>
		public string? InsurerId { get; set; }
	}
}
