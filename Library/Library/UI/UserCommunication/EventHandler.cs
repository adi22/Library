using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.UI.UserCommunication
{
    public class EventHandler : IEventHandler
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly UserOptionsInFile _userOptionsInFile;

        public EventHandler(
            IRepository<Book> booksRepository,
            UserOptionsInFile userOptionsInFile)
        {
            _booksRepository = booksRepository;
            _userOptionsInFile = userOptionsInFile;
        }
        public void HandleEvents()
        {
            _booksRepository.BookAdded += BookRepositoryBookAdded;
            _booksRepository.BookDeleted += BookRepositoryBookDeleted;
            _booksRepository.Done += BookRepositoryActionDone;
        }
        private void BookRepositoryBookAdded(object? sender, Book e)
        {
            _userOptionsInFile.GetLogBuffer().Add($"[{DateTime.Now}]-BookAdded-[{e.Title}]");
        }

        private void BookRepositoryBookDeleted(object? sender, Book e)
        {
            _userOptionsInFile.GetLogBuffer().Add($"[{DateTime.Now}]-BookDeleted-[{e.Title}]");
        }

        private void BookRepositoryActionDone(object? sender, Book e)
        {
            Console.WriteLine("Done!");
        }
    }
}