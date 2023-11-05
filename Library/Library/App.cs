using Library.Data;
using Library.Data.Entities;
using Library.Data.Repositories;
using Library.UserCommunication;

namespace Library
{
    public class App : IApp
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IEventHandler _eventHandler;
        private readonly IMenu _menu;
        private readonly MenuForFile _menuForFile;
        private readonly MenuForDatabase _menuForDatabase;
        private readonly MenuMain _menuMain;

        public App(
                   IRepository<Book> booksRepository,
                   LibraryDbContext libraryDbContext,
                   IEventHandler eventHandler,
                   IMenu menu,
                   MenuForFile menuForFile,
                   MenuForDatabase menuForDatabase,
                   MenuMain menuMain)
        {
            _booksRepository = booksRepository;
            _libraryDbContext = libraryDbContext;
            _eventHandler = eventHandler;
            _menu = menu;
            _menuForFile = menuForFile;
            _menuForDatabase = menuForDatabase;
            _menuMain = menuMain;
        }

        public void Run()
        {
            PrepareAllData();
            ShowUi();
        }

        private void PrepareAllData()
        {
            _libraryDbContext.Database.EnsureCreated();
            _booksRepository.GetAllSaved();
            _eventHandler.HandleEvents();
        }

        private void ShowUi()
        {
            while (true)
            {
                if (_menu.DoYouWantExit())
                {
                    break;
                }

                switch (_menu.GetChosenOption())
                {
                    case 0:
                        _menuMain.ShowMenu();
                        break;
                    case 1:
                        _menuForFile.ShowMenu();
                        break;
                    case 2:
                        _menuForDatabase.ShowMenu();
                        break;
                    default:
                        _menuMain.ShowMenu();
                        break;
                }
            }
        }
    }
}
