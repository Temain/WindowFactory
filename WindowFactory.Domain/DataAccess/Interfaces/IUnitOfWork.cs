using System.Collections.Generic;
using WindowFactory.Domain.DataAccess.Repositories;

namespace WindowFactory.Domain.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IEnumerable<T> Execute<T>(string query, params object[] parameters);
        void Save();
        void Dispose(bool disposing);
        void Dispose();
    }
}