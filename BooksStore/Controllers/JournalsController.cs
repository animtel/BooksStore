using BooksStore.Models;
using BooksStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class JournalsController : Controller
    {
        JournalService _journalservice;


        public JournalsController()
        {
            _journalservice = new JournalService();
        }

        public ActionResult Index()
        {
            ViewBag.DataTable = _journalservice.GetJournalsList().ToList();

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            foreach (var item in _journalservice.GetJournalsList())
            {
                _journalservice.Add(item);
            }
            Journal it = _journalservice.GetJournal(id);
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
        public ActionResult Create(Journal journal)
        {
            if (ModelState.IsValid)
            {
                _journalservice.Add(journal);
                _journalservice.Save();
                return RedirectToAction("Index");
            }
            return View(journal);
        }

        public ActionResult Edit(int id)
        {
            Journal comp = _journalservice.GetJournal(id);
            if (comp != null)
            {
                return PartialView("Edit", comp);
            }
            return View("test");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Journal journal)
        {
            _journalservice.Update(journal);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Journal comp = _journalservice.GetJournal(id);
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
            Journal comp = _journalservice.GetJournal(id);

            if (comp != null)
            {
                _journalservice.Delete(id);
                _journalservice.Save();
            }
            return RedirectToAction("Index");
        }
    }
}