using System.Text;

namespace Library.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public int? Length { get; set; }
        public int? Rating { get; set; }
        public decimal? Price { get; set; }

        public Book()
        {
            
        }
        public Book(string title, string author, int length, int rating)
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
            if (Price.HasValue)
            {
                sb.AppendLine($"        Price: {Price} PLN");
            }
            return sb.ToString();
        }



    }
}
