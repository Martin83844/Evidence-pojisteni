using Microsoft.AspNetCore.Identity;
using Pojistenci_v3.Data.Models;
using Pojistenci_v3.Data;

namespace Pojistenci_v3.Api.Configuration
{
	/// <summary>
	/// Třída zodpovědná za konfiguraci Identity služeb a autentizace v aplikaci.
	/// </summary>
	public static class IdentityConfiguration
	{
		/// <summary>
		/// Rozšiřující metoda pro přidání Identity služeb do DI kontejneru.
		/// Zajišťuje konfiguraci Identity, správu uživatelů a přihlašování.
		/// </summary>
		/// <param name="services">Kolekce služeb pro Dependency Injection.</param>
		public static void AddIdentityServices(this IServiceCollection services)
		{
			// Přidání Identity frameworku s podporou EF Core pro ukládání uživatelů
			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders(); // Povolení tokenů pro reset hesla, dvoufázovou autentizaci atd.

			// Konfigurace cookies pro autentizaci uživatele
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true; // Zabraňuje JavaScriptu přístup k cookie
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Použití pouze přes HTTPS
				options.Cookie.SameSite = SameSiteMode.Strict; // Prevence proti CSRF útokům
				options.LoginPath = "/Account/Login"; // Cesta k přihlašovací stránce
				options.LogoutPath = "/Account/Logout"; // Cesta k odhlašovací stránce
				options.ExpireTimeSpan = TimeSpan.FromHours(1); // Doba platnosti cookie
				options.SlidingExpiration = true; // Automatické prodloužení platnosti cookie při aktivitě
			});
		}
	}
}