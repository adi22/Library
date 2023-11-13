using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.UI.UserCommunication
{
    public class EventHandler<TRepository> : IEventHandler<TRepository> 
        where TRepository : class, IRepository<Book>
    {
        private readonly UserOptions<TRepository> _userOptions;
        private readonly TRepository _booksRepository;

        public EventHandler(
            UserOptions<TRepository> userOptions,
            TRepository booksRepository)
        {
            _userOptions = userOptions;
            _booksRepository = booksRepository;
        }
        public void HandleEvents()
        {
            _booksRepository.BookAdded += BookRepositoryBookAdded;
            _booksRepository.BookDeleted += BookRepositoryBookDeleted;
            _booksRepository.ChangesSaved += BookRepositoryChangesSaved;
        }
        private void BookRepositoryBookAdded(object? sender, Book e)
        {
            _userOptions.GetLogBuffer().Add($"[{DateTime.Now}]-BookAdded-[Title: {e.Title}]");
        }

        private void BookRepositoryBookDeleted(object? sender, Book e)
        {
            _userOptions.GetLogBuffer().Add($"[{DateTime.Now}]-BookDeleted-[Title: {e.Title}]");
        }

        private void BookRepositoryChangesSaved(object? sender, Book e)
        {
            Console.WriteLine("All changes have been saved!");
        }
    }
}