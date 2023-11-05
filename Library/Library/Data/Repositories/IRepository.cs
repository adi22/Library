using Library.Data.Entities;

namespace Library.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
        public event EventHandler<T>? BookAdded;
        public event EventHandler<T>? BookDeleted;
        public event EventHandler<T>? Done;
    }
}
