using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksStore.Models;
using BooksStore.Util;
using System.Threading.Tasks;
using System.Data.Entity;
using BooksStore.Repository;

namespace BooksStore.Controllers
{

    public class HomeController : Controller
    {
        // создаем контекст данных
        IRepository<Book> db_of_Books;
        IRepository<Purchase> db_of_Purchases;
        IRepository<Journal> db_of_Journales;
        TestBD db = new TestBD();

        public HomeController()
        {
            db_of_Books = new SQLBookRepository();
            db_of_Purchases = new SQLPurchaseRepository();
            db_of_Journales = new SQLJournalRepository();
        }


        public ActionResult Index()
        {
            IEnumerable<Book> books = db_of_Books.GetItemList();
            
            return View(books.ToList());
        }

        //[HttpPost]
        //public ActionResult Index(string name, string author, string price)
        //{
        //    db_of_Books.Create(new Book { Name = $"{name}", Author = $"{author}", Price = Convert.ToInt32(price) });
        //    db_of_Books.Save();
        //    IEnumerable<Book> books = db_of_Books.GetItemList();
        //    ViewBag.Books = books;
        //    return View(db_of_Books.GetItemList());
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db_of_Books.Create(book);
                db_of_Books.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        
        public ActionResult Edit(int id)
        {
            Book book = db_of_Books.GetItem(id);
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db_of_Books.Update(book);
                db_of_Books.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            
            db_of_Books.Delete(id);
            db_of_Books.Save();
            IEnumerable<Book> books = db_of_Books.GetItemList();
            ViewBag.Books = books;
            return RedirectToAction("Index");
        }

        

        protected override void Dispose(bool disposing)
        {
            db_of_Books.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Choice(string choice)
        {
            if (choice == "books")
            {
                ViewBag.Items = db_of_Journales.GetItemList();
            }
            else
            {
                ViewBag.Items = db_of_Books.GetItemList();
            }
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
            db_of_Purchases.Create(purchase);
            db_of_Purchases.Save();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

    }

}