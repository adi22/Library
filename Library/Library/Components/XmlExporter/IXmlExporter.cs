using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Components.XmlExporter
{
    public interface IXmlExporter
    {
        void ExportToXml(IEnumerable<Book> bookRepository);
    }
}
