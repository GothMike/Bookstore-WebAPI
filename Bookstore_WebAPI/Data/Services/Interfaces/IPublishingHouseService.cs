using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Services.Interfaces
{
    /// <summary>
    /// Интерфейс, который определяет методы для работы со сущностями издательств.
    /// </summary>
    public interface IPublishingHouseService : IBaseService<PublishingHouse, PublishingHouseDto>, IBaseAbstractService<PublishingHouse, PublishingHouseDto>
    {
        /// <summary>
        /// Асинхронно создает новое издательство на основе DTO объекта.
        /// </summary>
        /// <param name="entityDto">DTO объект с данными нового издательства.</param>
        /// <returns>Асинхронная операция.</returns>
        Task CreatePublishingHouseAsync(PublishingHouseDto entityDto);
    }
}
