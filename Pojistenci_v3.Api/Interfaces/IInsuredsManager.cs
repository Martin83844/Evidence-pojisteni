using Pojistenci_v3.Common.ModelsDTO;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu operací s pojištěnými osobami.
	/// Dědí základní CRUD operace z <see cref="IManager{TEntity, TDTO}"/>.
	/// </summary>
	public interface IInsuredsManager : IManager<Insured, InsuredDTO>
	{
		// Možnost přidání specifických metod pro správu pojištěných osob.
	}
}
