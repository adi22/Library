namespace Library.UI.UserCommunication
{
    public abstract class UserOptionsBase : IUserOptions
    {
        private readonly IInputValidator _inputValidator;

        public UserOptionsBase(IInputValidator inputValidator)
        {
            _inputValidator = inputValidator;
        }

        public abstract void AddBook();
        public abstract void RemoveBook();
        public abstract void SaveChanges();
        public abstract void ShowLibrary();
        public abstract void ShowUniqueAuthors();
        public abstract void ShowMaximalLengthFromAllBooks();
        public abstract void OrderByTitle();
        public abstract void ShowAverageRatingOfAllBooks();
        public abstract void ShowPageNumber();
        public abstract void ImportDataFromCsv();
        public abstract void ExportDataToXml();

        protected string PrepateTitle()
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

        protected string PrepareAuthor()
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

        protected int PrepareLength()
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

        protected double PrepareRating()
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

        protected int PrepareId()
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

        protected int PreparePageNumber()
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