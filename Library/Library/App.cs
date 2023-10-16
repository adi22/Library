using Library.DataProviders;
using Library.Entities;
using Library.Repositories;

namespace Library
{
    public class App : IApp
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IBooksProvider _booksProvider;
        private const string logFile = "log.txt";
        private List<string> _logBuffer = new List<string>();

        public App(IRepository<Book> booksRepository, 
                   IBooksProvider booksProvider)
        {
            _booksRepository = booksRepository;
            _booksProvider = booksProvider;
        }

        public void Run()
        {
            _booksRepository.GetAll();

            _booksRepository.BookAdded += BookRepositoryBookAdded;
            _booksRepository.BookDeleted += BookRepositoryBookDeleted;
            _booksRepository.Done += BookRepositoryActionDone;

            if (_booksRepository.GetAll().Count() == 0)
            {
                DataGenerate();
                _booksRepository.Save();
                logBufferSave(_logBuffer);
            }

            Menu();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "Q" || input == "q")
                {
                    break;
                }
                switch (input)
                {
                    case "m":
                    case "M":
                        Menu();
                        break;
                    case "1":
                        ShowLibrary();
                        break;
                    case "2":
                        AddBook();
                        break;
                    case "3":
                        RemoveBook();
                        break;
                    case "4":
                        SaveChanges(_logBuffer);
                        break;
                    case "5":
                        ShowUniqueAuthors();
                        break;
                    case "6":
                        ShowMaximalLengthOfAllBooks();
                        break;
                    case "7":
                        ShowOrderedByTitle();
                        break;
                    default:
                        Console.WriteLine("Wrong option!");
                        break;

                }
            }
        }

        private void DataGenerate()
        {
            _booksRepository.Add(new Book { Title = "The Stand", Author = "Stephen King", Length = 1088, Rating = 8 });
            _booksRepository.Add(new Book { Title = "The Godfather", Author = "Mario Puzo", Length = 576, Rating = 9 });
            _booksRepository.Add(new Book { Title = "The Green Mile", Author = "Stephen King", Length = 416, Rating = 9 });
            _booksRepository.Add(new Book { Title = "A Game of Thrones", Author = "George R.R. Martin", Length = 1000, Rating = 8 });
            _booksRepository.Add(new Book { Title = "1984", Author = "George Orwell", Length = 352, Rating = 8 });
            _booksRepository.Add(new Book { Title = "TLotR: The Fellowship of the Ring", Author = "J.R.R Tolkien", Length = 448, Rating = 8 });
            _booksRepository.Add(new Book { Title = "The Hobbit, or There and Back Again", Author = "J.R.R Tolkien", Length = 304, Rating = 7 });
            _booksRepository.Add(new Book { Title = "Metro 2033", Author = "Dmitry Glukhovsky", Length = 578, Rating = 7 });
            _booksRepository.Add(new Book { Title = "Dune", Author = "Frank Herbert", Length = 777, Rating = 8 });
            _booksRepository.Add(new Book { Title = "Frankenstein", Author = "Mary Shelley", Length = 320, Rating = 7 });
            _booksRepository.Add(new Book { Title = "Dracula", Author = "Bram Stoker", Length = 428, Rating = 7 });
            _booksRepository.Add(new Book { Title = "It", Author = "Stephen King", Length = 1104, Rating = 7 });
            _booksRepository.Add(new Book { Title = "Othello", Author = "William Shakespeare", Length = 224, Rating = 6 });
        }

        private void BookRepositoryBookAdded(object? sender, Book e)
        {
            _logBuffer.Add($"[{DateTime.Now}]-BookAdded-[{e.Title}]");
        }

        private void BookRepositoryBookDeleted(object? sender, Book e)
        {
            _logBuffer.Add($"[{DateTime.Now}]-BookDeleted-[{e.Title}]");
        }

        private void BookRepositoryActionDone(object? sender, Book e)
        {
            Console.WriteLine("Done!");
        }

        private void ShowLibrary()
        {
            var list = _booksRepository.Show();

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private void AddBook()
        {
            Console.WriteLine("Enter the title of book you want to add to your library:");
            try
            {
                var input = ValidateTitle(Console.ReadLine());
                _booksRepository.Add(new Book() { Title = input });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RemoveBook()
        {
            Console.WriteLine("Enter the Id of book you want to delete:");
            try
            {
                var input = ValidateId(Console.ReadLine());
                var item = _booksRepository.GetById(input);
                _booksRepository.Remove(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SaveChanges(List<string> buffer)
        {
            Console.WriteLine("Do you want to save changes? (y)es/(n)o");

            var input = Console.ReadLine();

            switch (input)
            {
                case "y":
                    _booksRepository.Save();
                    logBufferSave(buffer);
                    break;
                case "n":
                    break;
                default:
                    Console.WriteLine("Wrong option");
                    break;
            }
        }

        private static void logBufferSave(List<string> buffer)
        {
            foreach (var item in buffer)
            {
                using (var writer = File.AppendText(logFile))
                {
                    writer.WriteLine(item);
                }
            }
        }

        private static string ValidateTitle(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length > 30)
            {
                throw new Exception("Wrong input");
            }
            else
            {
                return input;
            }
        }

        private static int ValidateId(string input)
        {
            if (!int.TryParse(input, out int result))
            {
                throw new Exception("Wrong Id");
            }
            else
            {
                return result;
            }
        }

        private void ShowUniqueAuthors()
        {
            foreach (var item in _booksProvider.GetUniqueAuthors())
            {
                Console.WriteLine(item);
            }
        }

        private void ShowMaximalLengthOfAllBooks()
        {
            Console.WriteLine(_booksProvider.getMaximalLengthOfAllBooks());
        }

        private void ShowOrderedByTitle()
        {
            foreach (var item in _booksProvider.OrderByTitle())
            {
                Console.WriteLine(item);
            }
        }

        private static void Menu()
        {
            Console.WriteLine("Hello in Library App");
            Console.WriteLine("Choose:");
            Console.WriteLine("1. To show the library");
            Console.WriteLine("2. To add book to the library");
            Console.WriteLine("3. To remove book from the library");
            Console.WriteLine("4. To save changes");
            Console.WriteLine("5. Show unique authors");
            Console.WriteLine("6. Show maximal length of all books");
            Console.WriteLine("7. Show all books ordered by title");
            Console.WriteLine("M. Show menu");
            Console.WriteLine("Q. To quit application");
        }
    }
}
