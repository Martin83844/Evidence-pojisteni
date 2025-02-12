using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu pojištění domácností.
	/// Dědí základní CRUD operace z <see cref="IManager{TEntity, TDTO}"/>.
	/// Poskytuje specifické metody pro práci s pojištěními domácností.
	/// </summary>
	public interface IHomeInsuranceManager : IManager<HomeInsurance, HomeInsuranceDTO>
	{
		/// <summary>
		/// Vytvoří nové pojištění domácnosti.
		/// </summary>
		/// <param name="createHomeInsuranceDTO">DTO s daty pro vytvoření nového pojištění domácnosti.</param>
		/// <param name="id">ID pojistitele, který vytváří pojištění.</param>
		/// <returns>DTO vytvořeného pojištění domácnosti.</returns>
		Task<HomeInsuranceDTO> CreateAsync(CreateHomeInsuranceDTO createHomeInsuranceDTO, string id);

		/// <summary>
		/// Aktualizuje existující pojištění domácnosti podle jeho ID.
		/// </summary>
		/// <param name="id">ID pojištění domácnosti.</param>
		/// <param name="updateHomeInsuranceDTO">DTO s daty pro aktualizaci pojištění domácnosti.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		Task<bool> UpdateAsync(string id, UpdateHomeInsuranceDTO updateHomeInsuranceDTO);

		/// <summary>
		/// Získá všechna pojištění domácností podle ID pojištěného uživatele.
		/// </summary>
		/// <param name="insuredId">ID pojištěného uživatele.</param>
		/// <returns>Seznam pojištění domácností jako DTO.</returns>
		Task<IEnumerable<HomeInsuranceDTO>> GetByInsuredIdAsync(string insuredId);

		/// <summary>
		/// Získá všechna pojištění domácností podle ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění domácností jako DTO.</returns>
		Task<IEnumerable<HomeInsuranceDTO>> GetByInsurerIdAsync(string insurerId);
	}
}
