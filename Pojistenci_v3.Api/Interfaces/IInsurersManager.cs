using Pojistenci_v3.Common.ModelsDTO;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Interfaces
{
	/// <summary>
	/// Rozhraní pro správu operací s pojišťovateli.
	/// Dědí základní CRUD operace z <see cref="IManager{TEntity, TDTO}"/>.
	/// </summary>
	public interface IInsurersManager : IManager<Insurer, InsurerDTO>
	{
		// V budoucnu můžete přidat specifické metody pro správu pojišťovatelů.
	}
}
