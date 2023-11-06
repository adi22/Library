using Library.DataAccess.Data.Entities;

namespace Library.ApplicationServices.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Book> ProcessBooks(string filePath);
    }
}