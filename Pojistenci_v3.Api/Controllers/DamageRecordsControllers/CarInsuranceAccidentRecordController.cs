using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.CarInsuranceAccidentRecordDTOs;

namespace Pojistenci_v3.Api.Controllers.DamageRecordsControllers
{
	/// <summary>
	/// Kontroler pro správu záznamů o nehodách vozidel pojištěných prostřednictvím pojištění vozidel.
	/// Poskytuje CRUD operace pro manipulaci s těmito záznamy.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CarInsuranceAccidentRecordController : ControllerBase
	{
		private readonly ICarInsuranceAccidentRecordManager _carInsuranceAccidentRecordManager;

		/// <summary>
		/// Inicializuje novou instanci <see cref="CarInsuranceAccidentRecordController"/>.
		/// </summary>
		/// <param name="carInsuranceAccidentRecordManager">Instance manažeru pro práci se záznamy o nehodách vozidel.</param>
		public CarInsuranceAccidentRecordController(ICarInsuranceAccidentRecordManager carInsuranceAccidentRecordManager)
		{
			_carInsuranceAccidentRecordManager = carInsuranceAccidentRecordManager;
		}

		/// <summary>
		/// Získá všechny záznamy o nehodách vozidel.
		/// </summary>
		/// <returns>Seznam všech záznamů jako DTO.</returns>
		/// <response code="200">Seznam záznamů úspěšně získán.</response>
		// GET: api/CarInsuranceAccidentRecord
		[HttpGet]
		public async Task<IActionResult> GetAllCarInsuranceAccidentRecords()
		{
			var damageRecords = await _carInsuranceAccidentRecordManager.GetAllAsync();
			return Ok(damageRecords);
		}


		/// <summary>
		/// Získá konkrétní záznam o nehodě podle ID.
		/// </summary>
		/// <param name="id">ID záznamu o nehodě.</param>
		/// <returns>DTO záznamu nebo <c>404 Not Found</c>, pokud záznam neexistuje.</returns>
		/// <response code="200">Záznam nalezen a vrácen.</response>
		/// <response code="404">Záznam nenalezen.</response>
		// GET: api/CarInsuranceAccidentRecord/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCarInsuranceAccidentRecordById(string id)
		{
			var damageRecord = await _carInsuranceAccidentRecordManager.GetByIdAsync(id);

			if (damageRecord == null)
			{
				return NotFound(new { Message = "Událost nenalezena." });
			}
			return Ok(damageRecord);
		}

		/// <summary>
		/// Získá všechny záznamy o nehodách spojené s konkrétním pojištěním vozidla.
		/// </summary>
		/// <param name="insuranceId">ID pojištění vozidla.</param>
		/// <returns>Seznam záznamů o nehodách jako DTO.</returns>
		/// <response code="200">Seznam záznamů úspěšně získán.</response>
		// GET: api/CarInsuranceAccidentRecord/insurance/{insuranceId}
		[HttpGet("insurance/{insuranceId}")]
		public async Task<IActionResult> GetAccidentRecordsByInsuranceId(string insuranceId)
		{
			// Získání pojištění patřícího konkrétnímu pojištěnému
			var damageRecords = await _carInsuranceAccidentRecordManager.GetByInsuranceIdAsync(insuranceId);
			return Ok(damageRecords);
		}

		/// <summary>
		/// Vytvoří nový záznam o nehodě vozidla.
		/// </summary>
		/// <param name="createCarInsuranceAccidentRecordDTO">DTO s daty pro vytvoření nového záznamu.</param>
		/// <returns>DTO vytvořeného záznamu.</returns>
		/// <response code="201">Záznam úspěšně vytvořen.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		// POST: api/CarInsuranceAccidentRecord
		[HttpPost]
		public async Task<IActionResult> CreateCarInsuranceAccidentRecord([FromBody] CreateCarInsuranceAccidentRecordDTO createCarInsuranceAccidentRecordDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var damageRecord = await _carInsuranceAccidentRecordManager.CreateAsync(createCarInsuranceAccidentRecordDTO);

			return CreatedAtAction(nameof(GetCarInsuranceAccidentRecordById), new { id = damageRecord.Id }, damageRecord);
		}

		/// <summary>
		/// Aktualizuje záznam o nehodě vozidla podle ID.
		/// </summary>
		/// <param name="id">ID záznamu, který má být aktualizován.</param>
		/// <param name="updateCarInsuranceAccidentRecordDTO">DTO s daty pro aktualizaci záznamu.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byla aktualizace úspěšná,
		/// nebo <c>404 Not Found</c>, pokud záznam neexistuje.
		/// </returns>
		/// <response code="200">Záznam úspěšně aktualizován.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="404">Záznam nenalezen.</response>
		// PUT: api/CarInsuranceAccidentRecord/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> EditCarInsuranceAccidentRecord(string id, [FromBody] UpdateCarInsuranceAccidentRecordDTO updateCarInsuranceAccidentRecordDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var success = await _carInsuranceAccidentRecordManager.UpdateAsync(id, updateCarInsuranceAccidentRecordDTO);
			if (!success)
			{
				return NotFound(new { Message = "Událost nenalezena." });
			}

			return Ok(new { Message = "Událost byla úspěšně aktualizována." });
		}

		/// <summary>
		/// Smaže záznam o nehodě podle ID.
		/// </summary>
		/// <param name="id">ID záznamu, který má být smazán.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byl záznam úspěšně smazán,
		/// nebo <c>404 Not Found</c>, pokud záznam neexistuje.
		/// </returns>
		/// <response code="200">Záznam úspěšně smazán.</response>
		/// <response code="404">Záznam nenalezen.</response>
		// DELETE: api/CarInsuranceAccidentRecord/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccidentRecord(string id)
		{
			var success = await _carInsuranceAccidentRecordManager.DeleteAsync(id);
			if (!success)
			{
				return NotFound(new { Message = "Událost nenalezena." });
			}

			return Ok(new { Message = "Událost byla úspěšně smazána." });
		}
	}
}
