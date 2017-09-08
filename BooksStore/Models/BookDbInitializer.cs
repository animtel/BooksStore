using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksStore.Models
{
    //для работы этого класса и заполнения бд при старте программы, goto Global.asax
    public class BookDbInitializer : DropCreateDatabaseAlways<TestBD> // позволяет при каждом запуске приложения, заполнять бд заново
    {
        public void AddToDB(string name_of_book, string author, int price)
        {
            TestBD db = new TestBD();
            db.Books.Add(new Book { Name = $"{name_of_book}", Author = $"{author}", Price = price });
        }
        protected override void Seed(TestBD db)
        {


            db.Journales.Add(new Journal { Name = "Война и мир", Author = "Л. Толстой", Price = 220 , Number = "1" }); // добавляем каждый объект в бд



            //db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
            //db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Price = 150 });
            //db.Books.Add(new Book { Name = "Some", Author = "А. Чехов", Price = 160 });
            //db.Books.Add(new Book { Name = "Word", Author = "А. Чехов", Price = 170 });
            //db.Books.Add(new Book { Name = "Project", Author = "А. Чехов", Price = 180 });
            //db.Books.Add(new Book { Name = "Interest", Author = "А. Чехов", Price = 190 });
            //db.Books.Add(new Book { Name = "Kendo", Author = "А. Чехов", Price = 200 });
            //db.Books.Add(new Book { Name = "UI", Author = "А. Чехов", Price = 210 });
            //db.Books.Add(new Book { Name = "loop", Author = "А. Чехов", Price = 220 });

            base.Seed(db);
        }
    }
}