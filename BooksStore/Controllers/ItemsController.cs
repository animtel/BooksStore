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
        private ItemSevice _itemservice;
        private JournalService _service_of_journal;
        private BookService _service_of_book;
        public ItemsController()
        {
            _itemservice = new ItemSevice();
            _service_of_book = new BookService();
            _service_of_journal = new JournalService();
        }

        public void DeletElements()
        {
            for (int i = 0; i < _itemservice.GetItemList().ToList().Count+1; i++)
            {
                _itemservice.Delete(i);
            }
        }

        // GET: Items
        public ActionResult Index()
        {
            int id_of_items = 0;
            DeletElements();

            foreach (var item in _service_of_book.GetBookList())
            {

                _itemservice.Add(new Item{ Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = "-", Type = "Book" });

            }

            foreach (var item in _service_of_journal.GetJournalsList())
            {

                _itemservice.Add(new Item { Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = item.Number, Type = "Jurnal" });

            }
            _itemservice.Save();
            
            ViewBag.DataTable = _itemservice.GetItemList();
            return View();
        }



    }
}