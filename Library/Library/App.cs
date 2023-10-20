using Library.Components.CsvReader;
using Library.Components.DataProviders;
using Library.Data.Entities;
using Library.Data.Repositories;
using System.Xml.Linq;

namespace Library
{
    public class App : IApp
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IBooksProvider _booksProvider;
        private readonly ICsvReader _csvReader;
        private const string logFile = "log.txt";
        private readonly List<string> _logBuffer = new List<string>();

        public App(IRepository<Book> booksRepository, 
                   IBooksProvider booksProvider,
                   ICsvReader csvReader)
        {
            _booksRepository = booksRepository;
            _booksProvider = booksProvider;
            _csvReader = csvReader;
        }

        public void Run()
        {
            _booksRepository.GetAll();

            _booksRepository.BookAdded += BookRepositoryBookAdded;
            _booksRepository.BookDeleted += BookRepositoryBookDeleted;
            _booksRepository.Done += BookRepositoryActionDone;

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
                    case "i":
                    case "I":
                        ImportFromCsv();
                        break;
                    case "e":
                    case "E":
                        ExportToXml();
                        break;
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
                    case "8":
                        ShowPage();
                        break;
                    case "9":
                        ShowAverageRating();
                        break;
                    default:
                        Console.WriteLine("Wrong option!");
                        break;

                }
            }
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
            Console.WriteLine(_booksProvider.GetMaximalLengthOfAllBooks());
        }

        private void ShowOrderedByTitle()
        {
            foreach (var item in _booksProvider.OrderByTitle())
            {
                Console.WriteLine(item);
            }
        }

        private void ShowPage()
        {
            Console.WriteLine("Choose the page:");
            var page = Console.ReadLine();

            if (int.TryParse(page, out int res))
            {
                var result = _booksProvider.ResultsToPage(res);

                foreach (var item in result)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else 
            {
                Console.WriteLine("Wrong input!");
            }
        }

        private void ShowAverageRating()
        {
            Console.WriteLine(Math.Round(_booksProvider.GetAverageRatingOfAllBooks(),2));
        }

        private void ImportFromCsv()
        {
            var records = _csvReader.ProcessBooks("Resources\\Files\\books.csv");
            foreach (var record in records)
            {
                _booksRepository.Add(record);
            }
        }

        private void ExportToXml()
        {
            var records = _booksRepository.GetAll();

            var document = new XDocument();
            var books = new XElement("Books", records
                .Select(x =>
                new XElement("Book",
                new XAttribute("Title", x.Title),
                new XAttribute("Author", x.Author),
                new XAttribute("Rating", x.Rating),
                new XAttribute("Length", x.Length))));
                
            document.Add(books);
            document.Save("books.xml");
        }

        private static void Menu()
        {
            Console.WriteLine("Hello in Library App");
            Console.WriteLine("///// Standard options /////");
            Console.WriteLine("1. Show the library");
            Console.WriteLine("2. Add book to the library");
            Console.WriteLine("3. Remove book from the library");
            Console.WriteLine("4. Save changes");
            Console.WriteLine("///// Data provider options /////");
            Console.WriteLine("5. Show unique authors");
            Console.WriteLine("6. Show maximal length of all books");
            Console.WriteLine("7. Show all books ordered by title");
            Console.WriteLine("8. Choose the page you want to see");
            Console.WriteLine("9. Show average rating of all books");
            Console.WriteLine("///// Import/Export data /////");
            Console.WriteLine("I. Import data from CSV file");
            Console.WriteLine("E. Export data to XML file");
            Console.WriteLine("///// Navigation /////");
            Console.WriteLine("M. Show menu");
            Console.WriteLine("Q. Quit application");
        }
    }
}
