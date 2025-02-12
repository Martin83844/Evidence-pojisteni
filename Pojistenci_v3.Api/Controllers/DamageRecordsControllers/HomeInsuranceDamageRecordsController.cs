using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDamageRecordDTOs;

namespace Pojistenci_v3.Api.Controllers.DamageRecordsControllers
{
	/// <summary>
	/// Kontroler pro správu záznamů o škodách souvisejících s pojištěním domácností.
	/// Poskytuje CRUD operace pro manipulaci s těmito záznamy.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class HomeInsuranceDamageRecordsController : ControllerBase
	{
		private readonly IHomeInsuranceDamageRecordManager _homeInsuranceDamageRecordManager;

		/// <summary>
		/// Inicializuje novou instanci <see cref="HomeInsuranceDamageRecordsController"/>.
		/// </summary>
		/// <param name="homeInsuranceDamageRecordManager">Instance manažeru pro práci se záznamy o škodách.</param>
		public HomeInsuranceDamageRecordsController(IHomeInsuranceDamageRecordManager homeInsuranceDamageRecordManager)
		{
			_homeInsuranceDamageRecordManager = homeInsuranceDamageRecordManager;
		}

		/// <summary>
		/// Získá všechny záznamy o škodách.
		/// </summary>
		/// <returns>Seznam všech záznamů jako DTO.</returns>
		/// <response code="200">Seznam záznamů úspěšně získán.</response>
		// GET: api/HomeInsuranceDamageRecord
		[HttpGet]
		public async Task<IActionResult> GetAllHomeInsuranceDamageRecords()
		{
			var damageRecords = await _homeInsuranceDamageRecordManager.GetAllAsync();
			return Ok(damageRecords);
		}


		/// <summary>
		/// Získá konkrétní záznam o škodě podle ID.
		/// </summary>
		/// <param name="id">ID záznamu o škodě.</param>
		/// <returns>DTO záznamu nebo <c>404 Not Found</c>, pokud záznam neexistuje.</returns>
		/// <response code="200">Záznam nalezen a vrácen.</response>
		/// <response code="404">Záznam nenalezen.</response>
		// GET: api/HomeInsuranceDamageRecord/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetHomeInsuranceDamageRecordById(string id)
		{
			var damageRecord = await _homeInsuranceDamageRecordManager.GetByIdAsync(id);

			if (damageRecord == null)
			{
				return NotFound(new { Message = "Událost nenalezena." });
			}
			return Ok(damageRecord);
		}

		/// <summary>
		/// Získá všechny záznamy o škodách spojené s konkrétním pojištěním domácnosti.
		/// </summary>
		/// <param name="insuranceId">ID pojištění domácnosti.</param>
		/// <returns>Seznam záznamů o škodách jako DTO.</returns>
		/// <response code="200">Seznam záznamů úspěšně získán.</response>
		// GET: api/HomeInsuranceDamageRecord/insurance/{insuranceId}
		[HttpGet("insurance/{insuranceId}")]
		public async Task<IActionResult> GetDamageRecordsByInsuranceId(string insuranceId)
		{
			var damageRecords = await _homeInsuranceDamageRecordManager.GetByInsuranceIdAsync(insuranceId);
			return Ok(damageRecords);
		}

		/// <summary>
		/// Vytvoří nový záznam o škodě.
		/// </summary>
		/// <param name="createHomeInsuranceDamageRecordDTO">DTO s daty pro vytvoření nového záznamu.</param>
		/// <returns>DTO vytvořeného záznamu.</returns>
		/// <response code="201">Záznam úspěšně vytvořen.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		// POST: api/HomeInsuranceDamageRecord
		[HttpPost]
		public async Task<IActionResult> CreateHomeInsuranceDamageRecord([FromBody] CreateHomeInsuranceDamageRecordDTO createHomeInsuranceDamageRecordDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var damageRecord = await _homeInsuranceDamageRecordManager.CreateAsync(createHomeInsuranceDamageRecordDTO);

			return CreatedAtAction(nameof(GetHomeInsuranceDamageRecordById), new { id = damageRecord.Id }, damageRecord);
		}

		/// <summary>
		/// Aktualizuje záznam o škodě podle ID.
		/// </summary>
		/// <param name="id">ID záznamu, který má být aktualizován.</param>
		/// <param name="updateHomeInsuranceDamageRecordDTO">DTO s daty pro aktualizaci záznamu.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byla aktualizace úspěšná,
		/// nebo <c>404 Not Found</c>, pokud záznam neexistuje.
		/// </returns>
		/// <response code="200">Záznam úspěšně aktualizován.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="404">Záznam nenalezen.</response>
		// PUT: api/HomeInsuranceDamageRecord/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> EditHomeInsuranceDamageRecord(string id, [FromBody] UpdateHomeInsuranceDamageRecordDTO updateHomeInsuranceDamageRecordDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var success = await _homeInsuranceDamageRecordManager.UpdateAsync(id, updateHomeInsuranceDamageRecordDTO);
			if (!success)
			{
				return NotFound(new { Message = "Událost nenalezena." });
			}

			return Ok(new { Message = "Událost byla úspěšně aktualizována." });
		}

		/// <summary>
		/// Smaže záznam o škodě podle ID.
		/// </summary>
		/// <param name="id">ID záznamu, který má být smazán.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byl záznam úspěšně smazán,
		/// nebo <c>404 Not Found</c>, pokud záznam neexistuje.
		/// </returns>
		/// <response code="200">Záznam úspěšně smazán.</response>
		/// <response code="404">Záznam nenalezen.</response>
		// DELETE: api/HomeInsuranceDamageRecord/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDamageRecord(string id)
		{
			var success = await _homeInsuranceDamageRecordManager.DeleteAsync(id);
			if (!success)
			{
				return NotFound(new { Message = "Událost nenalezena." });
			}

			return Ok(new { Message = "Událost byla úspěšně smazána." });
		}
	}
}
