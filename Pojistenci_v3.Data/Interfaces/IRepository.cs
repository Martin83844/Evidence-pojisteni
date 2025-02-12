namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Obecné rozhraní pro práci s úložištěm dat.
	/// Poskytuje základní CRUD operace pro typ <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Typ entity, se kterou repozitář pracuje.</typeparam>
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// Získá všechny entity z databáze.
		/// </summary>
		/// <returns>Seznam všech entit typu <typeparamref name="T"/>.</returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Získá konkrétní entitu z databáze podle jejího ID.
		/// </summary>
		/// <param name="id">Unikátní identifikátor entity.</param>
		/// <returns>
		/// Entita typu <typeparamref name="T"/> s odpovídajícím ID, 
		/// nebo <c>null</c>, pokud nebyla nalezena.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Vyhazuje výjimku, pokud je parametr <paramref name="id"/> prázdný nebo null.
		/// </exception>
		Task<T?> GetByIdAsync(string id);

		/// <summary>
		/// Přidá novou entitu do databáze.
		/// </summary>
		/// <param name="entity">Entita, která má být přidána.</param>
		/// <returns>Asynchronní úloha reprezentující dokončení operace.</returns>
		/// <exception cref="ArgumentNullException">
		/// Vyhazuje výjimku, pokud je parametr <paramref name="entity"/> null.
		/// </exception>
		Task AddAsync(T entity);

		/// <summary>
		/// Aktualizuje existující entitu v databázi.
		/// </summary>
		/// <param name="entity">Entita s aktualizovanými daty.</param>
		/// <returns>Asynchronní úloha reprezentující dokončení operace.</returns>
		/// <exception cref="ArgumentNullException">
		/// Vyhazuje výjimku, pokud je parametr <paramref name="entity"/> null.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Vyhazuje výjimku, pokud entita neexistuje v kontextu sledování změn.
		/// </exception>
		Task UpdateAsync(T entity);

		/// <summary>
		/// Odstraní entitu z databáze podle jejího ID.
		/// </summary>
		/// <param name="id">Unikátní identifikátor entity, která má být odstraněna.</param>
		/// <returns>
		/// <c>true</c>, pokud byla entita úspěšně odstraněna, 
		/// nebo <c>false</c>, pokud entita neexistuje.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Vyhazuje výjimku, pokud je parametr <paramref name="id"/> prázdný nebo null.
		/// </exception>
		Task<bool> DeleteAsync(string id);

		/// <summary>
		/// Ověří, zda existuje entita s daným ID v databázi.
		/// </summary>
		/// <param name="id">Unikátní identifikátor entity.</param>
		/// <returns>
		/// <c>true</c>, pokud entita s odpovídajícím ID existuje; 
		/// jinak <c>false</c>.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Vyhazuje výjimku, pokud je parametr <paramref name="id"/> prázdný nebo null.
		/// </exception>
		Task<bool> ExistsAsync(string id);
	}
}
