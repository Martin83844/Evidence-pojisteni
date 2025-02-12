using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Common.Helpers;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Repositories
{
	/// <summary>
	/// Repozitář pro správu pojištěných osob s podporou specifických operací.
	/// </summary>
	public class InsuredRepository : Repository<Insured>, IInsuredRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly UserAndUserEmailUpdater _userAndUserEmailUpdater;

		/// <summary>
		/// Inicializuje nový repozitář pro pojištěné osoby.
		/// </summary>
		/// <param name="context">Databázový kontext.</param>
		/// <param name="userAndUserEmailUpdater">Třída pro aktualizaci údajů pojištěných osob.</param>
		public InsuredRepository(ApplicationDbContext context, UserAndUserEmailUpdater userAndUserEmailUpdater) : base(context)
		{
			_context = context;
			_userAndUserEmailUpdater = userAndUserEmailUpdater;
		}

		/// <summary>
		/// Získá pojištěnou osobu podle ID včetně navázaných pojištění.
		/// </summary>
		/// <param name="id">ID pojištěné osoby.</param>
		/// <returns>Pojištěná osoba nebo <c>null</c>, pokud neexistuje.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID null nebo prázdné.</exception>
		public override async Task<Insured?> GetByIdAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID nesmí být null nebo prázdné.", nameof(id));
			}
			return await _context.Insured
				.Include(i => i.Insurances)
				.FirstOrDefaultAsync(m => m.Id == id);
		}

		/// <summary>
		/// Aktualizuje údaje pojištěné osoby.
		/// </summary>
		/// <param name="insured">Aktualizovaná data pojištěné osoby.</param>
		/// <exception cref="ArgumentNullException">Vyhozeno, pokud je pojištěná osoba null.</exception>
		/// <exception cref="KeyNotFoundException">Vyhozeno, pokud pojištěná osoba s daným ID neexistuje.</exception>
		public override async Task UpdateAsync(Insured insured)
		{
			if (insured == null)
			{
				throw new ArgumentNullException(nameof(insured), "Pojištěný nesmí být null.");
			}

			var existingInsured = await _context.Insured.FindAsync(insured.Id);
			if (existingInsured == null)
			{
				throw new KeyNotFoundException("Pojištěný s daným ID neexistuje.");
			}

			await UpdateInsuredEmail(existingInsured, insured);
			_userAndUserEmailUpdater.UpdateEntity(existingInsured, insured);

			_context.Update(existingInsured);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Aktualizuje e-mail pojištěné osoby, pokud došlo ke změně.
		/// </summary>
		/// <param name="existingInsured">Pojištěná osoba uložená v databázi.</param>
		/// <param name="insured">Nová data pojištěné osoby.</param>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je nový e-mail null nebo prázdný.</exception>
		/// <exception cref="InvalidOperationException">Vyhozeno, pokud e-mail již existuje u jiné pojištěné osoby.</exception>
		private async Task UpdateInsuredEmail(Insured existingInsured, Insured insured)
		{
			if (string.IsNullOrWhiteSpace(insured.Email))
			{
				throw new ArgumentException("E-mail nesmí být null nebo prázdný.", nameof(insured.Email));
			}

			if (!string.Equals(existingInsured.Email, insured.Email, StringComparison.OrdinalIgnoreCase))
			{
				bool emailExists = await _context.Insured
					.AnyAsync(i => i.Email == insured.Email && i.Id != insured.Id);

				if (emailExists)
				{
					throw new InvalidOperationException("Zadaný e-mail již existuje u jiné pojištěné osoby.");
				}

				_userAndUserEmailUpdater.UpdateEmail(existingInsured, insured.Email);
			}
		}
	}
}