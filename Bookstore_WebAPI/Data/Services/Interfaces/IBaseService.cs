namespace Bookstore_WebAPI.Data.Services
{
    public interface IBaseService<TEntity, TMapEntity>
    {

        Task<IEnumerable<TMapEntity>> GetAllAsync();
        Task<TMapEntity> GetMapEntityByIdAsync(int id);
        Task<TEntity> GetEntityByIdAsync(int id);
        Task Update(TMapEntity entityDto, TEntity entity);
        Task DeleteAsync(TEntity entity);
        TEntity ConvertToMapEntity(TMapEntity entityDto);
    }
}
