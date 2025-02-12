using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO;

namespace Pojistenci_v3.Api.Controllers
{
	/// <summary>
	/// Kontroler pro správu pojišťovatelů (Insurers).
	/// Poskytuje CRUD operace pro manipulaci s pojišťovateli.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class InsurersController : ControllerBase
	{
		private readonly IInsurersManager _insurersManager;

		/// <summary>
		/// Inicializuje novou instanci <see cref="InsurersController"/>.
		/// </summary>
		/// <param name="insurersManager">Instance manažeru pro práci s pojišťovateli.</param>
		public InsurersController(IInsurersManager insurersManager)
		{
			_insurersManager = insurersManager;
		}

		/// <summary>
		/// Získá všechny pojišťovatele z databáze.
		/// </summary>
		/// <returns>Seznam všech pojišťovatelů jako <see cref="InsurerDTO"/>.</returns>
		// GET: api/Insurers
		[HttpGet]
		public async Task<IActionResult> GetAllInsurers()
		{
			var insurersDTOs = await _insurersManager.GetAllAsync();
			return Ok(insurersDTOs);
		}

		/// <summary>
		/// Získá pojišťovatele podle jeho ID.
		/// </summary>
		/// <param name="id">ID pojišťovatele.</param>
		/// <returns>DTO pojišťovatele nebo <c>404 Not Found</c>, pokud pojišťovatel neexistuje.</returns>
		/// <response code="200">Pojišťovatel nalezen a vrácen.</response>
		/// <response code="404">Pojišťovatel s daným ID nebyl nalezen.</response>
		// GET: api/Insurers/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetInsurerById(string id)
		{
			// Načtení konkrétního pojistitele z databáze
			var insurerDTO = await _insurersManager.GetByIdAsync(id);

			if (insurerDTO == null)
			{
				return NotFound(new { Message = "Pojistitel nenalezen." });
			}
			return Ok(insurerDTO);
		}

		/// <summary>
		/// Aktualizuje existujícího pojišťovatele na základě jeho ID.
		/// </summary>
		/// <param name="id">ID pojišťovatele, který má být aktualizován.</param>
		/// <param name="insurerDTO">Data pro aktualizaci pojišťovatele.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byla aktualizace úspěšná, 
		/// nebo <c>404 Not Found</c>, pokud pojišťovatel neexistuje.
		/// </returns>
		/// <response code="200">Pojišťovatel úspěšně aktualizován.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="404">Pojišťovatel nenalezen.</response>
		// PUT: api/Insurers/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> EditInsurer(string id, [FromBody] InsurerDTO insurerDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var success = await _insurersManager.UpdateAsync(id, insurerDTO);
			if (!success)
			{
				return NotFound(new { Message = "Pojistitel nenalezen." });
			}
			return Ok(new { Message = "Pojistitel byl úspěšně aktualizován." });
		}

		/// <summary>
		/// Smaže pojišťovatele na základě jeho ID.
		/// </summary>
		/// <param name="id">ID pojišťovatele, který má být smazán.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byl pojišťovatel úspěšně smazán,
		/// nebo <c>404 Not Found</c>, pokud pojišťovatel neexistuje.
		/// </returns>
		/// <response code="200">Pojišťovatel úspěšně smazán.</response>
		/// <response code="404">Pojišťovatel nenalezen.</response>
		// DELETE: api/Insurers/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteInsurer(string id)
		{
			var success = await _insurersManager.DeleteAsync(id);
			if (!success)
			{
				return NotFound(new { Message = "Pojistitel nenalezen." });
			}
			return Ok(new { Message = "Pojistitel byl úspěšně smazán." });
		}
	}
}
