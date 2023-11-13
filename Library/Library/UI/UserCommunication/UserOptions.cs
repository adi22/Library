using Library.ApplicationServices.Components.CsvReader;
using Library.ApplicationServices.Components.DataProviders;
using Library.ApplicationServices.Components.XmlExporter;
using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.UI.UserCommunication
{
    public class UserOptions<TRepository> : IUserOptions<TRepository>
        where TRepository : class, IRepository<Book>
    {
        private readonly IInputValidator _inputValidator;
        private readonly TRepository _booksRepository;
        private readonly BooksProvider<TRepository> _booksProvider;
        private readonly ICsvReader _csvReader;
        private readonly IXmlExporter _xmlExporter;
        private static string logFile;
        private readonly List<string> _logBuffer = new();

        public UserOptions(
            IInputValidator inputValidator,
            TRepository booksRepository,
            BooksProvider<TRepository> booksProvider,
            ICsvReader csvReader,
            IXmlExporter xmlExporter)
        {
            _inputValidator = inputValidator;
            _booksRepository = booksRepository;
            _booksProvider = booksProvider;
            _csvReader = csvReader;
            _xmlExporter = xmlExporter;
            logFile = booksRepository.GetLogName();
        }

        public void AddBook()
        {
            var bookTitle = PrepareTitle();
            var bookAuthor = PrepareAuthor();
            var bookLength = PrepareLength();
            var bookRating = PrepareRating();

            _booksRepository.Add(new Book()
            {
                Title = bookTitle,
                Author = bookAuthor,
                Length = bookLength,
                Rating = bookRating
            });
        }

        public void RemoveBook()
        {
            var id = PrepareId();

            try
            {
                var book = _booksRepository.GetById(id);
                _booksRepository.Remove(book);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }

        public void SaveChanges()
        {
            bool doYouGetRightChoice = false;

            Console.WriteLine("Do you want to save changes? (y)es/(n)o");

            do
            {
                var input = Console.ReadLine();

                switch (input)
                {
                    case "y":
                        _booksRepository.Save();
                        LogBufferSave(_logBuffer);
                        doYouGetRightChoice = true;
                        break;
                    case "n":
                        doYouGetRightChoice = true;
                        break;
                    default:
                        Console.WriteLine("Wrong option");
                        break;
                }
            } while (!doYouGetRightChoice);
        }

        public void ShowLibrary()
        {
            if (_booksRepository.GetAll().Any())
            {
                foreach (var book in _booksRepository.GetAll())
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        public void ShowUniqueAuthors()
        {
            if (_booksRepository.GetAll().Any())
            {
                foreach (var author in _booksProvider.GetUniqueAuthors())
                {
                    Console.WriteLine(author);
                }
            }
            else
            {
                Console.WriteLine("Library is empty");
            }

        }

        public void ShowMaximalLengthFromAllBooks()
        {
            if (_booksRepository.GetAll().Any())
            {
                Console.WriteLine($"\nThe longest book has {_booksProvider.GetMaximalLengthFromAllBooks()} pages\n");
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        public void OrderByTitle()
        {
            if (_booksRepository.GetAll().Any())
            {
                foreach (var book in _booksProvider.OrderByTitle())
                {
                    Console.WriteLine(book);
                }
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        public void ShowAverageRatingOfAllBooks()
        {
            if (_booksRepository.GetAll().Any())
            {
                Console.WriteLine($"\nThe average rating of all books is: {_booksProvider.GetAverageRatingOfAllBooks()}\n");
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        public void ShowPageNumber()
        {
            if (_booksRepository.GetAll().Any())
            {
                var pageNumber = PreparePageNumber();

                try
                {
                    foreach (var book in _booksProvider.ResultsToPage(pageNumber))
                    {
                        Console.WriteLine(book);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        public void ImportDataFromCsv()
        {
            var records = _csvReader.ProcessBooks("DataAccess\\Resources\\Files\\books.csv");

            foreach (var record in records)
            {
                _booksRepository.Add(record);
            }
        }

        public void ExportDataToXml()
        {
            if (_booksRepository.GetAll().Any())
            {
                _xmlExporter.ExportToXml(_booksRepository.GetAll());
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        private static void LogBufferSave(List<string> buffer)
        {
            foreach (var item in buffer)
            {
                using (var writer = File.AppendText(logFile))
                {
                    writer.WriteLine(item);
                }
            }
        }

        public List<string> GetLogBuffer()
        {
            return _logBuffer;
        }

        private string PrepareTitle()
        {
            var title = "";

            while (true)
            {
                Console.WriteLine("Enter the title of the book:");
                try
                {
                    title = _inputValidator.ValidateTitle(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return title;
        }

        private string PrepareAuthor()
        {
            var author = "";

            while (true)
            {
                Console.WriteLine("Enter the authors of the book:");
                try
                {
                    author = _inputValidator.ValidateAuthor(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return author;
        }

        private int PrepareLength()
        {
            var length = 0;

            while (true)
            {
                Console.WriteLine("Enter the length of the book:");
                try
                {
                    length = _inputValidator.ValidateLength(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return length;
        }

        private double PrepareRating()
        {
            var rating = 0.0;

            while (true)
            {
                Console.WriteLine("Enter the rating of the book:");
                try
                {
                    rating = _inputValidator.ValidateRating(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return rating;
        }

        private int PrepareId()
        {
            var id = 0;

            while (true)
            {
                Console.WriteLine("Enter the Id of book you want to delete:");
                try
                {
                    id = _inputValidator.ValidateId(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return id;
        }

        private int PreparePageNumber()
        {
            var pageNumber = 0;

            while (true)
            {
                Console.WriteLine("Enter the page number you want to see:");
                try
                {
                    pageNumber = _inputValidator.ValidatePageNumber(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return pageNumber;
        }
    }
}