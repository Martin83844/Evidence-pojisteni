using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDamageRecordDTOs;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu záznamů o škodách spojených s pojištěním domácností.
	/// Dědí základní CRUD operace z <see cref="IManager{TEntity, TDTO}"/>.
	/// Poskytuje specifické metody pro práci se záznamy o škodách.
	/// </summary>
	public interface IHomeInsuranceDamageRecordManager : IManager<HomeInsuranceDamageRecord, HomeInsuranceDamageRecordDTO>
	{
		/// <summary>
		/// Vytvoří nový záznam o škodě spojený s pojištěním domácnosti.
		/// </summary>
		/// <param name="createHomeInsuranceDamageRecordDTO">DTO s daty pro vytvoření nového záznamu o škodě.</param>
		/// <returns>DTO vytvořeného záznamu o škodě.</returns>
		Task<HomeInsuranceDamageRecordDTO> CreateAsync(CreateHomeInsuranceDamageRecordDTO createHomeInsuranceDamageRecordDTO);

		/// <summary>
		/// Aktualizuje existující záznam o škodě podle jeho ID.
		/// </summary>
		/// <param name="id">ID záznamu o škodě.</param>
		/// <param name="updateCarInsuranceAccidentRecordDTO">DTO s daty pro aktualizaci záznamu o škodě.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		Task<bool> UpdateAsync(string id, UpdateHomeInsuranceDamageRecordDTO updateCarInsuranceAccidentRecordDTO);

		/// <summary>
		/// Získá všechny záznamy o škodách podle ID pojištění domácnosti.
		/// </summary>
		/// <param name="insuranceId">ID pojištění domácnosti, podle kterého se záznamy o škodách vyhledávají.</param>
		/// <returns>Seznam záznamů o škodách jako DTO.</returns>
		Task<IEnumerable<HomeInsuranceDamageRecordDTO>> GetByInsuranceIdAsync(string insuranceId);
	}
}
