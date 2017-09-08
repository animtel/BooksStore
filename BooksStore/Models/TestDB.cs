﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksStore.Models
{
    public class TestBD : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Journal> Journales { get; set; }


        public DbSet<Item> Items { get; set; }
    }
}