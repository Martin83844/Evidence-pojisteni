using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje pojistitele v systému.
	/// Dědí základní vlastnosti z třídy <see cref="User"/> a přidává kolekci spravovaných pojištění.
	/// </summary>
	public class Insurer : User
	{
		/// <summary>
		/// Interní kolekce spravovaných pojištění. Pouze pro interní úpravy.
		/// </summary>
		private readonly List<Insurance> _managedInsurances = new List<Insurance>();

		/// <summary>
		/// Kolekce pojištění, která pojistitel spravuje.
		/// Tato kolekce je pouze pro čtení a nemůže být upravována přímo mimo třídu.
		/// </summary>
		[Display(Name = "Spravované ojistky")]
		public IReadOnlyCollection<Insurance> ManagedInsurances => _managedInsurances.AsReadOnly();
	}
}
