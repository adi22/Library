using Library.DataAccess.Data;
using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;
using Library.UI.UserCommunication;

namespace Library.UI
{
    public class App : IApp
    {
        private readonly FileRepository<Book> _fileRepository;
        private readonly SqlRepository<Book> _sqlRepository;
        private readonly UserCommunication.EventHandler<FileRepository<Book>> _fileEventHandler;
        private readonly UserCommunication.EventHandler<SqlRepository<Book>> _databaseEventHandler;
        private readonly IMenu _menu;
        private readonly MenuForFile _menuForFile;
        private readonly MenuForDatabase _menuForDatabase;
        private readonly MenuMain _menuMain;

        public App(
                   FileRepository<Book> fileRepository,
                   SqlRepository<Book> sqlRepository,
                   UserCommunication.EventHandler<FileRepository<Book>> fileEventHandler,
                   UserCommunication.EventHandler<SqlRepository<Book>> databaseEventHandler,
                   IMenu menu,
                   MenuForFile menuForFile,
                   MenuForDatabase menuForDatabase,
                   MenuMain menuMain)
        {
            _fileRepository = fileRepository;
            _sqlRepository = sqlRepository;
            _fileEventHandler = fileEventHandler;
            _databaseEventHandler = databaseEventHandler;
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
            _sqlRepository.EnsureCreated();
            _fileRepository.GetAll();
            _fileEventHandler.HandleEvents();
            _databaseEventHandler.HandleEvents();
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