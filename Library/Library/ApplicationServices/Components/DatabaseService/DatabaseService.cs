using Library.DataAccess.Data;
using Library.DataAccess.Data.Entities;

namespace Library.ApplicationServices.Components.DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        private readonly LibraryDbContext _libraryDbContext;

        public DatabaseService(
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public Book ReadById(int id)
        {
            if (_libraryDbContext.Books.Any(book => book.Id == id))
            {
                return _libraryDbContext.Books.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                throw new Exception("There is no such id");
            }
        }
    }
}