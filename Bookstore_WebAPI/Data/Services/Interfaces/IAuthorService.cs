using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Services.Interfaces
{
    /// <summary>
    /// Интерфейс, который определяет методы для работы со сущностями авторов.
    /// </summary>
    public interface IAuthorService : IBaseService<Author, AuthorDto>, IBaseAbstractService<Author, AuthorDto>
    {
        /// <summary>
        /// Асинхронно создает нового автора на основе DTO объекта.
        /// </summary>
        /// <param name="entityDto">DTO объект с данными нового автора.</param>
        /// <returns>Асинхронная операция.</returns>
        Task CreateAuthorAsync(AuthorDto entityDto);

        /// <summary>
        /// Асинхронно получает все книги автора с соответствующей маппинговой информацией по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора.</param>
        /// <returns>Коллекция DTO объектов книг автора.</returns>
        Task<IEnumerable<BookDto>> GetAllMappingAuthorBooks(int id);
    }
}
