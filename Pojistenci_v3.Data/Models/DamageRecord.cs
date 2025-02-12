using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje záznam o škodě spojené s pojištěním.
	/// Obsahuje informace o datu škody, popisu, nákladech a vztahu k pojištění.
	/// </summary>
	public class DamageRecord
	{
		/// <summary>
		/// Unikátní identifikátor záznamu škody.
		/// Pro záznamy škod spojené s pojištěním vozidel je ID generováno pomocí procedury
		/// <c>GenerateCarInsuranceAccidentRecordId</c>.
		/// Pro záznamy škod spojené s pojištěním nemovitostí je ID generováno pomocí procedury
		/// <c>GenerateHomeInsuranceDamageRecordId</c>.
		/// </summary>
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Datum, kdy došlo ke škodě.
		/// </summary>
		[Display(Name = "Datum škody")]
		[Required(ErrorMessage = "Datum škody je povinné.")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Popis škody.
		/// </summary>
		[Display(Name = "Popis škody")]
		[Required(ErrorMessage = "Popis škody je povinný.")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Odhadovaná výše škody v korunách.
		/// </summary>
		[Display(Name = "Odhadovaná výše škody (Kč)")]
		[Range(0, double.MaxValue, ErrorMessage = "Výše škody musí být nezáporné číslo.")]
		[Precision(18, 2)]
		public decimal EstimatedDamageCost { get; set; }

		/// <summary>
		/// Schválená částka náhrady v korunách.
		/// </summary>
		[Display(Name = "Schválená částka náhrady (Kč)")]
		[Range(0, double.MaxValue, ErrorMessage = "Náhrada musí být nezáporné číslo.")]
		[Precision(18, 2)]
		public decimal ApprovedCompensation { get; set; }

		/// <summary>
		/// Identifikátor pojištění, ke kterému je záznam o škodě přiřazen.
		/// </summary>
		[Required]
		public string InsuranceId { get; set; } = string.Empty;

		/// <summary>
		/// Pojištění spojené se záznamem škody.
		/// </summary>
		[Display(Name = "Pojištěný")]
		public Insurance? Insurance { get; set; }
	}
}
