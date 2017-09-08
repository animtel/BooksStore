using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksStore.Repository
{
    public class SQLJournalRepository : IRepository<Journal>
    {
        private TestBD db;

        public SQLJournalRepository()
        {
            this.db = new TestBD();
        }

        public IEnumerable<Journal> GetItemList()
        {
            return db.Journales;
        }

        public Journal GetItem(int id)
        {
            return db.Journales.Find(id);
        }

        public void Create(Journal journal)
        {
            db.Journales.Add(journal);
        }

        public void Update(Journal journal)
        {
            db.Entry(journal).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Journal journal = db.Journales.Find(id);
            if (journal != null)
                db.Journales.Remove(journal);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}