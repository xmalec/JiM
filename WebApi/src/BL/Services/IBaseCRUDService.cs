namespace BL.Services
{
    public interface IBaseCRUDService<TEntity, TEntityDTO> where TEntity : class
    {
        TEntityDTO GetById(int id);

        IEnumerable<TEntityDTO> GetAll();
        Task Delete(int id);

        Task<int> Add(TEntityDTO newEntityDTO);

        Task Update(TEntityDTO updateEntityDTO);
    }
}
