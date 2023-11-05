using Library.Data;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Components.DataProviders
{
    public class BooksFromDatabaseProvider : IBooksProvider
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BooksFromDatabaseProvider(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public double GetAverageRatingOfAllBooks()
        {
            return Math.Round(_libraryDbContext.Books.Select(x => x.Rating).Average(), 2);
        }

        public int GetMaximalLengthFromAllBooks()
        {
            return _libraryDbContext.Books.Select(x => x.Length).Max();
        }

        public IEnumerable<string> GetUniqueAuthors()
        {
            return _libraryDbContext
            .Books
            .OrderBy(x => x.Author)
            .Select(b => b.Author)
            .Distinct()
            .ToList();
        }

        public IEnumerable<Book> OrderByTitle()
        {
            return _libraryDbContext.Books.OrderBy(x => x.Title).ToList();
        }

        public IEnumerable<Book> ResultsToPage(int page)
        {
            if (IsEnteredPageInDatabaseRange(page))
            {
                return _libraryDbContext
                    .Books
                    .OrderBy(x => x.Title)
                    .Skip((page - 1) * 100)
                    .Take(100)
                    .ToList();
            }
            else
            {
                throw new Exception("Page is out of range");
            }
        }

        private bool IsEnteredPageInDatabaseRange(int enteredPage)
        {
            var lastPage = (_libraryDbContext.Books.Count()-1) / 100 + 1;

            if(enteredPage <= lastPage) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
