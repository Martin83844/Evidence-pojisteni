using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Common.Helpers;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Data.Repositories
{
	/// <summary>
	/// Repozitář pro správu pojistitelů s podporou specifických operací.
	/// </summary>
	public class InsurerRepository : Repository<Insurer>, IInsurerRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly UserAndUserEmailUpdater _userAndUserEmailUpdater;

		/// <summary>
		/// Inicializuje nový repozitář pro pojistitele.
		/// </summary>
		/// <param name="context">Databázový kontext.</param>
		/// <param name="userAndUserEmailUpdater">Třída pro aktualizaci údajů pojistitele.</param>
		public InsurerRepository(ApplicationDbContext context, UserAndUserEmailUpdater userAndUserEmailUpdater)
			: base(context)
		{
			_context = context;
			_userAndUserEmailUpdater = userAndUserEmailUpdater;
		}

		/// <summary>
		/// Získá pojistitele podle ID včetně navázaných pojištění.
		/// </summary>
		/// <param name="id">ID pojistitele.</param>
		/// <returns>Pojistitel nebo <c>null</c>, pokud neexistuje.</returns>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je ID null nebo prázdné.</exception>
		public override async Task<Insurer?> GetByIdAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID nesmí být null nebo prázdné.", nameof(id));
			}
			return await _context.Insurer
				.Include(i => i.ManagedInsurances)
				.ThenInclude(m => m.Insured)
				.FirstOrDefaultAsync(m => m.Id == id);
		}

		/// <summary>
		/// Aktualizuje údaje pojistitele.
		/// </summary>
		/// <param name="insurer">Aktualizovaná data pojistitele.</param>
		/// <exception cref="ArgumentNullException">Vyhozeno, pokud je pojistitel null.</exception>
		/// <exception cref="KeyNotFoundException">Vyhozeno, pokud pojistitel s daným ID neexistuje.</exception>
		/// <exception cref="Exception">Vyhozeno při konfliktu souběžnosti.</exception>
		public override async Task UpdateAsync(Insurer insurer)
		{
			if (insurer == null)
			{
				throw new ArgumentNullException(nameof(insurer), "Pojistitel nesmí být null.");
			}

			var existingInsurer = await _context.Insurer.FindAsync(insurer.Id);
			if (existingInsurer == null)
			{
				throw new KeyNotFoundException("Pojistitel s daným ID neexistuje.");
			}

			await UpdateInsurerEmail(existingInsurer, insurer);
			_userAndUserEmailUpdater.UpdateEntity(existingInsurer, insurer);

			_context.Update(existingInsurer);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Aktualizuje e-mail pojistitele, pokud došlo ke změně.
		/// </summary>
		/// <param name="existingInsurer">Pojistitel uložený v databázi.</param>
		/// <param name="insurer">Nová data pojistitele.</param>
		/// <exception cref="ArgumentException">Vyhozeno, pokud je nový e-mail null nebo prázdný.</exception>
		/// <exception cref="InvalidOperationException">Vyhozeno, pokud e-mail již existuje u jiného pojistitele.</exception>
		private async Task UpdateInsurerEmail(Insurer existingInsurer, Insurer insurer)
		{
			if (string.IsNullOrWhiteSpace(insurer.Email))
			{
				throw new ArgumentException("E-mail nesmí být null nebo prázdný.", nameof(insurer.Email));
			}

			if (!string.Equals(existingInsurer.Email, insurer.Email, StringComparison.OrdinalIgnoreCase))
			{
				bool emailExists = await _context.Insurer
					.AnyAsync(i => i.Email == insurer.Email && i.Id != insurer.Id);

				if (emailExists)
				{
					throw new InvalidOperationException("Zadaný e-mail již existuje u jiného pojistitele.");
				}

				_userAndUserEmailUpdater.UpdateEmail(existingInsurer, insurer.Email);
			}
		}
	}
}
