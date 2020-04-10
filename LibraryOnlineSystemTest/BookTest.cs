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

            Book book = new Book
            {
                BookId = 1,
                DateOfPublication = DateTime.Today,
                Genre = Genre.Fantasy,
                Name = "Book1",
                Overview = "A great book test",
                Publisher = "TestPublisher",
                Rating = 5
            };
            //db.Books.Add(book);
            //db.SaveChanges();
            
            Assert.AreEqual(book.BookId,1);
        }
    }
}
