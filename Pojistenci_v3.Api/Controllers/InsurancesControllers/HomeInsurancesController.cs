using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs;
using System.Security.Claims;

namespace Pojistenci_v3.Api.Controllers.InsurancesControllers
{
	/// <summary>
	/// Kontroler pro správu pojištění domácností.
	/// Poskytuje CRUD operace a další funkce pro manipulaci s pojištěními domácností.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class HomeInsurancesController : ControllerBase
	{
		private readonly IHomeInsuranceManager _homeInsurancesManager;

		/// <summary>
		/// Inicializuje novou instanci <see cref="HomeInsurancesController"/>.
		/// </summary>
		/// <param name="homeInsurancesManager">Instance manažeru pro práci s pojištěními domácností.</param>
		public HomeInsurancesController(IHomeInsuranceManager homeInsurancesManager)
		{
			_homeInsurancesManager = homeInsurancesManager;
		}

		/// <summary>
		/// Získá všechna pojištění domácností.
		/// </summary>
		/// <returns>Seznam všech pojištění domácností jako DTO.</returns>
		/// <response code="200">Pojištění úspěšně získána.</response>
		// GET: api/HomeInsurance
		[HttpGet]
		public async Task<IActionResult> GetAllHomeInsurances()
		{
			var homeInsurances = await _homeInsurancesManager.GetAllAsync();
			return Ok(homeInsurances);
		}

		/// <summary>
		/// Získá konkrétní pojištění domácnosti podle ID.
		/// </summary>
		/// <param name="id">ID pojištění.</param>
		/// <returns>DTO pojištění nebo <c>404 Not Found</c>, pokud pojištění neexistuje.</returns>
		/// <response code="200">Pojištění nalezeno a vráceno.</response>
		/// <response code="404">Pojištění nenalezeno.</response>
		// GET: api/HomeInsurance/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetHomeInsuranceById(string id)
		{
			var homeInsurance = await _homeInsurancesManager.GetByIdAsync(id);

			if (homeInsurance == null)
			{
				return NotFound(new { Message = "Pojištění nenalezeno." });
			}
			return Ok(homeInsurance);
		}

		/// <summary>
		/// Získá všechna pojištění domácností pro konkrétního pojištěného uživatele.
		/// </summary>
		/// <param name="insuredId">ID pojištěného uživatele.</param>
		/// <returns>Seznam pojištění domácností jako DTO.</returns>
		/// <response code="200">Seznam pojištění úspěšně získán.</response>
		// GET: api/HomeInsurance/insured/{insuredId}
		[HttpGet("insured/{insuredId}")]
		public async Task<IActionResult> GetHomeInsurancesByInsuredId(string insuredId)
		{
			// Získání pojištění patřícího konkrétnímu pojištěnému
			var homeInsurances = await _homeInsurancesManager.GetByInsuredIdAsync(insuredId);
			return Ok(homeInsurances);
		}

		/// <summary>
		/// Získá všechna pojištění domácností pro konkrétního pojistitele.
		/// </summary>
		/// <param name="insurerId">ID pojistitele.</param>
		/// <returns>Seznam pojištění domácností jako DTO.</returns>
		/// <response code="200">Seznam pojištění úspěšně získán.</response>
		// GET: api/HomeInsurance/insurer/{insurerId}
		[HttpGet("insurer/{insurerId}")]
		public async Task<IActionResult> GetHomeInsurancesByInsurerId(string insurerId)
		{
			// Získání pojištění patřícího konkrétnímu pojištěnému
			var homeInsurances = await _homeInsurancesManager.GetByInsurerIdAsync(insurerId);
			return Ok(homeInsurances);
		}

		/// <summary>
		/// Vytvoří nové pojištění domácnosti.
		/// </summary>
		/// <param name="createHomeInsuranceDTO">DTO s daty pro vytvoření nového pojištění domácnosti.</param>
		/// <returns>DTO vytvořeného pojištění.</returns>
		/// <response code="201">Pojištění úspěšně vytvořeno.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="401">Uživatel není přihlášen.</response>
		// POST: api/HomeInsurance
		[HttpPost]
		public async Task<IActionResult> CreateHomeInsurance([FromBody] CreateHomeInsuranceDTO createHomeInsuranceDTO)
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

			var createdInsurance = await _homeInsurancesManager.CreateAsync(createHomeInsuranceDTO, userId);

			return CreatedAtAction(nameof(GetHomeInsuranceById), new { id = createdInsurance.Id }, createdInsurance);
		}

		/// <summary>
		/// Aktualizuje pojištění domácnosti podle ID.
		/// </summary>
		/// <param name="id">ID pojištění, které má být aktualizováno.</param>
		/// <param name="updateHomeInsuranceDTO">DTO s daty pro aktualizaci pojištění.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byla aktualizace úspěšná, 
		/// nebo <c>404 Not Found</c>, pokud pojištění neexistuje.
		/// </returns>
		/// <response code="200">Pojištění úspěšně aktualizováno.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="404">Pojištění nenalezeno.</response>
		// PUT: api/HomeInsurance/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> EditHomeInsurance(string id, [FromBody] UpdateHomeInsuranceDTO updateHomeInsuranceDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var success = await _homeInsurancesManager.UpdateAsync(id, updateHomeInsuranceDTO);
			if (!success)
			{
				return NotFound(new { Message = "Pojištění nenalezeno." });
			}

			return Ok(new { Message = "Pojištění bylo úspěšně aktualizováno." });
		}

		/// <summary>
		/// Smaže pojištění domácnosti podle ID.
		/// </summary>
		/// <param name="id">ID pojištění, které má být smazáno.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud bylo pojištění úspěšně smazáno,
		/// nebo <c>404 Not Found</c>, pokud pojištění neexistuje.
		/// </returns>
		/// <response code="200">Pojištění úspěšně smazáno.</response>
		/// <response code="404">Pojištění nenalezeno.</response>
		// DELETE: api/HomeInsurance/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteHomeInsurance(string id)
		{
			var success = await _homeInsurancesManager.DeleteAsync(id);
			if (!success)
			{
				return NotFound(new { Message = "Pojištění nenalezeno." });
			}

			return Ok(new { Message = "Pojištění bylo úspěšně smazáno." });
		}
	}
}
