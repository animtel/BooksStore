using BooksStore.Models;
using BooksStore.Services;
using System.Linq;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class BooksController : Controller
    {
        private BookService _book_service;
        
        public BooksController()
        {
             _book_service = new BookService();
        }

        public ActionResult Index()
        {
            ViewBag.DataTable = _book_service.GetBookList().ToList();

            return View("Index");
        }
       
        public ActionResult Details(int id)
        {
            foreach (var item in _book_service.GetBookList())
            {
                _book_service.Add(item);
            }
            Book it = _book_service.GetBook(id);
            if (it != null)
            {
                return PartialView("Details", it);
            }
            return View("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _book_service.Add(book);
                _book_service.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Edit(int id)
        {
            Book book = _book_service.GetBook(id);
            if (book != null)
            {
                return PartialView("Edit", book);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            _book_service.Update(book);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            Book comp = _book_service.GetBook(id);
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
            Book comp = _book_service.GetBook(id);

            if (comp != null)
            {
                _book_service.Delete(id);
                _book_service.Save();
            }
            return RedirectToAction("Index");
        }
    }
}