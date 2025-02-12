using Pojistenci_v3.Data;
using Microsoft.EntityFrameworkCore;
using Pojistenci_v3.Api.Configuration;

/// <summary>
/// Hlavní třída programu pro konfiguraci a spuštění ASP.NET Core aplikace.
/// </summary>
namespace Pojistenci_v3.Api
{
	/// <summary>
	/// Hlavní třída programu obsahující konfiguraci služeb, middleware a spouštění aplikace.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Vstupní bod aplikace. Konfiguruje služby a middleware, následně spouští webovou aplikaci.
		/// </summary>
		/// <param name="args">Argumenty příkazového řádku.</param>
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Nastavení připojení k databázi
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

			// Registrace služeb, middleware a konfigurací
			builder.Services.AddApplicationServices();  // Registrace služeb (Managers, Repositories)
			builder.Services.AddIdentityServices();    // Registrace ASP.NET Identity
			builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile)); // Registrace AutoMapperu
			builder.Services.AddControllers();         // Registrace kontrolerů

			// Konfigurace CORS (umožnění přístupu pro React frontend)
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowReactApp", policy =>
				{
					policy.WithOrigins("http://localhost:5173")
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials();
				});
			});

			// Registrace Swaggeru pro API dokumentaci
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Vytvoření aplikace
			var app = builder.Build();

			// Seedování rolí a SuperAdmina při startu aplikace
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				await DatabaseSeeder.SeedRoles(services);
				await DatabaseSeeder.SeedSuperAdmin(services); // Nově přidané volání
			}

			// Konfigurace middleware (HTTPS, autentizace, autorizace)
			app.ConfigureMiddlewares();

			// Spuštění aplikace
			app.Run();
		}
	}
}
