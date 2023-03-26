using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Persistence.DataContext;
using Bookstore_WebAPI.Persistence.Factory.Factory.Interfaces;

namespace Bookstore_WebAPI.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        private IAuthorRepository _authorRepository;
        private IBookRepository _bookRepository;
        private IPublishingHouseRepository _publishingHouseRepository;

        public UnitOfWork(ApplicationContext context, IRepositoryFactory repositoryFactory)
        { 
            _context = context; 
            _repositoryFactory = repositoryFactory;
        }

        public IGenericRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            return _repositoryFactory.GetRepository<TEntity>();
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                {
                    _authorRepository = new AuthorRepository(_context);
                }
                return _authorRepository;
            }
        }

        public IBookRepository BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_context);
                }
                return _bookRepository;
            }
        }

        public IPublishingHouseRepository PublishingHouseRepository
        {
            get
            {
                if (_publishingHouseRepository == null)
                {
                    _publishingHouseRepository = new PublishingHouseRepository(_context);
                }
                return _publishingHouseRepository;
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
                    _context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
