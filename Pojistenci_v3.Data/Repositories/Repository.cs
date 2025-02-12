using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Data.Interfaces;

namespace Pojistenci_v3.Data.Repositories
{
	/// <summary>
	/// Obecný repozitář pro správu entit v databázi s podporou základních CRUD operací.
	/// </summary>
	/// <typeparam name="T">Typ entity.</typeparam>
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		/// <summary>
		/// Inicializuje nový repozitář s databázovým kontextem.
		/// </summary>
		/// <param name="context">Databázový kontext.</param>
		public Repository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		/// <summary>
		/// Získá všechny entity.
		/// </summary>
		/// <returns>Seznam všech entit typu <typeparamref name="T"/>.</returns>
		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		/// <summary>
		/// Získá konkrétní entitu podle ID.
		/// </summary>
		/// <param name="id">ID entity.</param>
		/// <returns>Entita nebo <c>null</c>, pokud neexistuje.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID prázdné nebo null.</exception>
		public virtual async Task<T?> GetByIdAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(id));
			}
			return await _dbSet.FindAsync(id);
		}

		/// <summary>
		/// Přidá novou entitu.
		/// </summary>
		/// <param name="entity">Nová entita.</param>
		/// <exception cref="ArgumentNullException">Vyhozeno, pokud je entita null.</exception>
		public async Task AddAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity), "Entita nesmí být null.");
			}

			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Aktualizuje existující entitu.
		/// </summary>
		/// <param name="entity">Aktualizovaná entita.</param>
		/// <exception cref="ArgumentNullException">Vyhozeno, pokud je entita null.</exception>
		public virtual async Task UpdateAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity), "Entita nesmí být null.");
			}

			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Odstraní entitu podle ID.
		/// </summary>
		/// <param name="id">ID entity.</param>
		/// <returns>True, pokud byla entita odstraněna, jinak false.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID prázdné nebo null.</exception>
		public async Task<bool> DeleteAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(id));
			}

			var entity = await _dbSet.FindAsync(id);

			if (entity == null)
			{
				return false;
			}

			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		/// <summary>
		/// Ověří, zda existuje entita s daným ID.
		/// </summary>
		/// <param name="id">ID entity.</param>
		/// <returns>True, pokud entita existuje, jinak false.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID prázdné nebo null.</exception>
		public async Task<bool> ExistsAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(id));
			}
			return await _dbSet.FindAsync(id) != null;
		}
	}
}
