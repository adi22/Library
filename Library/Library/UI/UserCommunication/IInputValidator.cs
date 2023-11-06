namespace Library.UI.UserCommunication
{
    public interface IInputValidator
    {
        int ValidateId(string id);
        string ValidateTitle(string title);
        string ValidateAuthor(string author);
        int ValidateLength(string length);
        double ValidateRating(string rating);
        int ValidatePageNumber(string pageNumber);
    }
}