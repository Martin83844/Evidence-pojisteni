using Pojistenci_v3.Data;

namespace Pojistenci_v3.Common.Helpers
{
	/// <summary>
	/// Pomocná třída pro aktualizaci údajů uživatelů a jejich e-mailových adres.
	/// </summary>
	public class UserAndUserEmailUpdater
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Inicializuje novou instanci třídy UserAndUserEmailUpdater.
		/// </summary>
		/// <param name="context">Databázový kontext aplikace.</param>
		public UserAndUserEmailUpdater(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Aktualizuje běžné vlastnosti entity podle dat z jiné entity.
		/// </summary>
		/// <typeparam name="T">Typ entity.</typeparam>
		/// <param name="existingEntity">Existující entita, která má být aktualizována.</param>
		/// <param name="updatedEntity">Entita obsahující nové hodnoty.</param>
		public void UpdateEntity<T>(T existingEntity, T updatedEntity) where T : class
		{
			typeof(T).GetProperty("Name")?.SetValue(existingEntity, typeof(T).GetProperty("Name")?.GetValue(updatedEntity));
			typeof(T).GetProperty("Surname")?.SetValue(existingEntity, typeof(T).GetProperty("Surname")?.GetValue(updatedEntity));
			typeof(T).GetProperty("PhoneNumber")?.SetValue(existingEntity, typeof(T).GetProperty("PhoneNumber")?.GetValue(updatedEntity));
			typeof(T).GetProperty("City")?.SetValue(existingEntity, typeof(T).GetProperty("City")?.GetValue(updatedEntity));
			typeof(T).GetProperty("Postcode")?.SetValue(existingEntity, typeof(T).GetProperty("Postcode")?.GetValue(updatedEntity));
			typeof(T).GetProperty("Street")?.SetValue(existingEntity, typeof(T).GetProperty("Street")?.GetValue(updatedEntity));
			typeof(T).GetProperty("UpdatedAt")?.SetValue(existingEntity, DateTime.UtcNow);
		}

		/// <summary>
		/// Aktualizuje e-mailové vlastnosti entity.
		/// </summary>
		/// <typeparam name="T">Typ entity.</typeparam>
		/// <param name="existingEntity">Entita, která má být aktualizována.</param>
		/// <param name="newEmail">Nová e-mailová adresa.</param>

		public void UpdateEmail<T>(T existingEntity, string newEmail) where T : class
		{
			typeof(T).GetProperty("Email")?.SetValue(existingEntity, newEmail);
			typeof(T).GetProperty("NormalizedEmail")?.SetValue(existingEntity, newEmail.ToUpperInvariant());
			typeof(T).GetProperty("UserName")?.SetValue(existingEntity, newEmail);
			typeof(T).GetProperty("NormalizedUserName")?.SetValue(existingEntity, newEmail.ToUpperInvariant());
		}
	}
}
