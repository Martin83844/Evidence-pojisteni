using AutoMapper;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.CArInsuranceDTOs;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Manager pro správu pojištění vozidel.
	/// Poskytuje CRUD operace a specifické metody pro pojištění vozidel.
	/// </summary>
	public class CarInsuranceManager : Manager<CarInsurance, CarInsuranceDTO>, ICarInsuranceManager
	{
		private readonly ICarInsuranceRepository _carInsuranceRepository;
		private readonly IIdGeneratorService _idGeneratorService;
		private readonly IMapper _mapper;

		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="CarInsuranceManager"/>.
		/// </summary>
		/// <param name="carInsuranceRepository">Repozitář pro přístup k datům pojištění vozidel.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		/// <param name="idGeneratorService">Služba pro generování unikátních ID.</param>
		public CarInsuranceManager(ICarInsuranceRepository carInsuranceRepository, IMapper mapper, IIdGeneratorService idGeneratorService) : base(carInsuranceRepository, mapper)
		{
			_carInsuranceRepository = carInsuranceRepository;
			_idGeneratorService = idGeneratorService;
			_mapper = mapper;
		}

		/// <summary>
		/// Získá všechna pojištění vozidel podle ID pojištěného uživatele.
		/// </summary>
		/// <param name="insuredId">ID pojištěného uživatele.</param>
		/// <returns>Seznam pojištění vozidel jako DTO.</returns>
		public async Task<IEnumerable<CarInsuranceDTO>> GetByInsuredIdAsync(string insuredId)
		{
			var carInsurances = await _carInsuranceRepository.GetByInsuredIdAsync(insuredId);
			return _mapper.Map<List<CarInsuranceDTO>>(carInsurances);
		}

		/// <summary>
		/// Získá všechna pojištění vozidel podle ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění vozidel jako DTO.</returns>
		public async Task<IEnumerable<CarInsuranceDTO>> GetByInsurerIdAsync(string insurerId)
		{
			var carInsurances = await _carInsuranceRepository.GetByInsurerIdAsync(insurerId);
			return _mapper.Map<List<CarInsuranceDTO>>(carInsurances);
		}

		/// <summary>
		/// Vytvoří nové pojištění vozidla.
		/// </summary>
		/// <param name="createCarInsuranceDTO">DTO s daty pro vytvoření nového pojištění.</param>
		/// <param name="userId">ID uživatele (pojistitele), který vytváří pojištění.</param>
		/// <returns>DTO vytvořeného pojištění vozidla.</returns>
		public async Task<CarInsuranceDTO> CreateAsync(CreateCarInsuranceDTO createCarInsuranceDTO, string userId)
		{
			var carInsurance = _mapper.Map<CarInsurance>(createCarInsuranceDTO);
			carInsurance.Id = await _idGeneratorService.GenerateCarInsuranceIdAsync();
			carInsurance.InsurerId = userId;
			await _carInsuranceRepository.AddAsync(carInsurance);
			return _mapper.Map<CarInsuranceDTO>(carInsurance);
		}

		/// <summary>
		/// Aktualizuje existující pojištění vozidla podle jeho ID.
		/// </summary>
		/// <param name="id">ID pojištění vozidla.</param>
		/// <param name="updateCarInsuranceDTO">DTO s daty pro aktualizaci pojištění.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		public async Task<bool> UpdateAsync(string id, UpdateCarInsuranceDTO updateCarInsuranceDTO)
		{
			var existingInsurance = await _carInsuranceRepository.GetByIdAsync(id);
			if (existingInsurance == null)
			{
				return false;
			}

			_mapper.Map(updateCarInsuranceDTO, existingInsurance);
			await _carInsuranceRepository.UpdateAsync(existingInsurance);
			return true;
		}
	}
}