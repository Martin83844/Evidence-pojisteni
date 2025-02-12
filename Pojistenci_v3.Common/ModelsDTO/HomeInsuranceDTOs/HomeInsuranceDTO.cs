using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs
{
	/// <summary>
	/// DTO pro pojištění nemovitosti.
	/// Dědí základní vlastnosti z <see cref="InsuranceDTO"/>.
	/// </summary>
	public class HomeInsuranceDTO : InsuranceDTO
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
        public string PropertyType { get; set; } = string.Empty; // Můžeme použít string pro enum hodnotu.

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
		public string HomeInsuranceType { get; set; } = string.Empty; // Pro enum hodnotu.

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
    }
}
