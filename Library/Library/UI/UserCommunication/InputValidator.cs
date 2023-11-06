namespace Library.UI.UserCommunication
{
    public class InputValidator : IInputValidator
    {
        public string ValidateAuthor(string author)
        {
            if (string.IsNullOrEmpty(author) || author.Length > 50)
            {
                throw new Exception("Wrong author");
            }
            else
            {
                return author;
            }
        }

        public int ValidateId(string id)
        {
            if (!int.TryParse(id, out int result) || int.Parse(id) < 1)
            {
                throw new Exception("Wrong Id number");
            }
            else
            {
                return result;
            }
        }

        public int ValidateLength(string length)
        {
            if (!int.TryParse(length, out int result) || int.Parse(length) < 1)
            {
                throw new Exception("Wrong length");
            }
            else
            {
                return result;
            }
        }

        public double ValidateRating(string rating)
        {
            if (!double.TryParse(rating, out double result) || double.Parse(rating) > 5 || double.Parse(rating) < 1)
            {
                throw new Exception("Wrong rating");
            }
            else
            {
                return result;
            }
        }

        public string ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title) || title.Length > 50)
            {
                throw new Exception("Wrong title");
            }
            else
            {
                return title;
            }
        }

        public int ValidatePageNumber(string pageNumber)
        {
            if (!int.TryParse(pageNumber, out int result) || int.Parse(pageNumber) < 1)
            {
                throw new Exception("Wrong page number");
            }
            else
            {
                return result;
            }
        }
    }
}