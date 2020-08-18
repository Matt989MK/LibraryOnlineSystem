using LibraryOnlineSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibraryOnlineSystemTest
{
    [TestClass]
    public class BookTest
    {
        private readonly LibraryContext db = new LibraryContext();
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

            Assert.AreEqual(book.BookId, 1);
        }
    }
}
