using Library.DataAccess.Data.Entities;

namespace Library.DataAccess.Data.Repositories
{
    public interface IReadRepository<out TEntity> 
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        string GetLogName();
    }
}