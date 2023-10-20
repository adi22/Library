using Library.Data.Entities;
using Library.Data.Repositories;

namespace Library.Components.DataProviders
{
    public class BooksProvider : IBooksProvider
    {
        private readonly IRepository<Book> _booksRepository;

        public BooksProvider(IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public List<string?> GetUniqueAuthors()
        {
            var books = _booksRepository.GetAll();
            return books
            .OrderBy (x => x.Author)
            .Select(b => b.Author)
            .Distinct()
            .ToList();
        }

        public int GetMaximalLengthOfAllBooks()
        {
            var books = _booksRepository.GetAll();
            return (int)books.Select(x => x.Length).Max();
        }

        public List<Book> OrderByTitle()
        {
            var books = _booksRepository.GetAll();
            return books.OrderBy(x => x.Title).ToList();
        }

        public double GetAverageRatingOfAllBooks()
        {
            var books = _booksRepository.GetAll();
            return (double)books.Select(x => x.Rating).Average();
        }

        public List<Book> ResultsToPage(int page)
        {
            var books = _booksRepository.GetAll();
            return books
                .OrderBy(x => x.Title)
                .Take((page*100-100)..(page*100)).ToList();
        }
    }
}
