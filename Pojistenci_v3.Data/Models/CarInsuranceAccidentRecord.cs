using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje záznam o škodě spojený s pojištěním vozidla.
	/// Tato třída dědí základní vlastnosti z třídy <see cref="DamageRecord"/> 
	/// a přidává specifické informace pro nehody vozidel.
	/// </summary>
	public class CarInsuranceAccidentRecord : DamageRecord
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
