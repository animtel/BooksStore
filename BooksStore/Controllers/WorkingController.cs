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
        List<Item> current;
        



        public WorkingController()
        {
            db_of_Books = new SQLBookRepository();
            db_of_Purchases = new SQLPurchaseRepository();
            db_of_Journales = new SQLJournalRepository();
            db_of_Items = new SQLItemRepository();

            current = db_of_Items.GetItemList().ToList();

            List<string> all_items = new List<string>();
            all_items.Add("Journals");
            all_items.Add("Books");

            ViewBag.AllItems = all_items;
        }

        public ActionResult test()
        {

            

            ViewBag.DataTable = db.Items.ToList();

            return View("test");
        }


        public ActionResult Drop()
        {
            string some = Request.Form["DropTypes"].ToString();
            
            switch (some)
            {
                case "Books":
                    foreach (var item in db.Books.ToList())
                    {
                        db_of_Items.Create(new Item { Id = item.Id, Name = item.Name, Author = item.Author, Price = item.Price, Number = "-", Type = "Book" });
                    }
                    db_of_Items.Save();

                    ViewBag.DataTable = db.Books.ToList();
                    break;
                case "Journals":
                    db.Items.RemoveRange(current);
                    foreach (var item in db.Journales.ToList())
                    {
                        db_of_Items.Create(new Item { Id = item.Id, Name = item.Name, Author = item.Author, Price = item.Price, Number = item.Number, Type = "Jurnal" });
                    }
                    db_of_Items.Save();

                    ViewBag.DataTable = db.Journales.ToList();
                    break;
            }
            db_of_Items.Save();
            return View("test");
        }

        
       
        public ActionResult Details(int id)
        {
            foreach (var item in current)
            {
                db.Items.Add(item);
            }
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
        public ActionResult Edit(Item comp)
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