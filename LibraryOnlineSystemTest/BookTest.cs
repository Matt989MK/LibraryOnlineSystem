using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryOnlineSystem;
using LibraryOnlineSystem.Models;

namespace LibraryOnlineSystemTest
{
    [TestClass]
    public class BookTest
    {
        private LibraryContext db = new LibraryContext();
        [TestMethod]
        public void AddNewBook()
        {

            Book book = new Book();
            book.BookId = 1;
            book.DateOfPublication= DateTime.Today;
            book.Genre = Genre.Fantasy;
            book.Name = "Book1";
            book.Overview = "A great book test";
            book.Publisher = "TestPublisher";
            book.Rating = 5;
            db.Books.Add(book);
            db.SaveChanges();
            Assert.AreEqual(db.Books.Where(a=>a.BookId==book.BookId),book);
        }
    }
}
