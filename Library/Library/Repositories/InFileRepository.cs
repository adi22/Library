using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Repositories
{
    public class InFileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private const string booksFile = "books.txt";
        private const string latestIdFile = "latestId.txt"; 
        private List<T> _books = new List<T>();
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

        public IEnumerable<T> GetAll()
        {
            if (File.Exists(booksFile))
            {
                using (var reader = File.OpenText(booksFile))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var book = JsonSerializer.Deserialize<T>(line);
                        _books.Add(book);

                        line = reader.ReadLine();
                    }
                }
            }
            GetLatestId();
            return _books;
        }

        public T GetById(int id)
        {
            return _books.Single(item => item.Id == id);
        }

        public void Remove(T item)
        {
            _books.Remove(item);
            BookDeleted?.Invoke(this, item);
            Done?.Invoke(this, item);
        }

        public List<T> Show()
        {
            return _books;
        }

        public void Save()
        {
            using (var erase = File.CreateText(booksFile)) { };
            foreach (var item in _books)
            {
                var json = JsonSerializer.Serialize(item);

                using (var writer = File.AppendText(booksFile))
                {
                    writer.WriteLine(json);
                }
            }
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
