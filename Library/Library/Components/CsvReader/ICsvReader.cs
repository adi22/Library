using Library.Data.Entities;

namespace Library.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Book> ProcessBooks(string filePath);
    }
}
