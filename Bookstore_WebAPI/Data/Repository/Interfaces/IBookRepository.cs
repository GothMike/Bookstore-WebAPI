using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAuthorsBooks(int id);
        Task<List<Book>> GetAllPublishingHouseBooks(int id);
    }
}
