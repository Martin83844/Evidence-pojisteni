using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO
{
	/// <summary>
	/// DTO pro přihlášení uživatele.
	/// Obsahuje potřebné informace a validace pro přihlášení uživatele do systému.
	/// </summary>
	public class LoginDTO
	{
		/// <summary>
		/// E-mailová adresa uživatele.
		/// Musí být zadána platná e-mailová adresa.
		/// </summary>
		[Required(ErrorMessage = "Zadejte e-mailovou adresu.")]
		[EmailAddress(ErrorMessage = "Zadejte platnou e-mailovou adresu.")]
		[Display(Name = "E-mail")]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Heslo uživatele.
		/// Musí obsahovat minimálně 6 znaků.
		/// </summary>
		[Required(ErrorMessage = "Zadejte heslo.")]
		[MinLength(6, ErrorMessage = "Heslo musí mít minimálně 6 znaků.")]
		[Display(Name = "Heslo")]
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Indikátor, zda si má systém zapamatovat přihlášení uživatele.
		/// Pokud je nastaven na true, přihlášení zůstane aktivní i po zavření prohlížeče.
		/// </summary>
		[Display(Name = "Zapamatovat si mě")]
		public bool RememberMe { get; set; } = false;
	}
}
