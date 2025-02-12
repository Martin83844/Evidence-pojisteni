using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.CArInsuranceDTOs;
using System.Security.Claims;

namespace Pojistenci_v3.Api.Controllers.InsurancesControllers
{
	/// <summary>
	/// Kontroler pro správu pojištění vozidel (Car Insurances).
	/// Poskytuje CRUD operace a další funkce pro manipulaci s pojištěními vozidel.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CarInsurancesController : ControllerBase
	{
		private readonly ICarInsuranceManager _carInsurancesManager;

		/// <summary>
		/// Inicializuje novou instanci <see cref="CarInsurancesController"/>.
		/// </summary>
		/// <param name="carInsurancesManager">Instance manažeru pro práci s pojištěními vozidel.</param>
		public CarInsurancesController(ICarInsuranceManager carInsurancesManager)
		{
			_carInsurancesManager = carInsurancesManager;
		}

		/// <summary>
		/// Získá všechna pojištění vozidel.
		/// </summary>
		/// <returns>Seznam všech pojištění vozidel jako DTO.</returns>
		/// <response code="200">Seznam pojištění vozidel byl úspěšně získán.</response>
		// GET: api/CarInsurance
		[HttpGet]
		public async Task<IActionResult> GetAllCarInsurances()
		{
			var carInsurances = await _carInsurancesManager.GetAllAsync();
			return Ok(carInsurances);
		}

		/// <summary>
		/// Získá konkrétní pojištění vozidla podle ID.
		/// </summary>
		/// <param name="id">ID pojištění.</param>
		/// <returns>DTO pojištění vozidla nebo <c>404 Not Found</c>, pokud pojištění neexistuje.</returns>
		/// <response code="200">Pojištění nalezeno a vráceno.</response>
		/// <response code="404">Pojištění nenalezeno.</response>
		// GET: api/CarInsurance/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCarInsuranceById(string id)
		{
			var carInsurance = await _carInsurancesManager.GetByIdAsync(id);

			if (carInsurance == null)
			{
				return NotFound(new { Message = "Pojištění nenalezeno." });
			}
			return Ok(carInsurance);
		}

		/// <summary>
		/// Získá všechna pojištění vozidel pro konkrétního pojištěného uživatele.
		/// </summary>
		/// <param name="insuredId">ID pojištěného uživatele.</param>
		/// <returns>Seznam pojištění vozidel jako DTO.</returns>
		/// <response code="200">Seznam pojištění úspěšně získán.</response>
		// GET: api/CarInsurance/insured/{insuredId}
		[HttpGet("insured/{insuredId}")]
		public async Task<IActionResult> GetCarInsurancesByInsuredId(string insuredId)
		{
			// Získání pojištění patřícího konkrétnímu pojištěnému
			var carInsurances = await _carInsurancesManager.GetByInsuredIdAsync(insuredId);
			return Ok(carInsurances);
		}

		/// <summary>
		/// Získá všechna pojištění vozidel pro konkrétního pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění vozidel jako DTO.</returns>
		/// <response code="200">Seznam pojištění úspěšně získán.</response>
		// GET: api/CarInsurance/insurer/{insurerId}
		[HttpGet("insurer/{insurerId}")]
		public async Task<IActionResult> GetCarInsurancesByInsurerId(string insurerId)
		{
			// Získání pojištění patřícího konkrétnímu pojištěnému
			var carInsurances = await _carInsurancesManager.GetByInsurerIdAsync(insurerId);
			return Ok(carInsurances);
		}

		/// <summary>
		/// Vytvoří nové pojištění vozidla.
		/// </summary>
		/// <param name="createCarInsuranceDTO">DTO s daty pro vytvoření nového pojištění vozidla.</param>
		/// <returns>DTO vytvořeného pojištění.</returns>
		/// <response code="201">Pojištění úspěšně vytvořeno.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="401">Uživatel není přihlášen.</response>
		// POST: api/CarInsurance
		[HttpPost]
		public async Task<IActionResult> CreateCarInsurance([FromBody] CreateCarInsuranceDTO createCarInsuranceDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Získání aktuálního uživatele z kontextu
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null)
			{
				return Unauthorized("Uživatel není přihlášen.");
			}

			var createdInsurance = await _carInsurancesManager.CreateAsync(createCarInsuranceDTO, userId);

			return CreatedAtAction(nameof(GetCarInsuranceById), new { id = createdInsurance.Id }, createdInsurance);
		}

		/// <summary>
		/// Aktualizuje pojištění vozidla podle ID.
		/// </summary>
		/// <param name="id">ID pojištění, které má být aktualizováno.</param>
		/// <param name="updateCarInsuranceDTO">DTO s daty pro aktualizaci pojištění.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byla aktualizace úspěšná,
		/// nebo <c>404 Not Found</c>, pokud pojištění neexistuje.
		/// </returns>
		/// <response code="200">Pojištění úspěšně aktualizováno.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="404">Pojištění nenalezeno.</response>
		// PUT: api/CarInsurance/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> EditCarInsurance(string id, [FromBody] UpdateCarInsuranceDTO updateCarInsuranceDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var success = await _carInsurancesManager.UpdateAsync(id, updateCarInsuranceDTO);
			if (!success)
			{
				return NotFound(new { Message = "Pojištění nenalezeno." });
			}

			return Ok(new { Message = "Pojištění bylo úspěšně aktualizováno." });
		}

		/// <summary>
		/// Smaže pojištění vozidla podle ID.
		/// </summary>
		/// <param name="id">ID pojištění, které má být smazáno.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud bylo pojištění úspěšně smazáno,
		/// nebo <c>404 Not Found</c>, pokud pojištění neexistuje.
		/// </returns>
		/// <response code="200">Pojištění úspěšně smazáno.</response>
		/// <response code="404">Pojištění nenalezeno.</response>
		// DELETE: api/CarInsurance/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCarInsurance(string id)
		{
			var success = await _carInsurancesManager.DeleteAsync(id);
			if (!success)
			{
				return NotFound(new { Message = "Pojištění nenalezeno." });
			}

			return Ok(new { Message = "Pojištění bylo úspěšně smazáno." });
		}
	}
}
