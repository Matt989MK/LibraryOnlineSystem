using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using LibraryOnlineSystem.Models;
using LibraryOnlineSystem.Queries;
namespace LibraryOnlineSystem
{
    public class Book
    {
        public int BookId { get; set; }

        public string Name
        { get; set; }

        public Genre Genre
        { get; set; }

        

        public DateTime DateOfPublication
        { get; set; }

        public string Overview
        { get; set; }
    

        public string Publisher
    { get; set; }

        public int Quantity { get; set; }

        public List<BookReview> bookReviews { get; set; }
        public virtual List<Author> Authors { get; set; }
        public virtual List<Comment> Comment { get; set; }//added this
        public void GetBookList()
        {
            throw new System.NotImplementedException();
        }

        public void GetBookById()
        {
            throw new System.NotImplementedException();
        }

        public void AddBook()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBook()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBook()
        {
            throw new System.NotImplementedException();
        }

        public void GetBookBorrowedByUserId()
        {
            throw new System.NotImplementedException();
        }

        // private string q = QueriesBook.getSelectedBook(3);
    }
}