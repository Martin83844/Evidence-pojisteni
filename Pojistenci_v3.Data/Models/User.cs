using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Abstraktní třída pro reprezentaci uživatele v systému.
	/// Tato třída dědí od <see cref="IdentityUser"/> a přidává další vlastnosti specifické pro pojištěnce a pojistitele.
	/// </summary>
	public abstract class User : IdentityUser
	{
		/// <summary>
		/// Datum vytvoření uživatele.
		/// Toto pole je povinné a nastavuje se na aktuální čas při vytvoření objektu.
		/// </summary>
		[Display(Name = "Vytvořen")]
		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Datum poslední aktualizace uživatele.
		/// Toto pole je povinné a nastavuje se na aktuální čas při vytvoření a úpravě objektu.
		/// </summary>
		[Display(Name = "Upraven")]
		[Required]
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Jméno uživatele.
		/// Toto pole je povinné a omezeno na maximálně 20 znaků.
		/// </summary>
		[Display(Name = "Jméno")]
		[Required(ErrorMessage = "Zadejte jméno.")]
		[StringLength(20, ErrorMessage = "Jméno může mít maximálně 20 znaků.")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Příjmení uživatele.
		/// Toto pole je povinné a omezeno na maximálně 20 znaků.
		/// </summary>
		[Display(Name = "Příjmení")]
		[Required(ErrorMessage = "Zadejte příjmení.")]
		[StringLength(20, ErrorMessage = "Příjmení může mít maximálně 20 znaků.")]
		public string Surname { get; set; } = string.Empty;

		/// <summary>
		/// Město, ve kterém uživatel žije.
		/// Toto pole je povinné a omezeno na maximálně 30 znaků.
		/// </summary>
		[Required(ErrorMessage = "Zadejte město")]
		[Display(Name = "Město")]
		[StringLength(30, ErrorMessage = "Město může mít maximálně 30 znaků.")]
		public string City { get; set; } = string.Empty;

		/// <summary>
		/// Poštovní směrovací číslo uživatele.
		/// Toto pole je povinné a musí obsahovat přesně 5 číslic ve formátu PSČ.
		/// </summary>
		[Display(Name = "PSČ")]
		[Required(ErrorMessage = "Zadejte poštovní směrovací číslo.")]
		[RegularExpression(@"\d{5}", ErrorMessage = "PSČ musí obsahovat přesně 5 číslic.")]
		public string Postcode { get; set; } = string.Empty;

		/// <summary>
		/// Ulice a číslo popisné uživatele.
		/// Toto pole je povinné a omezeno na maximálně 50 znaků.
		/// </summary>
		[Display(Name = "Ulice a číslo")]
		[Required(ErrorMessage = "Zadejte ulici včetně čísla popisného.")]
		[StringLength(50, ErrorMessage = "Ulice může mít maximálně 50 znaků.")]
		public string Street { get; set; } = string.Empty;

		/// <summary>
		/// Role uživatele v systému.
		/// </summary>
		[Required]
		public UserRole Role { get; set; }
	}

	/// <summary>
	/// Definuje role, které může mít uživatel.
	/// </summary>
	public enum UserRole
	{
		/// <summary>
		/// Role pro pojištěnce.
		/// </summary>
		Insured,
		/// <summary>
		/// Role pro pojistitele.
		/// </summary>
		Insurer,
		/// <summary>
		/// Role pro superadministrátora.
		/// </summary>
		SuperAdmin
	}
}
