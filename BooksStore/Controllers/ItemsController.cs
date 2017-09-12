using BooksStore.Models;
using BooksStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class ItemsController : Controller
    {
        private ItemSevice _serv;
        private JournalService _service_of_journal;
        private BookService _service_of_book;
        public ItemsController()
        {
            _serv = new ItemSevice();
            _service_of_book = new BookService();
            _service_of_journal = new JournalService();
        }

        public void DeletElements()
        {
            for (int i = 0; i < _serv.GetItemList().ToList().Count+1; i++)
            {
                _serv.Delete(i);
            }
        }

        // GET: Items
        public ActionResult Index()
        {
            int id_of_items = 0;
            DeletElements();

            foreach (var item in _service_of_book.GetBookList())
            {

                _serv.Add(new Item{ Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = "-", Type = "Book" });

            }

            foreach (var item in _service_of_journal.GetJournalsList())
            {

                _serv.Add(new Item { Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = item.Number, Type = "Jurnal" });

            }
            _serv.Save();
            
            ViewBag.DataTable = _serv.GetItemList();
            return View();
        }



    }
}