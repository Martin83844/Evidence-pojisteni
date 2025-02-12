using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Repositories
{
	/// <summary>
	/// Repozitář pro správu pojištění vozidel s podporou specifických operací.
	/// </summary>
	public class CarInsuranceRepository : Repository<CarInsurance>, ICarInsuranceRepository
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Inicializuje nový repozitář pro pojištění vozidel.
		/// </summary>
		/// <param name="context">Databázový kontext.</param>
		public CarInsuranceRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		/// <summary>
		/// Získá konkrétní pojištění podle ID včetně všech navázaných dat.
		/// </summary>
		/// <param name="id">ID pojištění.</param>
		/// <returns>Pojištění nebo <c>null</c>, pokud neexistuje.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID null nebo prázdné.</exception>
		public override async Task<CarInsurance?> GetByIdAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(id));
			}
			return await _context.CarInsurance
				.Include(i => i.Insured)
				.Include(i => i.Insurer)
				.Include(i => i.DamageRecords)
				.FirstOrDefaultAsync(i => i.Id == id);
		}

		/// <summary>
		/// Získá všechna pojištění vozidel včetně navázaných pojištěných osob a pojistitelů.
		/// </summary>
		/// <returns>Seznam všech pojištění vozidel.</returns>
		public override async Task<IEnumerable<CarInsurance>> GetAllAsync()
		{
			return await _context.CarInsurance
				.Include(i => i.Insured)
				.Include(i => i.Insurer)
				.ToListAsync();
		}

		/// <summary>
		/// Získá všechna pojištění vozidel podle ID pojištěné osoby.
		/// </summary>
		/// <param name="insuredId">ID pojištěné osoby.</param>
		/// <returns>Seznam pojištění vozidel spojených s danou pojištěnou osobou.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID null nebo prázdné.</exception>
		public async Task<IEnumerable<CarInsurance>> GetByInsuredIdAsync(string insuredId)
		{
			if (string.IsNullOrWhiteSpace(insuredId))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(insuredId));
			}

			return await _context.CarInsurance
				.Where(i => i.InsuredId == insuredId)
				.Include(i => i.Insured)
				.Include(i => i.Insurer)
				.ToListAsync();
		}

		/// <summary>
		/// Získá všechna pojištění vozidel podle ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění vozidel spojených s daným pojistitelem.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID null nebo prázdné.</exception>
		public async Task<IEnumerable<CarInsurance>> GetByInsurerIdAsync(string insurerId)
		{
			if (string.IsNullOrWhiteSpace(insurerId))
			{
				throw new ArgumentException("ID nesmí být prázdné nebo null.", nameof(insurerId));
			}

			return await _context.CarInsurance
				.Where(i => i.InsurerId == insurerId)
				.Include(i => i.Insured)
				.Include(i => i.Insurer)
				.ToListAsync();
		}

	}
}
