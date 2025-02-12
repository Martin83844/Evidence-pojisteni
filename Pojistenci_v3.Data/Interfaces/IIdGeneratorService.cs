namespace Pojistenci_v3.Data.Interfaces
{
	/// <summary>
	/// Rozhraní pro službu generování unikátních ID prostřednictvím uložených procedur.
	/// </summary>
	public interface IIdGeneratorService
	{
		/// <summary>
		/// Generuje unikátní ID pro pojištění vozidla (Car Insurance).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		Task<string> GenerateCarInsuranceIdAsync();

		/// <summary>
		/// Generuje unikátní ID pro pojištění nemovitosti (Home Insurance).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		Task<string> GenerateHomeInsuranceIdAsync();

		/// <summary>
		/// Generuje unikátní ID pro záznam o nehodě v pojištění vozidla (Car Insurance Accident Record).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		Task<string> GenerateCarInsuranceAccidentRecordIdAsync();

		/// <summary>
		/// Generuje unikátní ID pro záznam o škodě v pojištění nemovitosti (Home Insurance Damage Record).
		/// </summary>
		/// <returns>Unikátní identifikátor jako řetězec.</returns>
		Task<string> GenerateHomeInsuranceDamageRecordIdAsync();
	}
}
