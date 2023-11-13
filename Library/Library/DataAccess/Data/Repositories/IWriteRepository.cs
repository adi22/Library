using Library.DataAccess.Data.Entities;

namespace Library.DataAccess.Data.Repositories
{
    public interface IWriteRepository<in TEntity> 
        where TEntity : class, IEntity
    {
        void Add(TEntity item);
        void Remove(TEntity item);
        void Save();
    }
}