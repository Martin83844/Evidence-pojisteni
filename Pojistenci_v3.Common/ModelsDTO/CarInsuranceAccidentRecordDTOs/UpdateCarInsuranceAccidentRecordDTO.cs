using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.CarInsuranceAccidentRecordDTOs
{
	/// <summary>
	/// DTO pro aktualizaci záznamu o nehodě spojené s pojištěním vozidla.
	/// Obsahuje informace, které lze měnit při aktualizaci existujícího záznamu škody spojeného s pojištěním vozidla.
	/// </summary>
	public class UpdateCarInsuranceAccidentRecordDTO
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
		/// Popis poškozených částí vozidla.
		/// </summary>
		[Display(Name = "Poškozené části vozidla")]
		[Required(ErrorMessage = "Poškozené části jsou povinné.")]
		public string DamagedParts { get; set; } = string.Empty;

		/// <summary>
		/// Informace o dalších stranách zapojených do nehody (např. jiná vozidla, osoby).
		/// </summary>
		[Display(Name = "Další zúčastněné strany")]
		public string OtherPartiesInvolved { get; set; } = string.Empty;
	}
}
