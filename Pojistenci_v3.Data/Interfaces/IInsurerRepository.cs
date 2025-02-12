using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu pojistitelů v databázi.
	/// Obsahuje základní CRUD operace prostřednictvím <see cref="IRepository{T}"/>.
	/// </summary>
	public interface IInsurerRepository : IRepository<Insurer>
	{
		// Zde lze přidat specifické metody pro práci s pojistiteli, pokud budou potřeba.
	}
}
