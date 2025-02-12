using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.CArInsuranceDTOs
{
	/// <summary>
	/// DTO pro aktualizaci pojištění vozidla.
	/// Obsahuje vlastnosti, které lze měnit při aktualizaci existujícího pojištění vozidla.
	/// </summary>
	public class UpdateCarInsuranceDTO
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
		/// Registrační značka vozidla (SPZ).
		/// </summary>
		[Display(Name = "Registrační značka (SPZ)")]
		[Required(ErrorMessage = "Registrační značka je povinná.")]
		[RegularExpression(@"^[A-Z0-9]{1,8}$", ErrorMessage = "Registrační značka musí obsahovat 1–8 velkých písmen nebo číslic.")]
		public string RegistrationNumber { get; set; } = string.Empty;

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
	}
}
