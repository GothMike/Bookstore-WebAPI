using Bookstore_WebAPI.Data.Repository.Interfaces;
using Bookstore_WebAPI.Data.Repository;
using Bookstore_WebAPI.Persistence.DataContext;
using Bookstore_WebAPI.Persistence.Factory.Factory.Interfaces;

namespace Bookstore_WebAPI.Persistence.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public RepositoryFactory(ApplicationContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.TryGetValue(typeof(T), out object repository))
            {
                repository = new GenericRepository<T>(_context);
                _repositories.Add(typeof(T), repository);
            }
            return (IGenericRepository<T>)repository;
        }
    }
}
