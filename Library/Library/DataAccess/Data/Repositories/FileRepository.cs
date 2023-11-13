using Library.DataAccess.Data.Entities;
using System.Text.Json;

namespace Library.DataAccess.Data.Repositories
{
    public class FileRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class, IEntity, new()
    {
        private const string booksFile = "Books.json";
        private const string latestIdFile = "LatestId.txt";
        private const string logName = "FileLog.txt";
        private List<TEntity> _books = new();
        private int latestId;

        public event EventHandler<TEntity>? BookAdded;
        public event EventHandler<TEntity>? BookDeleted;
        public event EventHandler<TEntity>? ChangesSaved;

        public void Add(TEntity item)
        {

            item.Id = latestId + 1;
            latestId += 1;
            _books.Add(item);
            BookAdded?.Invoke(this, item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            if (File.Exists(booksFile))
            {
                var json = File.ReadAllText(booksFile);

                _books = JsonSerializer.Deserialize<List<TEntity>>(json);
                GetLatestId();
            }
            return _books;
        }

        public TEntity GetById(int id)
        {
            if (!_books.Any(book => book.Id == id))
            {
                throw new Exception("There is no such id");
            }
            else
            {
                return _books.Single(item => item.Id == id);
            }
        }

        public void Remove(TEntity item)
        {
            _books.Remove(item);
            BookDeleted?.Invoke(this, item);
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_books);
            File.WriteAllText(booksFile, json);
            SaveLatestId();
            ChangesSaved?.Invoke(this, null);
        }

        private void GetLatestId()
        {
            if (File.Exists(latestIdFile))
            {
                using (var reader = File.OpenText(latestIdFile))
                {
                    latestId = int.Parse(reader.ReadLine());
                }
            }
            else
            {
                latestId = _books.Max(item => item.Id);
            }
        }

        private void SaveLatestId()
        {
            using (var writer = File.CreateText(latestIdFile))
            {
                writer.Write(latestId);
            }
        }

        public string GetLogName()
        {
            return logName;
        }
    }
}