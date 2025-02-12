using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu pojištěných osob v databázi.
	/// Dědí základní CRUD operace z <see cref="IRepository{T}"/>.
	/// </summary>
	public interface IInsuredRepository : IRepository<Insured>
	{
		// Zde lze přidat specifické metody pro práci s pojistiteli, pokud budou potřeba.
	}
}
