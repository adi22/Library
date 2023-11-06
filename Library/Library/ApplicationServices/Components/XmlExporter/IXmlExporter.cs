using Library.DataAccess.Data.Entities;

namespace Library.ApplicationServices.Components.XmlExporter
{
    public interface IXmlExporter
    {
        void ExportToXml(IEnumerable<Book> bookRepository);
    }
}