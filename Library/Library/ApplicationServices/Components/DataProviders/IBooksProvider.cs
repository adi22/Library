using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.ApplicationServices.Components.DataProviders
{
    public interface IBooksProvider<TRepository> 
        where TRepository: class, IRepository<Book>
    {
        IEnumerable<string> GetUniqueAuthors();
        int GetMaximalLengthFromAllBooks();
        IEnumerable<Book> OrderByTitle();
        double GetAverageRatingOfAllBooks();
        IEnumerable<Book> ResultsToPage(int page);
    }
}