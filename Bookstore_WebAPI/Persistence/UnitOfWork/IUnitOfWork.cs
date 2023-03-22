using Bookstore_WebAPI.Data.Models;
using Bookstore_WebAPI.Data.Repository.Interfaces;

namespace Bookstore_WebAPI.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Author> AuthorGenericRepository { get; }
        IGenericRepository<Book> BookGenericRepository { get; }
        IGenericRepository<PublishingHouse> PublishingHouseGenericRepository { get; }
        IGenericRepository<AuthorBooks> AuthorBooksGenericRepository { get; }
        IGenericRepository<AuthorPublishingHouses> AuthorPublishingHousesRepository { get; }
        IAuthorRepository AuthorRepository { get;  }
        IBookRepository BookRepository { get; }
        IPublishingHouseRepository PublishingHouseRepository { get; }
        Task SaveAsync();
    }
}
