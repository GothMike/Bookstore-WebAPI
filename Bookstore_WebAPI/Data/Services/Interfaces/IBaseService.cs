using Bookstore_WebAPI.Data.Models;

namespace Bookstore_WebAPI.Data.Services
{
    /// <summary>
    /// Интерфейс для сервиса, работающего с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности, с которой работает сервис.</typeparam>
    /// <typeparam name="TEntityDto">Тип DTO, используемый для маппинга сущностей.</typeparam>
    public interface IBaseService<TEntity, TEntityDto>
    {
        /// <summary>
        /// Обновляет данные сущности в базе данных.
        /// </summary>
        /// <param name="entityDto">DTO сущности с новыми данными.</param>
        /// <param name="entity">Сущность с текущими данными в базе данных.</param>
        /// <returns>Асинхронная задача.</returns>
        Task Update(TEntityDto entityDto, TEntity entity);
        /// <summary>
        /// Асинхронно удаляет указанную сущность из репозитория.
        /// </summary>
        /// <param name="entity">Сущность, которую необходимо удалить.</param>
        /// <returns>Асинхронная операция.</returns>
        Task DeleteAsync(TEntity entity);
    }
}
