using Library.Data.Entities;

namespace Library.Components.DataProviders
{
    public interface IBooksProvider
    {
        IEnumerable<string> GetUniqueAuthors();
        int GetMaximalLengthFromAllBooks();
        IEnumerable<Book> OrderByTitle();
        double GetAverageRatingOfAllBooks();
        IEnumerable<Book> ResultsToPage(int page);
    }
}
