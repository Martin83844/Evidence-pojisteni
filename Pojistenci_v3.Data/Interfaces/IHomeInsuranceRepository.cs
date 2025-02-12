using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu pojištění nemovitostí v databázi.
	/// Dědí základní CRUD operace z <see cref="IRepository{T}"/>.
	/// Obsahuje specifické metody pro práci s entitou <see cref="HomeInsurance"/>.
	/// </summary>
	public interface IHomeInsuranceRepository : IRepository<HomeInsurance>
	{
		/// <summary>
		/// Získá všechna pojištění nemovitostí podle ID pojištěného.
		/// </summary>
		/// <param name="insuredId">ID pojištěné osoby, podle kterého se pojištění vyhledávají.</param>
		/// <returns>Seznam pojištění nemovitostí spojených s daným ID pojištěného.</returns>
		Task<IEnumerable<HomeInsurance>> GetByInsuredIdAsync(string insuredId);

		/// <summary>
		/// Získá všechna pojištění nemovitostí podle ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele, podle kterého se pojištění vyhledávají.</param>
		/// <returns>Seznam pojištění nemovitostí spojených s daným ID pojistitele.</returns>
		Task<IEnumerable<HomeInsurance>> GetByInsurerIdAsync(string insurerId);
	}
}
