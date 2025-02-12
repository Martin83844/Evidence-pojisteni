using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.CArInsuranceDTOs
{
	/// <summary>
	/// DTO pro vytvoření nového pojištění vozidla.
	/// Obsahuje informace potřebné pro inicializaci nového pojištění vozidla.
	/// </summary>
	public class CreateCarInsuranceDTO
	{
		/// <summary>
		/// Registrační značka vozidla (SPZ).
		/// </summary>
		[Display(Name = "Registrační značka (SPZ)")]
		[Required(ErrorMessage = "Registrační značka je povinná.")]
		[RegularExpression(@"^[A-Z0-9]{1,8}$", ErrorMessage = "Registrační značka musí obsahovat 1–8 velkých písmen nebo číslic.")]
		public string RegistrationNumber { get; set; } = string.Empty;

		/// <summary>
		/// VIN (Vehicle Identification Number) vozidla.
		/// </summary>
		[Display(Name = "VIN")]
		[Required(ErrorMessage = "VIN (Vehicle Identification Number) je povinný.")]
		[RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN musí obsahovat 17 znaků.")]
		public string VIN { get; set; } = string.Empty;

		/// <summary>
		/// Výrobce vozidla (např. Škoda, BMW, Ford).
		/// </summary>
		[Display(Name = "Výrobce vozidla")]
		[Required(ErrorMessage = "Výrobce vozidla je povinný.")]
		public string Make { get; set; } = string.Empty;

		/// <summary>
		/// Model vozidla (např. Octavia, Fiesta, X5).
		/// </summary>
		[Display(Name = "Model vozidla")]
		[Required(ErrorMessage = "Model vozidla je povinný.")]
		public string Model { get; set; } = string.Empty;

		/// <summary>
		/// Rok výroby vozidla.
		/// </summary>
		[Display(Name = "Rok výroby")]
		[Required(ErrorMessage = "Rok výroby je povinný.")]
		[Range(1886, 2100, ErrorMessage = "Rok výroby musí být mezi 1886 a 2100.")]
		public int YearOfManufacture { get; set; }

		/// <summary>
		/// Objem motoru v centimetrech krychlových (cm³).
		/// </summary>
		[Display(Name = "Objem motoru (cm³)")]
		[Range(0, int.MaxValue, ErrorMessage = "Objem motoru musí být nezáporné číslo.")]
		public int EngineSize { get; set; }

		/// <summary>
		/// Typ paliva vozidla (např. benzín, nafta, elektrický pohon).
		/// </summary>
		[Display(Name = "Typ paliva")]
		[Required(ErrorMessage = "Typ paliva je povinný.")]
		public string FuelType { get; set; } = string.Empty;

		/// <summary>
		/// Typ pojištění vozidla (např. havarijní, povinné ručení).
		/// </summary>
		[Display(Name = "Typ pojištění vozu")]
		[Required(ErrorMessage = "Typ pojištění vozu je povinný.")]
		public string CarInsuranceType { get; set; } = string.Empty;

		/// <summary>
		/// Jméno majitele vozidla.
		/// </summary>
		[Display(Name = "Majitel vozu")]
		[Required(ErrorMessage = "Majitel vozu je povinný.")]
		public string OwnerName { get; set; } = string.Empty;

		/// <summary>
		/// Kontakt na majitele vozidla (např. telefon nebo e-mail).
		/// </summary>
		[Display(Name = "Kontakt na majitele")]
		[RegularExpression(@"^\+420\d{9}$", ErrorMessage = "Telefonní číslo musí být ve formátu +420 ddddddddd.")]
		[Required(ErrorMessage = "Kontakt na mejitele vozu je povinný.")]
		public string OwnerContact { get; set; } = string.Empty;

		/// <summary>
		/// Účel používání vozidla (např. osobní nebo komerční účely).
		/// </summary>
		[Display(Name = "Účel vozu")]
		[Required(ErrorMessage = "Účel vozu je povinný.")]
		public string UsageType { get; set; } = string.Empty;

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
