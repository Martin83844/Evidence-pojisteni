using AutoMapper;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.CarInsuranceAccidentRecordDTOs;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Manager pro správu záznamů o nehodách pro pojištění vozidel.
	/// Poskytuje CRUD operace a specifické metody pro práci s nehodami.
	/// </summary>
	public class CarInsuranceAccidentRecordManager : Manager<DamageRecord, CarInsuranceAccidentRecordDTO>, ICarInsuranceAccidentRecordManager
	{
		private readonly IDamageRecordRepository _damageRecordRepository;
		private readonly IIdGeneratorService _idGeneratorService;
		private readonly IMapper _mapper;

		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="CarInsuranceAccidentRecordManager"/>.
		/// </summary>
		/// <param name="damageRecordRepository">Repozitář pro přístup k záznamům o škodách.</param>
		/// <param name="idGeneratorService">Služba pro generování unikátních ID.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		public CarInsuranceAccidentRecordManager(IDamageRecordRepository damageRecordRepository, IIdGeneratorService idGeneratorService, IMapper mapper) : base(damageRecordRepository, mapper)
		{
			_damageRecordRepository = damageRecordRepository;
			_idGeneratorService = idGeneratorService;
			_mapper = mapper;
		}

		/// <summary>
		/// Vytvoří nový záznam o nehodě spojený s pojištěním vozidla.
		/// </summary>
		/// <param name="createCarInsuranceAccidentRecordDTO">DTO s daty pro vytvoření nového záznamu o nehodě.</param>
		/// <returns>DTO vytvořeného záznamu o nehodě.</returns>
		public async Task<CarInsuranceAccidentRecordDTO> CreateAsync(CreateCarInsuranceAccidentRecordDTO createCarInsuranceAccidentRecordDTO)
		{
			var damageRecord = _mapper.Map<CarInsuranceAccidentRecord>(createCarInsuranceAccidentRecordDTO);
			damageRecord.Id = await _idGeneratorService.GenerateCarInsuranceAccidentRecordIdAsync();
			await _damageRecordRepository.AddAsync(damageRecord);
			return _mapper.Map<CarInsuranceAccidentRecordDTO>(damageRecord);
		}

		/// <summary>
		/// Získá všechny záznamy o nehodách podle ID pojištění.
		/// </summary>
		/// <param name="insuranceId">ID pojištění, podle kterého se záznamy o nehodách vyhledávají.</param>
		/// <returns>Seznam záznamů o nehodách jako DTO.</returns>
		public async Task<IEnumerable<CarInsuranceAccidentRecordDTO>> GetByInsuranceIdAsync(string insuranceId)
		{
			var damageRecords = await _damageRecordRepository.GetByInsuranceIdAsync(insuranceId);
			return _mapper.Map<List<CarInsuranceAccidentRecordDTO>>(damageRecords);
		}

		/// <summary>
		/// Aktualizuje existující záznam o nehodě podle jeho ID.
		/// </summary>
		/// <param name="id">ID záznamu o nehodě.</param>
		/// <param name="updateCarInsuranceAccidentRecordDTO">DTO s daty pro aktualizaci záznamu o nehodě.</param>
		/// <returns><c>true</c>, pokud byla aktualizace úspěšná; jinak <c>false</c>.</returns>
		public async Task<bool> UpdateAsync(string id, UpdateCarInsuranceAccidentRecordDTO updateCarInsuranceAccidentRecordDTO)
		{
			var existingDamageRecord = await _damageRecordRepository.GetByIdAsync(id);
			if (existingDamageRecord == null)
			{
				return false;
			}

			_mapper.Map(updateCarInsuranceAccidentRecordDTO, existingDamageRecord);
			await _damageRecordRepository.UpdateAsync(existingDamageRecord);
			return true;
		}
	}
}
