using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje záznam o škodě spojený s pojištěním nemovitosti.
	/// Tato třída dědí základní vlastnosti z třídy <see cref="DamageRecord"/> 
	/// a přidává specifické informace pro škody na nemovitostech.
	/// </summary>
	public class HomeInsuranceDamageRecord : DamageRecord
	{
		/// <summary>
		/// Popis konkrétního poškozeného prvku nemovitosti (např. střecha, okno, fasáda).
		/// </summary>
		[Display(Name = "Poškozený prvek")]
		[Required(ErrorMessage = "Poškozený prvek je povinný.")]
		public string DamagedPart { get; set; } = string.Empty;
	}
}
