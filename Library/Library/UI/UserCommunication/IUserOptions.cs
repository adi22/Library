using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.UI.UserCommunication
{
    public interface IUserOptions<TRepository>
        where TRepository : class, IRepository<Book>
    {
        void ShowLibrary();
        void AddBook();
        void RemoveBook();
        void SaveChanges();
        void ShowUniqueAuthors();
        void ShowMaximalLengthFromAllBooks();
        void OrderByTitle();
        void ShowAverageRatingOfAllBooks();
        void ShowPageNumber();
        void ImportDataFromCsv();
        void ExportDataToXml();
    }
}