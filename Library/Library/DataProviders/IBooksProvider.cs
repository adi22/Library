using Library.Entities;

namespace Library.DataProviders
{
    public interface IBooksProvider
    {
        List<string> GetUniqueAuthors();
        int getMaximalLengthOfAllBooks();
        List<Book> OrderByTitle();
    }
}
