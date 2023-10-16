using Library.Entities;
using Library.Repositories;

namespace Library.DataProviders
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
            return books.Select(b => b.Author)
            .Distinct()
            .ToList();
        }

        public int getMaximalLengthOfAllBooks()
        {
            var books = _booksRepository.GetAll();
            return (int)books.Select(x => x.Length).Max();
        }

        public List<Book> OrderByTitle()
        {
            var books = _booksRepository.GetAll();
            return books.OrderBy(x => x.Title).ToList();
        }
    }
}
