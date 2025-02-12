using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO
{
	/// <summary>
	/// DTO pro záznam o škodě.
	/// Obsahuje základní informace o škodě, které mohou být sdíleny mezi různými typy záznamů.
	/// </summary>
	public class DamageRecordDTO
	{
		[Required]
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
		public decimal EstimatedDamageCost { get; set; }

		/// <summary>
		/// Schválená částka náhrady v korunách.
		/// </summary>
		[Display(Name = "Schválená částka náhrady (Kč)")]
		[Range(0, double.MaxValue, ErrorMessage = "Náhrada musí být nezáporné číslo.")]
		public decimal ApprovedCompensation { get; set; }

		/// <summary>
		/// Identifikátor pojištění, ke kterému záznam o škodě patří.
		/// </summary>
		[Required]
		public string InsuranceId { get; set; } = string.Empty;

		/// <summary>
		/// Detailní informace o pojištění, ke kterému je záznam o škodě přiřazen.
		/// </summary>
		[Display(Name = "Pojištěný")]
		public InsuranceDTO? Insurance { get; set; }
	}
}
