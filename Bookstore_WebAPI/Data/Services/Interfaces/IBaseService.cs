namespace Bookstore_WebAPI.Data.Services
{
    /// <summary>
    /// Интерфейс для сервиса, работающего с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности, с которой работает сервис.</typeparam>
    /// <typeparam name="TMapEntity">Тип DTO, используемый для маппинга сущностей.</typeparam>
    public interface IBaseService<TEntity, TMapEntity>
    {
        /// <summary>
        /// Получает все сущности из базы данных.
        /// </summary>
         /// <returns>Коллекция всех сущностей.</returns>
        Task<IEnumerable<TMapEntity>> GetAllAsync();

        /// <summary>
        /// Получает DTO сущности по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>DTO сущности.</returns>
        Task<TMapEntity> GetMapEntityByIdAsync(int id);

        /// <summary>
        /// Получает сущность по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        Task<TEntity> GetEntityByIdAsync(int id);

        /// <summary>
        /// Обновляет данные сущности в базе данных.
        /// </summary>
        /// <param name="entityDto">DTO сущности с новыми данными.</param>
        /// <param name="entity">Сущность с текущими данными в базе данных.</param>
        /// <returns>Асинхронная задача.</returns>
        Task Update(TMapEntity entityDto, TEntity entity);

        /// <summary>
        /// Удаляет сущность из базы данных.
        /// </summary>
        /// <param name="entity">Сущность, которая должна быть удалена.</param>
        /// <returns>Асинхронная задача.</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Преобразует DTO сущности в сущность.
        /// </summary>
        /// <param name="entityDto">DTO сущности.</param>
        /// <returns>Сущность.</returns>
        TEntity ConvertToMapEntity(TMapEntity entityDto);
    }
}
