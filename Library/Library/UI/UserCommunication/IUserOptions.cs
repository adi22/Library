namespace Library.UI.UserCommunication
{
    public interface IUserOptions
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