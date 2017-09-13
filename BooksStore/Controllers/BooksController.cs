﻿using BooksStore.Models;
using BooksStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class BooksController : Controller
    {
        private BookService _bookservice;
        
        public BooksController()
        {
             _bookservice = new BookService();
        }

        public ActionResult Index()
        {
            ViewBag.DataTable = _bookservice.GetBookList().ToList();

            return View("Index");
        }
       
        public ActionResult Details(int id)
        {
            foreach (var item in _bookservice.GetBookList())
            {
                _bookservice.Add(item);
            }
            Book it = _bookservice.GetBook(id);
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
        public ActionResult Create(Book item)
        {
            if (ModelState.IsValid)
            {
                _bookservice.Add(item);
                _bookservice.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Edit(int id)
        {
            Book comp = _bookservice.GetBook(id);
            if (comp != null)
            {
                return PartialView("Edit", comp);
            }
            return View("test");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            _bookservice.Update(book);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            Book comp = _bookservice.GetBook(id);
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
            Book comp = _bookservice.GetBook(id);

            if (comp != null)
            {
                _bookservice.Delete(id);
                _bookservice.Save();
            }
            return RedirectToAction("Index");
        }
    }
}