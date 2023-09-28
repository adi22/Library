using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; }

        public override string ToString() => $"Id: {Id}, Title: {Title}";


    }
}
