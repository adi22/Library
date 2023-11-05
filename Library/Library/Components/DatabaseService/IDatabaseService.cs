using Library.Data.Entities;

namespace Library.Components.DatabaseProviders
{
    public interface IDatabaseService
    {
        void CsvToDatabase();
        IEnumerable<Book> DatabaseRead();
        Book? ReadById(int id);
        Book? ReadByTitle(string title);
    }
}
