using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;

        public event EventHandler<T>? BookAdded;
        public event EventHandler<T>? BookDeleted;
        public event EventHandler<T>? Done;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }



        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);

        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public List<T> Show()
        {
            throw new NotImplementedException();
        }
    }
}
