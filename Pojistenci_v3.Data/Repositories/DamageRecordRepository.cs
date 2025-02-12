using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Repositories
{
	/// <summary>
	/// Repozitář pro správu záznamů o škodách s podporou specifických operací.
	/// </summary>
	public class DamageRecordRepository : Repository<DamageRecord>, IDamageRecordRepository
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Inicializuje nový repozitář pro záznamy o škodách.
		/// </summary>
		/// <param name="context">Databázový kontext.</param>
		public DamageRecordRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		/// <summary>
		/// Získá všechny záznamy o škodách spojené s konkrétním pojištěním.
		/// </summary>
		/// <param name="insuranceId">ID pojištění.</param>
		/// <returns>Seznam záznamů o škodách spojených s daným pojištěním.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID pojištění null nebo prázdné.</exception>
		public async Task<IEnumerable<DamageRecord>> GetByInsuranceIdAsync(string insuranceId)
		{
			if (string.IsNullOrWhiteSpace(insuranceId))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(insuranceId));
			}

			return await _context.DamageRecord
				.Where(i => i.InsuranceId == insuranceId)
				.Include(i => i.Insurance)
				.ToListAsync();
		}
	}
}
