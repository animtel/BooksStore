using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksStore.Repository
{
    public class SQLPurchaseRepository : IRepository<Purchase>
    {
        private TestBD db;

        public SQLPurchaseRepository()
        {
            this.db = new TestBD();
        }

        public IEnumerable<Purchase> GetItemList()
        {
            return db.Purchases;
        }

        public Purchase GetItem(int id)
        {
            return db.Purchases.Find(id);
        }

        public void Create(Purchase purchase)
        {
            db.Purchases.Add(purchase);
        }

        public void Update(Purchase purchase)
        {
            db.Entry(purchase).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase != null)
                db.Purchases.Remove(purchase);
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