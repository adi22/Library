using Library.Data.Entities;

namespace Library.Data.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        IEnumerable<T> GetAllSaved();
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}