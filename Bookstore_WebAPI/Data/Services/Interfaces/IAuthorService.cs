using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Models.Dto;

namespace Bookstore_WebAPI.Data.Services.Interfaces
{
    public interface IAuthorService : IBaseService<Author, AuthorDto>
    {
        Task CreateAuthorAsync(AuthorDto entityDto);
        Task<IEnumerable<BookDto>> GetAllMappingAuthorBooks(int id);
    }
}
