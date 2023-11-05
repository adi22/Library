using Library.Data.Entities;
using System.Text.Json;

namespace Library.Data.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private const string booksFile = "books.json";
        private const string latestIdFile = "latestId.txt";
        private List<T> _books = new();
        private int latestId;

        public event EventHandler<T>? BookAdded;
        public event EventHandler<T>? BookDeleted;
        public event EventHandler<T>? Done;

        public void Add(T item)
        {

            item.Id = latestId + 1;
            latestId += 1;
            _books.Add(item);
            BookAdded?.Invoke(this, item);
            Done?.Invoke(this, item);
        }

        public IEnumerable<T> GetAllSaved()
        {
            if (File.Exists(booksFile))
            {
                var json = File.ReadAllText(booksFile);

                _books = JsonSerializer.Deserialize<List<T>>(json);
                GetLatestId();
            }
            return _books;
        }

        public IEnumerable<T> GetAll()
        { 
            return _books;
        }

        public T GetById(int id)
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

        public void Remove(T item)
        {
            _books.Remove(item);
            BookDeleted?.Invoke(this, item);
            Done?.Invoke(this, item);
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_books);
            File.WriteAllText(booksFile, json);
            SaveLatestId();
            Done?.Invoke(this, null);
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
    }
}
