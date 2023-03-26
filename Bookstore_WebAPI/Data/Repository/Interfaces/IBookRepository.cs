using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Repository.Interfaces
{
    /// <summary>
    /// Интерфейс, который определяет методы для работы с книжным репозиторием.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Асинхронно получает все книги автора по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора.</param>
        /// <returns>Коллекция книг автора.</returns>
        Task<IEnumerable<Book>> GetAllAuthorsBooks(int id);
    }
}
