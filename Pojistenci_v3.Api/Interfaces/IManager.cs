namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Obecný interface pro CRUD operace s entitami.
	/// </summary>
	/// <typeparam name="TEntity">Typ entity.</typeparam>
	/// <typeparam name="TDTO">Typ DTO.</typeparam>
	public interface IManager<TEntity, TDTO> where TEntity : class where TDTO : class
	{
		/// <summary>
		/// Získá všechny entity z databáze.
		/// </summary>
		/// <returns>Seznam všech entit jako <typeparamref name="TDTO"/>.</returns>
		Task<IEnumerable<TDTO>> GetAllAsync();

		/// <summary>
		/// Získá entitu podle jejího ID.
		/// </summary>
		/// <param name="id">ID entity.</param>
		/// <returns>Objekt <typeparamref name="TDTO"/> nebo <c>null</c>, pokud entita neexistuje.</returns>
		Task<TDTO?> GetByIdAsync(string id);

		/// <summary>
		/// Aktualizuje existující entitu na základě jejího ID.
		/// </summary>
		/// <param name="id">ID entity, která má být aktualizována.</param>
		/// <param name="dto">Data pro aktualizaci entity.</param>
		/// <returns>True, pokud byla entita úspěšně aktualizována; jinak False.</returns>
		Task<bool> UpdateAsync(string id, TDTO dto);

		/// <summary>
		/// Smaže entitu na základě jejího ID.
		/// </summary>
		/// <param name="id">ID entity, která má být smazána.</param>
		/// <returns>True, pokud byla entita úspěšně smazána; jinak False.</returns>
		Task<bool> DeleteAsync(string id);
	}
}
