using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs
{
	/// <summary>
	/// DTO pro aktualizaci pojištění nemovitosti.
	/// Obsahuje vlastnosti, které lze měnit při aktualizaci existujícího pojištění nemovitosti.
	/// </summary>
	public class UpdateHomeInsuranceDTO
	{
		/// <summary>
		/// Cena pojištění.
		/// Hodnota musí být větší než 0.
		/// </summary>
		[Required(ErrorMessage = "Cena je povinná.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Cena musí být větší než 0.")]
		public decimal Price { get; set; }

		/// <summary>
		/// Datum, do kterého je pojištění platné.
		/// </summary>
		[Display(Name = "Platnost do")]
		[Required(ErrorMessage = "Datum konce platnosti je povinné.")]
		[DataType(DataType.Date)]
		public DateTime? ValidityUntil { get; set; }

		/// <summary>
		/// Identifikátor pojistníka (pojišťovny).
		/// </summary>
		[Required(ErrorMessage = "Pojistník je povinný.")]
		public string InsurerId { get; set; } = string.Empty;

		/// <summary>
		/// Hodnota nemovitosti v korunách.
		/// </summary>
		[Display(Name = "Hodnota nemovitosti (v Kč)")]
		[Required(ErrorMessage = "Hodnota nemovitosti je povinná.")]
		[Range(0, double.MaxValue, ErrorMessage = "Hodnota nemovitosti musí být nezáporné číslo.")]
		public decimal PropertyValue { get; set; }

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
