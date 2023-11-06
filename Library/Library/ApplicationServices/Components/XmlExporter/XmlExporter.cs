using Library.DataAccess.Data.Entities;
using System.Xml.Linq;

namespace Library.ApplicationServices.Components.XmlExporter
{
    public class XmlExporter : IXmlExporter
    {
        public void ExportToXml(IEnumerable<Book> bookRepository)
        {
            var records = bookRepository;

            var document = new XDocument();
            var books = new XElement("Books", records
                .Select(x =>
                new XElement("Book",
                new XAttribute("Title", x.Title),
                new XAttribute("Author", x.Author),
                new XAttribute("Rating", x.Rating),
                new XAttribute("Length", x.Length))));

            document.Add(books);
            document.Save("books.xml");
        }
    }
}