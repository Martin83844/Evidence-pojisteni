using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.CarInsuranceAccidentRecordDTOs
{
	/// <summary>
	/// DTO pro záznam o nehodě spojené s pojištěním vozidla.
	/// Dědí základní vlastnosti z <see cref="DamageRecordDTO"/> a přidává specifické informace o nehodách vozidel.
	/// </summary>
	public class CarInsuranceAccidentRecordDTO : DamageRecordDTO
	{
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
