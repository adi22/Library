using System.Text;

namespace Library.Data.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; }
        public string? Author { get; set; }
        public int? Length { get; set; }
        public double? Rating { get; set; }
  

        public Book()
        {

        }
        public Book(string title, string author, int length, double rating)
        {
            Title = title;
            Author = author;
            Length = length;
            Rating = rating;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {Id}   Title: {Title}");
            if (Author != null)
            {
                sb.AppendLine($"        Author: {Author}");
            }
            if (Length.HasValue)
            {
                sb.AppendLine($"        Length: {Length} pages");
            }
            if (Rating.HasValue)
            {
                sb.AppendLine($"        Rating: {Rating}");
            }
            
            return sb.ToString();
        }



    }
}
