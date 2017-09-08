using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksStore.Models
{
    public class Paper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }

        public int? FormId { get; set; }
        public Form Form { get; set; }
    }

    public class Form
    {
        public int Id { get; set; }
        public string Form_of_paper { get; set; }

        public ICollection<Paper> Papers { get; set; }
        public Form()
        {
            Papers = new List<Paper>();
        }
    }
}