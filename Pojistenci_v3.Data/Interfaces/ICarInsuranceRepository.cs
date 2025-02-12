using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu pojištění vozidel (Car Insurance) v databázi.
	/// Dědí základní CRUD operace z <see cref="IRepository{T}"/>.
	/// Obsahuje specifické metody pro práci s entitou <see cref="CarInsurance"/>.
	/// </summary>
	public interface ICarInsuranceRepository : IRepository<CarInsurance>
	{
		/// <summary>
		/// Získá všechna pojištění vozidel (Car Insurance) spojená s daným ID pojištěného.
		/// </summary>
		/// <param name="insuredId">ID pojištěné osoby, podle kterého se pojištění vyhledávají.</param>
		/// <returns>Seznam pojištění vozidel spojených s daným ID pojištěného.</returns>
		Task<IEnumerable<CarInsurance>> GetByInsuredIdAsync(string insuredId);

		/// <summary>
		/// Získá všechna pojištění vozidel (Car Insurance) spojená s daným ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele, podle kterého se pojištění vyhledávají.</param>
		/// <returns>Seznam pojištění vozidel spojených s daným ID pojistitele.</returns>
		Task<IEnumerable<CarInsurance>> GetByInsurerIdAsync(string insurerId);
	}
}
