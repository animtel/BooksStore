using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksStore.Repository
{
    public class SQLPaperRepository : IRepository<Paper>
    {
        private TestBD db;

        public SQLPaperRepository()
        {
            this.db = new TestBD();
        }

        public IEnumerable<Paper> GetItemList()
        {
            return db.Papers;
        }

        public Paper GetItem(int id)
        {
            return db.Papers.Find(id);
        }

        public void Create(Paper paper)
        {
            db.Papers.Add(paper);
        }

        public void Update(Paper paper)
        {
            db.Entry(paper).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Paper paper = db.Papers.Find(id);
            if (paper != null)
                db.Papers.Remove(paper);
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