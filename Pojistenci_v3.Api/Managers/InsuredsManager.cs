using AutoMapper;
using Pojistenci_v3.Api.Interfaces;
using Pojistenci_v3.Common.ModelsDTO;
using Pojistenci_v3.Data.Interfaces;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Manager pro správu operací s pojištěnými osobami.
	/// Dědí základní CRUD operace z <see cref="Manager{TEntity, TDTO}"/>.
	/// </summary>
	public class InsuredsManager : Manager<Insured, InsuredDTO>, IInsuredsManager
	{
		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="InsuredsManager"/>.
		/// </summary>
		/// <param name="repository">Repozitář pro přístup k entitě <see cref="Insured"/>.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		public InsuredsManager(IRepository<Insured> repository, IMapper mapper) : base(repository, mapper)
		{

		}
	}
}
