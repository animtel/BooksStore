using BooksStore.Models;
using BooksStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksStore.Services
{
    public class JournalService
    {
        private IRepository<Journal> _db_of_journals;
        public JournalService()
        {
            _db_of_journals = new SQLJournalRepository();
        }

        public IEnumerable<Journal> GetJournalsList()
        {
            return _db_of_journals.GetItemList();
        }

        public Journal GetJournal(int id)
        {
            return _db_of_journals.GetItem(id);
        }

        public void Add(Journal journal)
        {
            _db_of_journals.Create(journal);
        }

        public void Update(Journal journal)
        {
            _db_of_journals.Update(journal);
        }

        public void Delete(int id)
        {
            _db_of_journals.Delete(id);
        }

        public void Save()
        {
            _db_of_journals.Save();
        }

        public void Dispose()
        {
            _db_of_journals.Dispose();
        }
    }
}