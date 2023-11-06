using Library.ApplicationServices.Components.CsvReader;
using Library.ApplicationServices.Components.DataProviders;
using Library.ApplicationServices.Components.XmlExporter;
using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.UI.UserCommunication
{
    public class UserOptionsInFile : UserOptionsBase
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly BooksFromFileProvider _booksFromFileProvider;
        private readonly ICsvReader _csvReader;
        private readonly IXmlExporter _xmlExporter;
        private const string logFile = "log.txt";
        private readonly List<string> _logBuffer = new();

        public UserOptionsInFile(
            IRepository<Book> booksRepository,
            IInputValidator inputValidator,
            BooksFromFileProvider booksFromFileProvider,
            ICsvReader csvReader,
            IXmlExporter xmlExporter)
            : base(inputValidator)
        {
            _booksRepository = booksRepository;
            _booksFromFileProvider = booksFromFileProvider;
            _csvReader = csvReader;
            _xmlExporter = xmlExporter;
        }

        public override void AddBook()
        {
            var bookTitle = PrepateTitle();
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

        public override void RemoveBook()
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

        public override void SaveChanges()
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

        public override void ShowLibrary()
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

        public override void ShowUniqueAuthors()
        {
            foreach (var author in _booksFromFileProvider.GetUniqueAuthors())
            {
                Console.WriteLine(author);
            }

        }

        public override void ShowMaximalLengthFromAllBooks()
        {
            Console.WriteLine($"\nThe longest book has {_booksFromFileProvider.GetMaximalLengthFromAllBooks()} pages\n");
        }

        public override void OrderByTitle()
        {
            foreach (var book in _booksFromFileProvider.OrderByTitle())
            {
                Console.WriteLine(book);
            }
        }

        public override void ShowAverageRatingOfAllBooks()
        {
            Console.WriteLine($"\nThe average rating of all books is: {_booksFromFileProvider.GetAverageRatingOfAllBooks()}\n");
        }

        public override void ShowPageNumber()
        {
            var pageNumber = PreparePageNumber();

            try
            {
                foreach (var book in _booksFromFileProvider.ResultsToPage(pageNumber))
                {
                    Console.WriteLine(book);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public override void ImportDataFromCsv()
        {
            var records = _csvReader.ProcessBooks("DataAccess\\Resources\\Files\\books.csv");

            foreach (var record in records)
            {
                _booksRepository.Add(record);
            }
        }

        public override void ExportDataToXml()
        {
            _xmlExporter.ExportToXml(_booksRepository.GetAll());
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
    }
}