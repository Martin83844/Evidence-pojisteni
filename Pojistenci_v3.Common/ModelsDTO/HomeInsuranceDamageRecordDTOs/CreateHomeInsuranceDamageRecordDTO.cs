using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDamageRecordDTOs
{
	/// <summary>
	/// DTO pro vytvoření záznamu o škodě na nemovitosti.
	/// Obsahuje informace potřebné pro inicializaci nového záznamu škody spojeného s pojištěním nemovitosti.
	/// </summary>
	public class CreateHomeInsuranceDamageRecordDTO
	{
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
		/// Popis konkrétního poškozeného prvku nemovitosti (např. střecha, okno, fasáda).
		/// </summary>
		[Display(Name = "Poškozený prvek")]
		[Required(ErrorMessage = "Poškozený prvek je povinný.")]
		public string DamagedPart { get; set; } = string.Empty;
	}
}
