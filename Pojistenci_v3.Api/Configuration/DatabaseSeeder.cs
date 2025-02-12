using Microsoft.AspNetCore.Identity;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Configuration
{
	/// <summary>
	/// Třída zodpovědná za inicializaci dat v databázi při startu aplikace.
	/// </summary>
	public static class DatabaseSeeder
	{
		/// <summary>
		/// Asynchronně seeduje role do databáze, pokud ještě neexistují.
		/// </summary>
		/// <param name="serviceProvider">Poskytovatel služeb aplikace.</param>
		/// <returns>Úloha reprezentující asynchronní operaci.</returns>
		public static async Task SeedRoles(IServiceProvider serviceProvider)
		{
			// Získání RoleManageru z DI kontejneru
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			// Definované role pro aplikaci
			string[] roles = { "Insured", "Insurer", "SuperAdmin" };

			// Procházíme seznam rolí a pokud nějaká neexistuje, vytvoříme ji
			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}

		/// <summary>
		/// Asynchronně seeduje SuperAdmin uživatele do databáze.
		/// </summary>
		public static async Task SeedSuperAdmin(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<User>>(); // Používáme User
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var superAdminEmail = "admin@example.com";
			var superAdminPassword = "Admin123!";

			var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);
			if (superAdmin == null)
			{
				superAdmin = new Insurer // Vytváříme Instanci Insurer
				{
					UserName = superAdminEmail,
					Email = superAdminEmail
				};

				var result = await userManager.CreateAsync(superAdmin, superAdminPassword);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						Console.WriteLine($"Error seeding SuperAdmin: {error.Description}");
					}
				}
			}
		}
	}
}
