using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDamageRecordDTOs
{
	/// <summary>
	/// DTO pro záznam o škodě na nemovitosti.
	/// Dědí základní vlastnosti z <see cref="DamageRecordDTO"/> a přidává specifické informace o poškozených částech nemovitosti.
	/// </summary>
	public class HomeInsuranceDamageRecordDTO : DamageRecordDTO
	{
		/// <summary>
		/// Popis konkrétního poškozeného prvku nemovitosti (např. střecha, okno, fasáda).
		/// </summary>
		[Display(Name = "Poškozený prvek")]
		[Required(ErrorMessage = "Poškozený prvek je povinný.")]
		public string DamagedPart { get; set; } = string.Empty;
	}
}
