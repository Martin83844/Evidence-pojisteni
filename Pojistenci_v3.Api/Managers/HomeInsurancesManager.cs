using AutoMapper;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Manager pro správu pojištění domácností.
	/// Poskytuje CRUD operace a specifické metody pro pojištění domácností.
	/// </summary>
	public class HomeInsurancesManager : Manager<HomeInsurance, HomeInsuranceDTO>, IHomeInsuranceManager
	{
		private readonly IHomeInsuranceRepository _homeInsuranceRepository;
		private readonly IMapper _mapper;
		private readonly IIdGeneratorService _idGeneratorService;


		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="HomeInsurancesManager"/>.
		/// </summary>
		/// <param name="homeInsuranceRepository">Repozitář pro přístup k datům pojištění domácností.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		/// <param name="idGeneratorService">Služba pro generování unikátních ID.</param>
		public HomeInsurancesManager(IHomeInsuranceRepository homeInsuranceRepository, IMapper mapper, IIdGeneratorService idGeneratorService) : base(homeInsuranceRepository, mapper)
		{
			_homeInsuranceRepository = homeInsuranceRepository;
			_idGeneratorService = idGeneratorService;
			_mapper = mapper;
		}

		/// <summary>
		/// Získá všechna pojištění domácností podle ID pojištěného uživatele.
		/// </summary>
		/// <param name="insuredId">ID pojištěného uživatele.</param>
		/// <returns>Seznam pojištění domácností jako DTO.</returns>
		public async Task<IEnumerable<HomeInsuranceDTO>> GetByInsuredIdAsync(string insuredId)
		{
			var homeInsurances = await _homeInsuranceRepository.GetByInsuredIdAsync(insuredId);
			return _mapper.Map<List<HomeInsuranceDTO>>(homeInsurances);
		}

		/// <summary>
		/// Získá všechna pojištění domácností podle ID pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění domácností jako DTO.</returns>
		public async Task<IEnumerable<HomeInsuranceDTO>> GetByInsurerIdAsync(string insurerId)
		{
			var homeInsurances = await _homeInsuranceRepository.GetByInsurerIdAsync(insurerId);
			return _mapper.Map<List<HomeInsuranceDTO>>(homeInsurances);
		}

		/// <summary>
		/// Vytvoří nové pojištění domácnosti.
		/// </summary>
		/// <param name="createHomeInsuranceDTO">DTO s daty pro vytvoření nového pojištění.</param>
		/// <param name="userId">ID uživatele (pojistitele), který vytváří pojištění.</param>
		/// <returns>DTO vytvořeného pojištění domácnosti.</returns>
		public async Task<HomeInsuranceDTO> CreateAsync(CreateHomeInsuranceDTO createHomeInsuranceDTO, string userId)
		{
			var homeInsurance = _mapper.Map<HomeInsurance>(createHomeInsuranceDTO);
			homeInsurance.Id = await _idGeneratorService.GenerateHomeInsuranceIdAsync();
			homeInsurance.InsurerId = userId;
			await _homeInsuranceRepository.AddAsync(homeInsurance);
			return _mapper.Map<HomeInsuranceDTO>(homeInsurance);
		}

		/// <summary>
		/// Aktualizuje existující pojištění domácnosti podle jeho ID.
		/// </summary>
		/// <param name="id">ID pojištění domácnosti.</param>
		/// <param name="updateHomeInsuranceDTO">DTO s daty pro aktualizaci pojištění.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		public async Task<bool> UpdateAsync(string id, UpdateHomeInsuranceDTO updateHomeInsuranceDTO)
		{
			var existingInsurance = await _homeInsuranceRepository.GetByIdAsync(id);
			if (existingInsurance == null)
			{
				return false;
			}

			_mapper.Map(updateHomeInsuranceDTO, existingInsurance);
			await _homeInsuranceRepository.UpdateAsync(existingInsurance);
			return true;
		}
	}
}
