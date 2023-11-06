using System.Text;

namespace Library.DataAccess.Data.Entities
{
    public class Book : EntityBase
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {Id}   Title: {Title}");
            sb.AppendLine($"        Author: {Author}");
            sb.AppendLine($"        Length: {Length} pages");
            sb.AppendLine($"        Rating: {Rating}");

            return sb.ToString();
        }
    }
}