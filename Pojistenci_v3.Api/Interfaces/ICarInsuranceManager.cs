using Pojistenci_v3.Common.ModelsDTO.CArInsuranceDTOs;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu pojištění vozidel.
	/// Dědí základní CRUD operace z <see cref="IManager{TEntity, TDTO}"/>.
	/// Poskytuje specifické metody pro práci s pojištěními vozidel.
	/// </summary>
	public interface ICarInsuranceManager : IManager<CarInsurance, CarInsuranceDTO>
	{
		/// <summary>
		/// Vytvoří nové pojištění vozidla.
		/// </summary>
		/// <param name="createCarInsuranceDTO">DTO s daty pro vytvoření nového pojištění vozidla.</param>
		/// <param name="id">ID pojistitele, který vytváří pojištění.</param>
		/// <returns>DTO vytvořeného pojištění vozidla.</returns>
		Task<CarInsuranceDTO> CreateAsync(CreateCarInsuranceDTO createCarInsuranceDTO, string id);

		/// <summary>
		/// Aktualizuje existující pojištění vozidla podle jeho ID.
		/// </summary>
		/// <param name="id">ID pojištění vozidla.</param>
		/// <param name="updateCarInsuranceDTO">DTO s daty pro aktualizaci pojištění vozidla.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		Task<bool> UpdateAsync(string id, UpdateCarInsuranceDTO updateCarInsuranceDTO);

		/// <summary>
		/// Získá všechna pojištění vozidel podle ID pojištěného uživatele.
		/// </summary>
		/// <param name="insuredId">ID pojištěného uživatele.</param>
		/// <returns>Seznam pojištění vozidel jako DTO.</returns>
		Task<IEnumerable<CarInsuranceDTO>> GetByInsuredIdAsync(string insuredId);

		/// <summary>
		/// Získá všechna pojištění vozidel podle ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění vozidel jako DTO.</returns>
		Task<IEnumerable<CarInsuranceDTO>> GetByInsurerIdAsync(string insurerId);
	}
}
