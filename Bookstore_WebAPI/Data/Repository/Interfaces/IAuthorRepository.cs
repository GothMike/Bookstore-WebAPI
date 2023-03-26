using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace Bookstore_WebAPI.Data.Repository.Interfaces
{
    /// <summary>
    /// Интерфейс, который определяет методы для работы с репозиторием авторов.
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Асинхронно получает все книги автора по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора.</param>
        /// <returns>Коллекция с информацией об авторе и его книгах.</returns>
        Task<ICollection<AuthorBooks>> GetById(int id);
    }
}
