using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Persistence.DataContext;
using Bookstore_WebAPI.Data.Models;

namespace Bookstore_WebAPI.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IGenericRepository<Author> _authorGenericRepository;
        private IGenericRepository<Book> _bookGenericRepository;
        private IGenericRepository<PublishingHouse> _publishingHouseGenericRepository;
        private IGenericRepository<AuthorBooks> _authorBooksGenericRepository;
        private IGenericRepository<AuthorPublishingHouses> _authorPublishingHousesRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IGenericRepository<Author> AuthorGenericRepository
        {
            get
            {
                if (_authorGenericRepository == null)
                {
                    _authorGenericRepository = new GenericRepository<Author>(_context);
                }
                return _authorGenericRepository;
            }
        }

        public IGenericRepository<Book> BookGenericRepository
        {
            get
            {
                if (_bookGenericRepository == null)
                {
                    _bookGenericRepository = new GenericRepository<Book>(_context);
                }
                return _bookGenericRepository;
            }
        }

        public IGenericRepository<PublishingHouse> PublishingHouseGenericRepository
        {
            get
            {
                if (_publishingHouseGenericRepository == null)
                {
                    _publishingHouseGenericRepository = new GenericRepository<PublishingHouse>(_context);
                }
                return _publishingHouseGenericRepository;
            }
        }

        public IGenericRepository<AuthorBooks> AuthorBooksGenericRepository
        {
            get
            {
                if (_authorBooksGenericRepository == null)
                {
                    _authorBooksGenericRepository = new GenericRepository<AuthorBooks>(_context);
                }
                return _authorBooksGenericRepository;
            }
        }

        public IGenericRepository<AuthorPublishingHouses> AuthorPublishingHousesRepository
        {
            get
            {
                if (_authorPublishingHousesRepository == null)
                {
                    _authorPublishingHousesRepository = new GenericRepository<AuthorPublishingHouses>(_context);
                }
                return _authorPublishingHousesRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            disposed = true;
        }
    }
}
