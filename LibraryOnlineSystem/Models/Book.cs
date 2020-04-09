using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.UI.WebControls;
using LibraryOnlineSystem.Models;
using LibraryOnlineSystem.Queries;
namespace LibraryOnlineSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = "Please put name of a book.")]
        public string Name{ get; set; }
        [Required(ErrorMessage = "Please choose Genre of the book.")]
        public Genre Genre{ get; set; }
        [Required(ErrorMessage = "Please Input a date")]
        public DateTime DateOfPublication{ get; set; }
        [Required(ErrorMessage = "Please Input an overview")]
        public string Overview{ get; set; }
        [Required(ErrorMessage = "Please Input a publisher")]
        public string Publisher{ get; set; }
        public List<BookReview> bookReviews { get; set; }
        public virtual List<Author> Authors { get; set; }
        public virtual List<Comment> Comment { get; set; }//added this
        [Required(ErrorMessage = "Please Input a rating")]
        public float Rating { get; set; }
        public List<BookCode> BookCode { get; set; }
        public string BookImage { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
       public string Link{get;set;}
    
    }
}