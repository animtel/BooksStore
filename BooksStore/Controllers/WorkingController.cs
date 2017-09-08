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
        }

        public ActionResult test()
        {
            var books = db.Books.ToList();
            var journales = db.Journales.ToList();

            foreach (var item in books)
            {
                db.Items.Add(new Item(item.Id, item.Name, item.Author, item.Price));
            }
            foreach (var item in journales)
            {
                db.Items.Add(new Item(item.Id, item.Name, item.Author, item.Price, item.Number));
            }

            current = db_of_Items.GetItemList().ToList();
            return View(current);
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
        // Добавление
        public ActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return RedirectToAction("test");
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