using Library.DataAccess.Data.Entities;

namespace Library.DataAccess.Data.Repositories
{
    public interface IRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity>
        where TEntity : class, IEntity
    {
        public event EventHandler<TEntity>? BookAdded;
        public event EventHandler<TEntity>? BookDeleted;
        public event EventHandler<TEntity>? ChangesSaved;
    }
}