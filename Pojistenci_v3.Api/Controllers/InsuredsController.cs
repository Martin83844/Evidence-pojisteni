using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO;

namespace Pojistenci_v3.Api.Controllers
{
	/// <summary>
	/// Kontroler pro správu pojištěnců (Insureds).
	/// Poskytuje CRUD operace pro manipulaci s pojištěnci.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class InsuredsController : ControllerBase
	{
		private readonly IInsuredsManager _insuredsManager;

		/// <summary>
		/// Inicializuje novou instanci <see cref="InsuredsController"/>.
		/// </summary>
		/// <param name="insuredsManager">Instance manažeru pro práci s pojištěnci.</param>
		public InsuredsController(IInsuredsManager insuredsManager)
		{
			_insuredsManager = insuredsManager;
		}

		/// <summary>
		/// Získá všechny pojištěnce z databáze.
		/// </summary>
		/// <returns>Seznam všech pojištěnců jako DTO.</returns>
		/// <response code="200">Seznam pojištěnců úspěšně získán.</response>
		// GET: api/Insureds
		[HttpGet]
		public async Task<IActionResult> GetAllInsureds()
		{
			var insuredsDTO = await _insuredsManager.GetAllAsync();
			return Ok(insuredsDTO);
		}

		/// <summary>
		/// Získá pojištěnce podle jeho ID.
		/// </summary>
		/// <param name="id">ID pojištěnce.</param>
		/// <returns>DTO pojištěnce nebo <c>404 Not Found</c>, pokud pojištěnec neexistuje.</returns>
		/// <response code="200">Pojištěnec nalezen a vrácen.</response>
		/// <response code="404">Pojištěnec nenalezen.</response>
		// GET: api/Insureds/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetInsuredById(string id)
		{
			var insuredDTO = await _insuredsManager.GetByIdAsync(id);

			if (insuredDTO == null)
			{
				return NotFound(new { Message = "Pojištěný nenalezen" });
			}
			return Ok(insuredDTO);
		}

		/// <summary>
		/// Aktualizuje existujícího pojištěnce na základě jeho ID.
		/// </summary>
		/// <param name="id">ID pojištěnce, který má být aktualizován.</param>
		/// <param name="insuredDTO">Data pro aktualizaci pojištěnce.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byla aktualizace úspěšná,
		/// nebo <c>404 Not Found</c>, pokud pojištěnec neexistuje.
		/// </returns>
		/// <response code="200">Pojištěnec úspěšně aktualizován.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="404">Pojištěnec nenalezen.</response>
		// PUT: api/Insureds/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> EditInsured(string id, [FromBody] InsuredDTO insuredDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var succes = await _insuredsManager.UpdateAsync(id, insuredDTO);
			if (!succes)
			{
				return NotFound(new { Message = "Pojištěný nenalezen." });
			}
			return Ok(new { Message = "Pojištěný byl úspěšně aktualizován." });
		}

		/// <summary>
		/// Smaže pojištěnce na základě jeho ID.
		/// </summary>
		/// <param name="id">ID pojištěnce, který má být smazán.</param>
		/// <returns>
		/// <c>200 OK</c>, pokud byl pojištěný úspěšně smazán,
		/// nebo <c>404 Not Found</c>, pokud pojištěnec neexistuje.
		/// </returns>
		/// <response code="200">Pojištěnec úspěšně smazán.</response>
		/// <response code="404">Pojištěnec nenalezen.</response>
		// DELETE: api/Insureds/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteInsured(string id)
		{
			var succes = await _insuredsManager.DeleteAsync(id);
			if (!succes)
			{
				return NotFound(new { Message = "Pojištěný nenalezen." });
			}
			return Ok(new { Message = "Pojištěný byl úspěšně smazán." });
		}
	}
}
