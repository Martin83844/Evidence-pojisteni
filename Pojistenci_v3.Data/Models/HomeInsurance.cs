using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Pojistenci_v3.Data.Models
{
	/// <summary>
	/// Reprezentuje pojištění nemovitosti. Dědí obecné vlastnosti z třídy <see cref="Insurance"/>.
	/// </summary>
	public class HomeInsurance : Insurance
	{
		/// <summary>
		/// Inicializuje novou instanci pojištění nemovitosti a nastaví typ pojištění na <see cref="InsuranceType.HomeInsurance"/>.
		/// </summary>
		public HomeInsurance()
		{
			Type = InsuranceType.HomeInsurance;
		}

		/// <summary>
		/// Adresa pojišťované nemovitosti.
		/// </summary>
		[Display(Name = "Adresa nemovitosti")]
		[Required(ErrorMessage = "Adresa nemovitosti je povinná.")]
		public string PropertyAddress { get; set; } = string.Empty;

		/// <summary>
		/// Typ nemovitosti (např. rodinný dům, byt, komerční budova).
		/// </summary>
		[Display(Name = "Typ nemovitosti")]
		[Required(ErrorMessage = "Typ nemovitosti je povinný.")]
		public PropertyType PropertyType { get; set; }

		/// <summary>
		/// Rok výstavby nemovitosti.
		/// </summary>
		[Display(Name = "Rok výstavby")]
		[Required(ErrorMessage = "Rok výstavby je povinný.")]
		[Range(0, int.MaxValue, ErrorMessage = "Rok výstavby musí být nezáporné číslo.")]
		public int YearBuilt { get; set; }

		/// <summary>
		/// Hodnota nemovitosti v korunách.
		/// </summary>
		[Display(Name = "Hodnota nemovitosti (v Kč)")]
		[Required(ErrorMessage = "Hodnota nemovitosti je povinná.")]
		[Range(0, double.MaxValue, ErrorMessage = "Hodnota nemovitosti musí být nezáporné číslo.")]
		[Precision(18, 2)]
		public decimal PropertyValue { get; set; }

		/// <summary>
		/// Plocha nemovitosti v metrech čtverečních.
		/// </summary>
		[Display(Name = "Plocha nemovitosti (m²)")]
		[Range(0, int.MaxValue, ErrorMessage = "Plocha nemovitosti musí být nezáporné číslo.")]
		public int PropertyArea { get; set; }

		/// <summary>
		/// Typ pojištění nemovitosti (např. proti požáru, krádeži, povodním nebo všechna rizika).
		/// </summary>
		[Display(Name = "Typ pojištění nemovitosti")]
		[Required(ErrorMessage = "Typ pojištění nemovitosti je povinný.")]
		public HomeInsuranceType HomeInsuranceType { get; set; }

		/// <summary>
		/// Jméno vlastníka nemovitosti.
		/// </summary>
		[Display(Name = "Jméno vlastníka")]
		[Required(ErrorMessage = "Jméno vlastníka je povinné.")]
		public string OwnerName { get; set; } = string.Empty;

		/// <summary>
		/// Kontakt na vlastníka nemovitosti.
		/// </summary>
		[Display(Name = "Kontakt na vlastníka")]
		[RegularExpression(@"^\+420\d{9}$", ErrorMessage = "Telefonní číslo musí být ve formátu +420 ddddddddd.")]
		[Required(ErrorMessage = "Kontakt na vlastníka je povinný.")]
		public string OwnerContact { get; set; } = string.Empty;

		/// <summary>
		/// Historie záznamů škod spojených s touto nemovitostí.
		/// </summary>
		private readonly List<HomeInsuranceDamageRecord> _damageHistory = new List<HomeInsuranceDamageRecord>();

		/// <summary>
		/// Kolekce pouze pro čtení obsahující historii záznamů škod spojených s touto nemovitostí.
		/// </summary>
		public IReadOnlyCollection<HomeInsuranceDamageRecord> DamageHistory => _damageHistory.AsReadOnly();
	}

	/// <summary>
	/// Typ nemovitosti (např. rodinný dům, byt, komerční budova).
	/// </summary>
	public enum PropertyType
	{
		[Display(Name = "Rodinný dům")]
		House,
		[Display(Name = "Byt")]
		Apartment,
		[Display(Name = "Komerční budova")]
		Commercial,
		[Display(Name = "Chata / Chalupa")]
		Cottage,
		[Display(Name = "Jiný typ")]
		Other
	}

	/// <summary>
	/// Typ pojištění nemovitosti (např. proti požáru, krádeži, povodním).
	/// </summary>
	public enum HomeInsuranceType
	{
		[Display(Name = "Pojištění proti požáru")]
		Fire,
		[Display(Name = "Pojištění proti krádeži")]
		Theft,
		[Display(Name = "Pojištění proti povodním")]
		Flood,
		[Display(Name = "Pojištění všech rizik")]
		AllRisk
	}
}
