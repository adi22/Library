using Library.Data.Entities;
using System.Globalization;

namespace Library.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {
        public List<Book> ProcessBooks(string filePath)
        {
            if (!File.Exists(filePath)) 
            {
                return new List<Book>();
            }

            var books = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new Book()
                    {
                        Title = columns[1],
                        Author = columns[2],
                        Rating = double.Parse(columns[3], CultureInfo.InvariantCulture),
                        Length = int.Parse(columns[7])
                    };
                });

            return books.ToList();
        }
    }
}
