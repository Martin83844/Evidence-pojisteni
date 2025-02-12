using Pojistenci_v3.Common.ModelsDTO.CarInsuranceAccidentRecordDTOs;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu záznamů o nehodách spojených s pojištěním vozidel.
	/// Dědí základní CRUD operace z <see cref="IManager{TEntity, TDTO}"/>.
	/// Poskytuje specifické metody pro práci se záznamy o nehodách.
	/// </summary>
	public interface ICarInsuranceAccidentRecordManager : IManager<CarInsuranceAccidentRecord, CarInsuranceAccidentRecordDTO>
	{
		/// <summary>
		/// Vytvoří nový záznam o nehodě spojený s pojištěním vozidla.
		/// </summary>
		/// <param name="createCarInsuranceAccidentRecordDTO">DTO s daty pro vytvoření nového záznamu o nehodě.</param>
		/// <returns>DTO vytvořeného záznamu o nehodě.</returns>
		Task<CarInsuranceAccidentRecordDTO> CreateAsync(CreateCarInsuranceAccidentRecordDTO createCarInsuranceAccidentRecordDTO);

		/// <summary>
		/// Aktualizuje existující záznam o nehodě podle jeho ID.
		/// </summary>
		/// <param name="id">ID záznamu o nehodě.</param>
		/// <param name="updateCarInsuranceAccidentRecordDTO">DTO s daty pro aktualizaci záznamu o nehodě.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		Task<bool> UpdateAsync(string id, UpdateCarInsuranceAccidentRecordDTO updateCarInsuranceAccidentRecordDTO);

		/// <summary>
		/// Získá všechny záznamy o nehodách podle ID pojištění.
		/// </summary>
		/// <param name="insuranceId">ID pojištění, podle kterého se záznamy o nehodách vyhledávají.</param>
		/// <returns>Seznam záznamů o nehodách jako DTO.</returns>
		Task<IEnumerable<CarInsuranceAccidentRecordDTO>> GetByInsuranceIdAsync(string insuranceId);
	}
}
