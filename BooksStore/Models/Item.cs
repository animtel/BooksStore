using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksStore.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public string Number { get; set; }

        public Item(int id, string name, string autor, int price, string number = "0")
        {
            Id = id;
            Name = name;
            Price = price;
            Number = number;
        }

    }
}