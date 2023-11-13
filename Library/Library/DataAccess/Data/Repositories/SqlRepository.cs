using Library.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Library.DataAccess.Data.Repositories
{
    public class SqlRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class, IEntity, new()
    {
        private readonly LibraryDbContext _libraryDbContext;
        private const string logName = "DatabaseLog.txt";

        public event EventHandler<TEntity>? BookAdded;
        public event EventHandler<TEntity>? BookDeleted;
        public event EventHandler<TEntity>? ChangesSaved;

        public SqlRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        private DbSet<TEntity> _dbSet
        {
            get { return _libraryDbContext.Set<TEntity>(); }
        }

        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            BookAdded?.Invoke(this, item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            if (_dbSet.Any(book => book.Id == id))
            {
                return _dbSet.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                throw new Exception("There is no such id");
            }
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            BookDeleted?.Invoke(this, item);
        }

        public void Save()
        {
            _libraryDbContext.SaveChanges();
            ChangesSaved?.Invoke(this, null);
        }

        public void EnsureCreated()
        {
            _libraryDbContext.Database.EnsureCreated();
        }

        public string GetLogName()
        {
            return logName;
        }
    }
}
