using AutoMapper;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDamageRecordDTOs;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Manager pro správu záznamů o škodách na pojištění domácností.
	/// Poskytuje CRUD operace a specifické metody pro práci se záznamy o škodách.
	/// </summary>
	public class HomeInsuranceDamageRecordManager : Manager<DamageRecord, HomeInsuranceDamageRecordDTO>, IHomeInsuranceDamageRecordManager
	{
		private readonly IDamageRecordRepository _damageRecordRepository;
		private readonly IIdGeneratorService _idGeneratorService;
		private readonly IMapper _mapper;

		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="HomeInsuranceDamageRecordManager"/>.
		/// </summary>
		/// <param name="damageRecordRepository">Repozitář pro správu záznamů o škodách.</param>
		/// <param name="idGeneratorService">Služba pro generování unikátních ID.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		public HomeInsuranceDamageRecordManager(IDamageRecordRepository damageRecordRepository, IIdGeneratorService idGeneratorService, IMapper mapper) : base(damageRecordRepository, mapper)
		{
			_damageRecordRepository = damageRecordRepository;
			_idGeneratorService = idGeneratorService;
			_mapper = mapper;
		}

		/// <summary>
		/// Vytvoří nový záznam o škodě na pojištění domácnosti.
		/// </summary>
		/// <param name="createHomeInsuranceDamageRecordDTO">DTO s daty pro vytvoření nového záznamu o škodě.</param>
		/// <returns>DTO vytvořeného záznamu o škodě.</returns>
		public async Task<HomeInsuranceDamageRecordDTO> CreateAsync(CreateHomeInsuranceDamageRecordDTO createHomeInsuranceDamageRecordDTO)
		{
			var damageRecord = _mapper.Map<HomeInsuranceDamageRecord>(createHomeInsuranceDamageRecordDTO);
			damageRecord.Id = await _idGeneratorService.GenerateHomeInsuranceDamageRecordIdAsync();
			await _damageRecordRepository.AddAsync(damageRecord);
			return _mapper.Map<HomeInsuranceDamageRecordDTO>(damageRecord);
		}

		/// <summary>
		/// Získá všechny záznamy o škodách podle ID pojištění domácnosti.
		/// </summary>
		/// <param name="insuranceId">ID pojištění domácnosti.</param>
		/// <returns>Seznam záznamů o škodách jako DTO.</returns>
		public async Task<IEnumerable<HomeInsuranceDamageRecordDTO>> GetByInsuranceIdAsync(string insuranceId)
		{
			var damageRecords = await _damageRecordRepository.GetByInsuranceIdAsync(insuranceId);
			return _mapper.Map<List<HomeInsuranceDamageRecordDTO>>(damageRecords);
		}

		/// <summary>
		/// Aktualizuje existující záznam o škodě podle jeho ID.
		/// </summary>
		/// <param name="id">ID záznamu o škodě.</param>
		/// <param name="updateHomeInsuranceDamageRecordDTO">DTO s daty pro aktualizaci záznamu o škodě.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		public async Task<bool> UpdateAsync(string id, UpdateHomeInsuranceDamageRecordDTO updateHomeInsuranceDamageRecordDTO)
		{
			var existingDamageRecord = await _damageRecordRepository.GetByIdAsync(id);
			if (existingDamageRecord == null)
			{
				return false;
			}

			_mapper.Map(updateHomeInsuranceDamageRecordDTO, existingDamageRecord);
			await _damageRecordRepository.UpdateAsync(existingDamageRecord);
			return true;
		}
	}
}
