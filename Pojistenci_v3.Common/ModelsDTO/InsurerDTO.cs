using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Common.ModelsDTO
{
	/// <summary>
	/// DTO pro zobrazení a editaci pojišťovatele.
	/// Obsahuje informace o pojišťovateli a validační pravidla pro každou vlastnost.
	/// </summary>
	public class InsurerDTO
	{
		/// <summary>
		/// Jedinečný identifikátor pojišťovatele.
		/// </summary>
		[Required]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// E-mailová adresa pojišťovatele.
		/// Musí být zadána platná e-mailová adresa.
		/// </summary>
		[Required(ErrorMessage = "Zadejte e-mailovou adresu.")]
		[EmailAddress(ErrorMessage = "Zadejte platnou e-mailovou adresu.")]
		[Display(Name = "E-mail")]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Město, kde pojišťovatel sídlí.
		/// Musí být zadáno a může obsahovat maximálně 30 znaků.
		/// </summary>
		[Required(ErrorMessage = "Zadejte město.")]
		[StringLength(30, ErrorMessage = "Město může mít maximálně 30 znaků.")]
		[Display(Name = "Město")]
		public string City { get; set; } = string.Empty;

		/// <summary>
		/// Poštovní směrovací číslo (PSČ) pojišťovatele.
		/// Musí obsahovat přesně 5 číslic.
		/// </summary>
		[Required(ErrorMessage = "Zadejte PSČ.")]
		[RegularExpression(@"\d{5}", ErrorMessage = "PSČ musí obsahovat přesně 5 číslic.")]
		[Display(Name = "PSČ")]
		public string Postcode { get; set; } = string.Empty;

		/// <summary>
		/// Ulice a číslo popisné pojišťovatele.
		/// Musí být zadáno a může obsahovat maximálně 50 znaků.
		/// </summary>
		[Required(ErrorMessage = "Zadejte ulici.")]
		[StringLength(50, ErrorMessage = "Ulice může mít maximálně 50 znaků.")]
		[Display(Name = "Ulice a číslo")]
		public string Street { get; set; } = string.Empty;

		/// <summary>
		/// Jméno pojišťovatele.
		/// Musí být zadáno a může obsahovat maximálně 20 znaků.
		/// </summary>
		[Required(ErrorMessage = "Zadejte jméno.")]
		[StringLength(20, ErrorMessage = "Jméno může mít maximálně 20 znaků.")]
		[Display(Name = "Jméno")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Příjmení pojišťovatele.
		/// Musí být zadáno a může obsahovat maximálně 20 znaků.
		/// </summary>
		[Required(ErrorMessage = "Zadejte příjmení.")]
		[StringLength(20, ErrorMessage = "Příjmení může mít maximálně 20 znaků.")]
		[Display(Name = "Příjmení")]
		public string Surname { get; set; } = string.Empty;

		/// <summary>
		/// Telefonní číslo pojišťovatele.
		/// Musí být zadáno ve formátu +420 ddddddddd.
		/// </summary>
		[Required(ErrorMessage = "Zadejte telefonní číslo.")]
		[RegularExpression(@"^\+420\d{9}$", ErrorMessage = "Telefonní číslo musí být ve formátu +420 ddddddddd.")]
		[Display(Name = "Telefonní číslo")]
		public string PhoneNumber { get; set; } = string.Empty;

		/// <summary>
		/// Datum poslední aktualizace údajů pojišťovatele.
		/// </summary>
		[Required]
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
