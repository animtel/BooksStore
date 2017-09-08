using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksStore.Repository
{
    public class SQLItemRepository : IRepository<Item>
    {
        private TestBD db;

        public SQLItemRepository()
        {
            this.db = new TestBD();
        }

        public IEnumerable<Item> GetItemList()
        {
            return db.Items;
        }

        public Item GetItem(int id)
        {
            return db.Items.Find(id);
        }

        public void Create(Item item)
        {
            db.Items.Add(item);
        }

        public void Update(Item paper)
        {
            db.Entry(paper).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Item paper = db.Items.Find(id);
            if (paper != null)
                db.Items.Remove(paper);
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