using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje pojištění vozidla. Dědí základní vlastnosti z třídy <see cref="Insurance"/>.
	/// </summary>
	public class CarInsurance : Insurance
	{
		/// <summary>
		/// Inicializuje novou instanci pojištění vozidla a nastaví typ pojištění na <see cref="InsuranceType.CarInsurance"/>.
		/// </summary>
		public CarInsurance()
		{
			Type = InsuranceType.CarInsurance;
		}

		/// <summary>
		/// Registrační značka (SPZ) vozidla.
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
		/// Typ paliva vozidla (např. benzín, nafta, elektromobil).
		/// </summary>
		[Display(Name = "Typ paliva")]
		[Required(ErrorMessage = "Typ paliva je povinný.")]
		public FuelType FuelType { get; set; }

		/// <summary>
		/// Typ pojištění vozidla (např. povinné ručení, havarijní pojištění).
		/// </summary>
		[Display(Name = "Typ pojištění vozu")]
		public CarInsuranceType CarInsuranceType { get; set; }

		/// <summary>
		/// Jméno majitele vozidla.
		/// </summary>
		[Display(Name = "Majitel vozu")]
		[Required(ErrorMessage = "Majitel vozu je povinný.")]
		public string OwnerName { get; set; } = string.Empty;

		/// <summary>
		/// Kontakt na majitele vozidla.
		/// </summary>
		[Display(Name = "Kontakt na majitele")]
		[RegularExpression(@"^\+420\d{9}$", ErrorMessage = "Telefonní číslo musí být ve formátu +420 ddddddddd.")]
		[Required(ErrorMessage = "Kontakt na mejitele vozu je povinný.")]
		public string OwnerContact { get; set; } = string.Empty;

		/// <summary>
		/// Účel používání vozidla (např. osobní, komerční).
		/// </summary>
		[Display(Name = "Účel vozu")]
		[Required(ErrorMessage = "Účel vozu je povinný.")]
		public UsageType UsageType { get; set; }
	}

	/// <summary>
	/// Typ paliva vozidla.
	/// </summary>
	public enum FuelType
	{
		[Display(Name = "Benzín")]
		Petrol,
		[Display(Name = "Nafta")]
		Diesel,
		[Display(Name = "Elektromobil")]
		Electric,
		Hybrid,
		CNG,
		LPG
	}

	/// <summary>
	/// Typ pojištění vozidla.
	/// </summary>
	public enum CarInsuranceType
	{
		[Display(Name = "Povinné ručení")]
		Liability,
		[Display(Name = "Havarijní pojištění")]
		Collision,
		[Display(Name = "Komplexní pojištění")]
		Comprehensive,
		[Display(Name = "Pojištění proti krádeži")]
		Theft
	}

	/// <summary>
	/// Účel používání vozidla.
	/// </summary>
	public enum UsageType
	{
		[Display(Name = "Osobní")]
		Personal,
		[Display(Name = "Komerční")]
		Commercial
	}
}
