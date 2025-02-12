using AutoMapper;
using Pojistenci_v3.Data.Interfaces;

namespace Pojistenci_v3.Api.Managers
{
	/// <summary>
	/// Generický manager pro CRUD operace.
	/// </summary>
	/// <typeparam name="TEntity">Typ entity v databázi.</typeparam>
	/// <typeparam name="TDTO">Typ DTO, které reprezentuje entitu v API.</typeparam>
	public class Manager<TEntity, TDTO> where TEntity : class where TDTO : class
	{
		private readonly IRepository<TEntity> _repository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="Manager{TEntity, TDTO}"/>.
		/// </summary>
		/// <param name="repository">Generický repozitář pro práci s entitami.</param>
		/// <param name="mapper">AutoMapper pro mapování mezi entitami a DTO.</param>
		public Manager(IRepository<TEntity> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		/// <summary>
		/// Získá všechny entity z databáze a převede je na DTO.
		/// </summary>
		/// <returns>Seznam všech entit jako DTO.</returns>
		public async Task<IEnumerable<TDTO>> GetAllAsync()
		{
			var entities = await _repository.GetAllAsync();
			return _mapper.Map<List<TDTO>>(entities);
		}

		/// <summary>
		/// Získá konkrétní entitu podle jejího ID a převede ji na DTO.
		/// </summary>
		/// <param name="id">ID entity.</param>
		/// <returns>DTO odpovídající entitě nebo <c>null</c>, pokud nebyla nalezena.</returns>
		public virtual async Task<TDTO?> GetByIdAsync(string id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return null;
			}
			return _mapper.Map<TDTO?>(entity);
		}

		/// <summary>
		/// Aktualizuje existující entitu na základě ID a dat v DTO.
		/// </summary>
		/// <param name="id">ID entity, která má být aktualizována.</param>
		/// <param name="dto">Data pro aktualizaci entity.</param>
		/// <returns><c>true</c>, pokud byla entita úspěšně aktualizována; jinak <c>false</c>.</returns>
		public async Task<bool> UpdateAsync(string id, TDTO dto)
		{
			var existingEntity = await _repository.GetByIdAsync(id);
			if (existingEntity == null)
			{
				return false;
			}
			_mapper.Map(dto, existingEntity);
			await _repository.UpdateAsync(existingEntity);
			return true;
		}

		/// <summary>
		/// Smaže entitu z databáze podle jejího ID.
		/// </summary>
		/// <param name="id">ID entity, která má být smazána.</param>
		/// <returns><c>true</c>, pokud byla entita úspěšně smazána; jinak <c>false</c>.</returns>
		public async Task<bool> DeleteAsync(string id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return false;
			}
			await _repository.DeleteAsync(id);
			return true;
		}
	}
}
