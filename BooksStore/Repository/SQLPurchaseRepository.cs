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
        private TestBD _db;

        public SQLPurchaseRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Purchase> GetItemList()
        {
            return _db.Purchases;
        }

        public Purchase GetItem(int id)
        {
            return _db.Purchases.Find(id);
        }

        public void Create(Purchase purchase)
        {
            _db.Purchases.Add(purchase);
        }

        public void Update(Purchase purchase)
        {
            _db.Entry(purchase).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Purchase purchase = _db.Purchases.Find(id);
            if (purchase != null)
                _db.Purchases.Remove(purchase);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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