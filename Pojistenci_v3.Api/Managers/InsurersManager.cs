using AutoMapper;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Manager pro správu operací s pojišťovateli.
	/// Dědí základní CRUD operace z <see cref="Manager{TEntity, TDTO}"/>.
	/// </summary>
	public class InsurersManager : Manager<Insurer, InsurerDTO>, IInsurersManager
	{
		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="InsurersManager"/>.
		/// </summary>
		/// <param name="repository">Repozitář pro přístup k entitě <see cref="Insurer"/>.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		public InsurersManager(IRepository<Insurer> repository, IMapper mapper) : base(repository, mapper)
		{

		}

	}
}