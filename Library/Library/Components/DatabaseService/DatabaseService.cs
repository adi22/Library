using Library.Components.CsvReader;
using Library.Data;
using Library.Data.Entities;

namespace Library.Components.DatabaseProviders
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ICsvReader _csvReader;
        private readonly LibraryDbContext _libraryDbContext;

        public DatabaseService(
            ICsvReader csvreader,
            LibraryDbContext libraryDbContext)
        {
            _csvReader = csvreader;
            _libraryDbContext = libraryDbContext;
        }

        public void CsvToDatabase()
        {
            var books = _csvReader.ProcessBooks("Resources\\Files\\books.csv");

            foreach ( var book in books ) 
            {
                _libraryDbContext.Books.Add(new Book()
                {
                    Title = book.Title,
                    Author = book.Author,
                    Length = book.Length,
                    Rating = book.Rating
                });
            }
            _libraryDbContext.SaveChanges();
        }
        
        public IEnumerable<Book> DatabaseRead()
        {
            var booksFromDb = _libraryDbContext.Books.ToList();

            return booksFromDb;
        }

        public Book? ReadById(int id)
        {
            if (_libraryDbContext.Books.Any(book => book.Id == id))
            {
                return _libraryDbContext.Books.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                throw new Exception("There is no such id");
            }
        }

        public Book? ReadByTitle(string title)
        {
            return _libraryDbContext.Books.FirstOrDefault(x => x.Title == title);
        }
    }
}
