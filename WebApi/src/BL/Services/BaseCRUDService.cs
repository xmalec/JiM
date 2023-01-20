using AutoMapper;
using DAL.Models;
using DAL.Repositories;
using Extensions.Extensions;

namespace BL.Services
{
    public class BaseCRUDService<TEntity, TEntityDTO>
        : IBaseCRUDService<TEntity, TEntityDTO> where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> repository;
        protected readonly IMapper mapper;

        public BaseCRUDService(IBaseRepository<TEntity> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Add(TEntityDTO newEntityDTO)
        {
            var entity = newEntityDTO.Map<TEntity>();
            await repository.Insert(entity);
            return entity.Id;
        }

        public IEnumerable<TEntityDTO> GetAll()
        {
            return repository
                    .Query()
                    .Map<IEnumerable<TEntityDTO>>();
        }

        public TEntityDTO GetById(int id)
        {
            return repository
                    .GetById(id)
                    .Map<TEntityDTO>();
        }

        public Task Update(TEntityDTO updateEntityDTO)
        {
            return repository
                    .Update(updateEntityDTO.Map<TEntity>());
        }

        public Task Delete(int id)
        {
            return repository
                    .Delete(id);
        }

    }
}
