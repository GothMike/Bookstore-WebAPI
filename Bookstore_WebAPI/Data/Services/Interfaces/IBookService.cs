using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Services.Interfaces
{
    /// <summary>
    /// Интерфейс, который определяет методы для работы со сущностями книг.
    /// </summary>
    public interface IBookService : IBaseService<Book, BookDto>, IBaseAbstractService<Book, BookDto>
    {
        /// <summary>
        /// Асинхронно создает новую книгу на основе DTO объекта, идентификатора главного автора и издательства.
        /// </summary>
        /// <param name="entityDto">DTO объект с данными новой книги.</param>
        /// <param name="mainAuthorId">Идентификатор главного автора книги.</param>
        /// <param name="publishingHouseId">Идентификатор издательства книги.</param>
        /// <returns>Асинхронная операция.</returns>
        Task CreateBookAsync(BookDto entityDto, int mainAuthorId, int publishingHouseId);

        /// <summary>
        /// Асинхронно проверяет зависимые сущности главного автора и издательства книги по их идентификаторам.
        /// </summary>
        /// <param name="mainAuthorId">Идентификатор главного автора книги.</param>
        /// <param name="publishingHouseId">Идентификатор издательства книги.</param>
        /// <returns>True, если зависимые сущности найдены, иначе - false.</returns>
        Task<bool> CheckDepentEntities(int mainAuthorId, int publishingHouseId);
    }
}
