using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Api.Managers;
using Pojistenci_v3.Common.Helpers;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Repositories;
using Pojistenci_v3.Data.Services;

namespace Pojistenci_v3.Api.Configuration
{
	/// <summary>
	/// Poskytuje metodu pro registraci služeb a závislostí v aplikaci.
	/// </summary>
	public static class ServiceRegistration
	{
		/// <summary>
		/// Registruje repozitáře, manažery a další služby do kolekce služeb aplikace.
		/// </summary>
		/// <param name="services">Kolekce služeb pro Dependency Injection.</param>
		/// <returns>Aktualizovaná kolekce služeb.</returns>
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Registrace repozitářů
			services.AddScoped<IInsuredRepository, InsuredRepository>();
			services.AddScoped<IInsurerRepository, InsurerRepository>();
			services.AddScoped<IHomeInsuranceRepository, HomeInsuranceRepository>();
			services.AddScoped<ICarInsuranceRepository, CarInsuranceRepository>();
			services.AddScoped<IDamageRecordRepository, DamageRecordRepository>();
			services.AddScoped<IIdGeneratorService, IdGeneratorService>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			// Registrace managerů
			services.AddScoped<IInsuredsManager, InsuredsManager>();
			services.AddScoped<IInsurersManager, InsurersManager>();
			services.AddScoped<IHomeInsuranceManager, HomeInsurancesManager>();
			services.AddScoped<ICarInsuranceManager, CarInsuranceManager>();
			services.AddScoped<ICarInsuranceAccidentRecordManager, CarInsuranceAccidentRecordManager>();
			services.AddScoped<IHomeInsuranceDamageRecordManager, HomeInsuranceDamageRecordManager>();
			services.AddScoped(typeof(IManager<,>), typeof(Manager<,>));

			// Registrace pomocných tříd
			services.AddScoped<UserAndUserEmailUpdater>();

			return services;
		}
	}

}
