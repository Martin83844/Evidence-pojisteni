using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Data.Interfaces;

namespace Pojistenci_v3.Data.Services
{
	/// <summary>
	/// Služba pro generování unikátních ID prostřednictvím uložených procedur.
	/// </summary>
	public class IdGeneratorService : IIdGeneratorService
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Inicializuje novou instanci služby <see cref="IdGeneratorService"/>.
		/// </summary>
		/// <param name="context">Databázový kontext aplikace.</param>
		public IdGeneratorService(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Generuje unikátní ID pro pojištění vozidla (Car Insurance).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		public async Task<string> GenerateCarInsuranceIdAsync()
		{
			return await ExecuteStoredProcedureAsync("GenerateCarInsuranceId");
		}

		/// <summary>
		/// Generuje unikátní ID pro pojištění nemovitosti (Home Insurance).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		public async Task<string> GenerateHomeInsuranceIdAsync()
		{
			return await ExecuteStoredProcedureAsync("GenerateHomeInsuranceId");
		}

		/// <summary>
		/// Generuje unikátní ID pro záznam o nehodě v pojištění vozidla (Car Insurance Accident Record).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		public async Task<string> GenerateCarInsuranceAccidentRecordIdAsync()
		{
			return await ExecuteStoredProcedureAsync("GenerateCarInsuranceAccidentRecordId");
		}

		/// <summary>
		/// Generuje unikátní ID pro záznam o škodě v pojištění nemovitosti (Home Insurance Damage Record).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		public async Task<string> GenerateHomeInsuranceDamageRecordIdAsync()
		{
			return await ExecuteStoredProcedureAsync("GenerateHomeInsuranceDamageRecordId");
		}

		/// <summary>
		/// Spouští uloženou proceduru pro generování unikátního ID.
		/// </summary>
		/// <param name="procedureName">Název uložené procedury.</param>
		/// <returns>Výsledek uložené procedury jako řetězec.</returns>
		/// <exception cref="InvalidOperationException">Vyhazuje výjimku, pokud uložená procedura nevrátí žádný výsledek.</exception>
		private async Task<string> ExecuteStoredProcedureAsync(string procedureName)
		{
			using var command = _context.Database.GetDbConnection().CreateCommand();
			command.CommandText = procedureName;
			command.CommandType = System.Data.CommandType.StoredProcedure;

			await _context.Database.OpenConnectionAsync();
			try
			{
				using var reader = await command.ExecuteReaderAsync();
				if (await reader.ReadAsync())
				{
					return reader.GetString(0);
				}
				throw new InvalidOperationException($"Uložená procedura '{procedureName}' nevrací žádný výsledek.");
			}
			finally
			{
				await _context.Database.CloseConnectionAsync();
			}
		}
	}
}
