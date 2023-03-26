using Bookstore_WebAPI.Data.Repository.Interfaces;

namespace Bookstore_WebAPI.Persistence.Factory.Factory.Interfaces
{
    /// <summary>
    /// Интерфейс фабрики репозиториев, который определяет метод для получения репозитория для указанного типа сущности.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Возвращает репозиторий указанного типа сущности.
        /// </summary>
        /// <typeparam name="T">Тип сущности, для которой нужен репозиторий.</typeparam>
        /// <returns>Репозиторий указанного типа сущности.</returns>
        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
