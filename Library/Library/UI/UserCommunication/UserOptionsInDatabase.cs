using Library.ApplicationServices.Components.CsvReader;
using Library.ApplicationServices.Components.DatabaseService;
using Library.ApplicationServices.Components.DataProviders;
using Library.ApplicationServices.Components.XmlExporter;
using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data;

namespace Library.UI.UserCommunication
{
    public class UserOptionsInDatabase : UserOptionsBase
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IDatabaseService _databaseService;
        private readonly BooksFromDatabaseProvider _booksFromDatabaseProvider;
        private readonly ICsvReader _csvReader;
        private readonly IXmlExporter _xmlExporter;

        public UserOptionsInDatabase(
            LibraryDbContext libraryDbContext,
            IDatabaseService databaseService,
            IInputValidator inputValidator,
            BooksFromDatabaseProvider booksFromDatabaseProvider,
            ICsvReader csvReader,
            IXmlExporter xmlExporter)
            : base(inputValidator)
        {
            _libraryDbContext = libraryDbContext;
            _databaseService = databaseService;
            _booksFromDatabaseProvider = booksFromDatabaseProvider;
            _csvReader = csvReader;
            _xmlExporter = xmlExporter;
        }

        public override void AddBook()
        {
            var bookTitle = PrepateTitle();
            var bookAuthor = PrepareAuthor();
            var bookLength = PrepareLength();
            var bookRating = PrepareRating();

            _libraryDbContext.Books.Add(new Book()
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
                var book = _databaseService.ReadById(id);
                _libraryDbContext.Books.Remove(book);
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
                        _libraryDbContext.SaveChanges();
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
            if (_libraryDbContext.Books.Any())
            {
                foreach (var book in _libraryDbContext.Books)
                {
                    Console.WriteLine(book);
                }
            }
            else
            {
                Console.WriteLine("Library is empty");
            }
        }

        public override void ShowUniqueAuthors()
        {
            foreach (var author in _booksFromDatabaseProvider.GetUniqueAuthors())
            {
                Console.WriteLine(author);
            }

        }

        public override void ShowMaximalLengthFromAllBooks()
        {
            Console.WriteLine($"\nThe longest book has {_booksFromDatabaseProvider.GetMaximalLengthFromAllBooks()} pages\n");
        }

        public override void OrderByTitle()
        {
            foreach (var book in _booksFromDatabaseProvider.OrderByTitle())
            {
                Console.WriteLine(book);
            }
        }

        public override void ShowAverageRatingOfAllBooks()
        {
            Console.WriteLine($"\nThe average rating of all books is: {_booksFromDatabaseProvider.GetAverageRatingOfAllBooks()}\n");
        }

        public override void ShowPageNumber()
        {
            var pageNumber = PreparePageNumber();

            try
            {
                foreach (var book in _booksFromDatabaseProvider.ResultsToPage(pageNumber))
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
                _libraryDbContext.Books.Add(record);
            }
        }

        public override void ExportDataToXml()
        {
            _xmlExporter.ExportToXml(_libraryDbContext.Books);
        }
    }
}