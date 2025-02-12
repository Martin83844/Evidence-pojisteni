using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu záznamů o škodách (Damage Records) v databázi.
	/// Dědí základní CRUD operace z <see cref="IRepository{T}"/>.
	/// Obsahuje specifické metody pro práci s entitou <see cref="DamageRecord"/>.
	/// </summary>
	public interface IDamageRecordRepository : IRepository<DamageRecord>
	{
		/// <summary>
		/// Získá všechny záznamy o škodách (Damage Records) spojené s daným pojištěním.
		/// </summary>
		/// <param name="insuranceId">ID pojištění, podle kterého se záznamy o škodách vyhledávají.</param>
		/// <returns>Seznam záznamů o škodách spojených s daným pojištěním.</returns>
		Task<IEnumerable<DamageRecord>> GetByInsuranceIdAsync(string insuranceId);
	}
}
