using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Services.Interfaces
{
    public interface IBookService : IBaseService<Book, BookDto>
    {
        Task CreateBookAsync(BookDto entityDto, int mainAuthorId, int publishingHouseId);
        Task<bool> CheckDepentEntities(int mainAuthorId, int publishingHouseId);
    }
}
