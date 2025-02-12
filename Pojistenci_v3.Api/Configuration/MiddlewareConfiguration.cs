namespace Pojistenci_v3.Api.Configuration
{
	/// <summary>
	/// Třída obsahující konfiguraci middleware komponent pro ASP.NET Core aplikaci.
	/// </summary>
	public static class MiddlewareConfiguration
	{
		/// <summary>
		/// Konfiguruje a přidává všechny potřebné middleware komponenty do aplikace.
		/// </summary>
		/// <param name="app">Instance aplikace <see cref="WebApplication"/>.</param>
		public static void ConfigureMiddlewares(this WebApplication app)
		{
			// Middleware pro CORS – umožňuje přístup z React frontend aplikace
			app.UseCors("AllowReactApp");

			// Middleware pro zabezpečení – HTTPS, autentizace a autorizace
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			// Middleware pro vývojové prostředí – aktivace Swagger dokumentace API
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// Middleware pro směrování požadavků na kontrolery
			app.MapControllers();
		}
	}
}
