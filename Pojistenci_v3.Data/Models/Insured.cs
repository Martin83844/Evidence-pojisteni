using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje pojištěného uživatele v systému.
	/// Dědí základní vlastnosti z třídy <see cref="User"/> a přidává specifické vlastnosti pro pojištěnce.
	/// </summary>
	public class Insured : User
	{
		/// <summary>
		/// Unikátní číslo zákazníka pojištěného.
		/// Tato hodnota je automaticky generována na úrovni databáze pomocí sekvence <c>CustomerNumberSequence</c>.
		/// </summary>
		[Display(Name = "Číslo zákazníka")]
		public int CustomerNumber { get; set; }

		/// <summary>
		/// Interní kolekce pojistek vlastněných tímto pojištěným.
		/// </summary>
		private readonly List<Insurance> _insurances = new List<Insurance>();

		/// <summary>
		/// Kolekce pojistek vlastněných tímto pojištěným.
		/// Kolekce je pouze pro čtení a nemůže být přímo upravována mimo třídu.
		/// </summary>
		[Display(Name = "Pojistky")]
		public IReadOnlyCollection<Insurance> Insurances => _insurances.AsReadOnly();
	}
}
