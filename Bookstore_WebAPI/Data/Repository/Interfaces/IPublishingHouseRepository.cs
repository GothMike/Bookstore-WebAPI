using Bookstore_WebAPI.Data.Models;

namespace Bookstore_WebAPI.Data.Repository.Interfaces
{
    /// <summary>
    /// Интерфейс, который определяет методы для работы с репозиторием издательств.
    /// </summary>
    public interface IPublishingHouseRepository
    {
        /// <summary>
        /// Асинхронно получает информацию об авторе и его издательствах по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора.</param>
        /// <returns>Объект с информацией об авторе и его издательствах.</returns>
        Task<AuthorPublishingHouses> GetAPHById(int id);
    }
}
