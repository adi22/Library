using Library.DataAccess.Data.Entities;

namespace Library.DataAccess.Data.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        IEnumerable<T> GetAllSaved();
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}