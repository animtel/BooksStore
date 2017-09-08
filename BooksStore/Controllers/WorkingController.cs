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
        IRepository<Paper> db_of_Papers;
        TestBD db = new TestBD();

        public WorkingController()
        {
            db_of_Books = new SQLBookRepository();
            db_of_Purchases = new SQLPurchaseRepository();
            db_of_Journales = new SQLJournalRepository();
            db_of_Papers = new SQLPaperRepository();
        }

        public ActionResult test()
        {
            var paper = db.Papers.Include(p => p.Form).ToList();
            var test = db.Books.ToList();
            return View(test);
        }

        // Просмотр подробных сведений о книге
        public ActionResult Details(int id)
        {
            Book comp = db.Books.Find(id);
            if (comp != null)
            {
                return PartialView("Details", comp);
            }
            return View("Index");
        }
        // Добавление
        public ActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Редактирование
        public ActionResult Edit(int id)
        {
            Book comp = db.Books.Find(id);
            if (comp != null)
            {
                return PartialView("Edit", comp);
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book comp)
        {
            db.Entry(comp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Удаление
        public ActionResult Delete(int id)
        {
            Book comp = db.Books.Find(id);
            if (comp != null)
            {
                return PartialView("Delete", comp);
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            Book comp = db.Books.Find(id);

            if (comp != null)
            {
                db.Books.Remove(comp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}