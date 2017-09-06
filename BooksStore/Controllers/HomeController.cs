using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksStore.Models;
using BooksStore.Util;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BooksStore.Controllers
{

    public class HomeController : Controller
    {
        // создаем контекст данных
        TestBD db = new TestBD();
        [HttpPost]
        public ActionResult Index(string name, string author, string price)
        {
            //BookDbInitializer book_init = new BookDbInitializer();
            //book_init.AddToDB(name, author, Convert.ToInt32(price));

            db.Books.Add(new Book { Name = $"{name}", Author = $"{author}", Price = Convert.ToInt32(price) });
            db.SaveChanges();

            IEnumerable<Book> books = db.Books;

            ViewBag.Books = books;

            return View(db.Books);
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;

            ViewBag.Books = books;

            return View(db.Books);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            IEnumerable<Book> books = db.Books;
            List<Book> list = new List<Book>();
            foreach (var item in books)
            {
                list.Add(new Book { Id = item.Id, Name = item.Name, Author = item.Author, Price=item.Price});
            }
            db.Books.Remove(list[2]);
            db.SaveChanges();

            IEnumerable<Book> bookss = db.Books;

            ViewBag.Books = bookss;
            return View("Index");
        }

        [HttpGet]
        public ActionResult Buy(int Id)
        {
            ViewBag.BookId = Id;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

    }

}