using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO
{
	/// <summary>
	/// DTO reprezentující základní informace o pojištění.
	/// Tato třída slouží jako základ pro specifické typy pojištění.
	/// </summary>
	public class InsuranceDTO
	{
		/// <summary>
		/// Unikátní identifikátor pojištění.
		/// </summary>
		[Required]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Cena pojištění.
		/// Hodnota musí být větší než 0.
		/// </summary>
		[Required(ErrorMessage = "Cena je povinná.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Cena musí být větší než 0.")]
		public decimal Price { get; set; }

		/// <summary>
		/// Typ pojištění (např. nemovitost, vozidlo).
		/// </summary>
		[Display(Name = "Typ pojištění")]
		[Required(ErrorMessage = "Typ pojištění je povinný.")]
		public string Type { get; set; } = string.Empty;

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
		public string InsuredId { get; set; } = string.Empty;

		/// <summary>
		/// Detailní informace o pojištěném uživateli.
		/// </summary>
		[Display(Name = "Pojištěný")]
		public InsuredDTO? Insured { get; set; }

		/// <summary>
		/// Identifikátor pojistníka (pojišťovny).
		/// </summary>
		[Required(ErrorMessage = "Pojistník je povinný.")]
		public string InsurerId { get; set; } = string.Empty;

		/// <summary>
		/// Detailní informace o pojistníkovi (pojišťovně).
		/// </summary>
		[Display(Name = "Pojistník")]
		public InsurerDTO? Insurer { get; set; }
	}
}
