using Library.Data.Entities;

namespace Library.Components.DataProviders
{
    public interface IBooksProvider
    {
        List<string> GetUniqueAuthors();
        int GetMaximalLengthOfAllBooks();
        List<Book> OrderByTitle();
        double GetAverageRatingOfAllBooks();
        List<Book> ResultsToPage(int page);
    }
}
