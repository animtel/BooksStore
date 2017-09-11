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
    public class WorkingController : Controller
    {
        // создаем контекст данных
        IRepository<Book> db_of_Books;
        IRepository<Purchase> db_of_Purchases;
        IRepository<Journal> db_of_Journales;
        IRepository<Item> db_of_Items;
        TestBD db = new TestBD();
        List<Item> current = new List<Item>();



        public WorkingController()
        {
            db_of_Books = new SQLBookRepository();
            db_of_Purchases = new SQLPurchaseRepository();
            db_of_Journales = new SQLJournalRepository();
            db_of_Items = new SQLItemRepository();

            List<string> all_items = new List<string>();
            all_items.Add("Books");
            all_items.Add("Journals");
            all_items.Add("All");

            ViewBag.AllItems = all_items;
        }

        public ActionResult test()
        {
            var books = db.Books.ToList();
            

            ViewBag.DataTable = books;

            return View("test");
        }

        public ActionResult Drop(string t)
        {
            
            var books = db.Books.ToList();
            var journales = db.Journales.ToList();

            foreach (var item in books)
            {
                current.Add(new Item { Id = item.Id, Name = item.Name, Author = item.Author, Price = item.Price });

            }
            foreach (var item in journales)
            {
                current.Add(new Item { Id = item.Id, Name = item.Name, Author = item.Author, Price = item.Price, Number = item.Number });
            }
            foreach (var item in current)
            {
                db.Items.Add(item);
            }

            
            string some = Request.Form["DropTypes"].ToString();
            switch (some)
            {
                case "Books":
                    ViewBag.DataTable = books;
                    break;
                case "Journals":
                    ViewBag.DataTable = journales;
                    break;
                case "All":
                    ViewBag.DataTable = current;
                    break;
            }

            return View("test");
        }

        // Просмотр подробных сведений о книге
        public ActionResult Details(int id)
        {
            Item it = db.Items.Find(id);

            if (it != null)
            {
                return PartialView("Details", it);
            }
            return View("test");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                db_of_Items.Create(item);
                db_of_Items.Save();
                return RedirectToAction("test");
            }
            return View(item);
        }
        // Редактирование
        public ActionResult Edit(int id)
        {
            Item comp = db.Items.Find(id);
            if (comp != null)
            {
                return PartialView("Edit", comp);
            }
            return View("test");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book comp)
        {
            db.Entry(comp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("test");
        }
        // Удаление
        public ActionResult Delete(int id)
        {
            Item comp = db.Items.Find(id);
            if (comp != null)
            {
                return PartialView("Delete", comp);
            }
            return View("test");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            Item comp = db.Items.Find(id);

            if (comp != null)
            {
                db.Items.Remove(comp);
                db.SaveChanges();
            }
            return RedirectToAction("test");
        }
    }
}