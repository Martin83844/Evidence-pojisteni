using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pojistenci_v3.Common.ModelsDTO;
using Pojistenci_v3.Data.Models;
using System.Security.Claims;

namespace Pojistenci_v3.Api.Controllers
{
	/// <summary>
	/// Kontroler pro správu uživatelských účtů, registrace, přihlašování a odhlašování.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IMapper _mapper;

		/// <summary>
		/// Inicializuje novou instanci <see cref="AccountController"/>.
		/// </summary>
		/// <param name="userManager">Správce uživatelů.</param>
		/// <param name="signInManager">Správce přihlášení uživatelů.</param>
		/// <param name="mapper">Mapper pro převod mezi entitami a DTO.</param>
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		/// <summary>
		/// Registruje nového pojištěnce.
		/// </summary>
		/// <param name="dto">DTO obsahující informace o pojištěnci.</param>
		/// <returns>Informace o registrovaném pojištěnci.</returns>
		/// <response code="200">Pojištěnec úspěšně registrován.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		// POST: api/Account/register-insured
		[HttpPost("register-insured")]
		public async Task<IActionResult> RegisterInsured([FromBody] RegisterInsuredDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Mapování DTO na Insured entitu
			var insured = _mapper.Map<Insured>(dto);
			insured.Role = UserRole.Insured;

			// Vytvoření uživatele přes UserManager
			var result = await _userManager.CreateAsync(insured, dto.Password);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}

			await _userManager.AddToRoleAsync(insured, UserRole.Insured.ToString());

			return Ok(new { insured.CustomerNumber, insured.Email });
		}

		/// <summary>
		/// Registruje nového pojišťovatele.
		/// </summary>
		/// <param name="dto">DTO obsahující informace o pojišťovateli.</param>
		/// <returns>Informace o registrovaném pojišťovateli.</returns>
		/// <response code="200">Pojišťovatel úspěšně registrován.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		// POST: api/Account/register-insurer
		[HttpPost("register-insurer")]
		public async Task<IActionResult> RegisterInsurer([FromBody] RegisterInsurerDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Mapování DTO na Insurer entitu
			var insurer = _mapper.Map<Insurer>(dto);
			insurer.Role = UserRole.Insurer;

			// Vytvoření uživatele přes UserManager
			var result = await _userManager.CreateAsync(insurer, dto.Password);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}

			await _userManager.AddToRoleAsync(insurer, UserRole.Insurer.ToString());

			return Ok(new { insurer.Id, insurer.Email });
		}

		/// <summary>
		/// Přihlásí uživatele na základě přihlašovacích údajů.
		/// </summary>
		/// <param name="dto">DTO obsahující e-mail a heslo uživatele.</param>
		/// <returns>Zpráva o úspěšném přihlášení nebo chybě.</returns>
		/// <response code="200">Uživatel úspěšně přihlášen.</response>
		/// <response code="400">Chybná data v požadavku.</response>
		/// <response code="401">Neplatné přihlašovací údaje.</response>
		// POST: api/Account/Login
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				return Unauthorized(new { Message = "Neplatné přihlašovací údaje." });
			}

			var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);
			if (!result.Succeeded)
			{
				return Unauthorized(new { Message = "Neplatné přihlašovací údaje." });
			}

			return Ok(new { Message = "Přihlášení úspěšné." });
		}

		/// <summary>
		/// Odhlásí aktuálního uživatele.
		/// </summary>
		/// <returns>Zpráva o úspěšném odhlášení.</returns>
		/// <response code="200">Uživatel úspěšně odhlášen.</response>
		// POST: api/Account/Logout
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok(new { Message = "Uživatel úspěšně odhlášen." });
		}

		/// <summary>
		/// Získá informace o aktuálně přihlášeném uživateli.
		/// </summary>
		/// <returns>Informace o aktuálním uživateli včetně jeho rolí.</returns>
		/// <response code="200">Informace o uživateli úspěšně získány.</response>
		/// <response code="404">Uživatel nenalezen.</response>
		[Authorize]
		[HttpGet("me")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			var roles = await _userManager.GetRolesAsync(user);

			return Ok(new
			{
				user.Id,
				user.Email,
				Roles = roles
			});
		}
	}
}