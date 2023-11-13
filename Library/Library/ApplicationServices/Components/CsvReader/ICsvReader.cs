using Library.DataAccess.Data.Entities;

namespace Library.ApplicationServices.Components.CsvReader
{
    public interface ICsvReader
    {
        IEnumerable<Book> ProcessBooks(string filePath);
    }
}