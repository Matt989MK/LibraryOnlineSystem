using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace LibraryOnlineSystem.Models
{
    public class Book
    {
        //[HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Please put name of a book."), MaxLengthAttribute(40)]
        public string Name { get; set; }
        //  [Required(ErrorMessage = "Please choose Genre of the book.")]
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "Please Input a date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfPublication { get; set; }
        [Required(ErrorMessage = "Please Input an overview")]
        [MaxLength(200, ErrorMessage = "Overview cannot be longer than 430 characters")]
        public string Overview { get; set; }
        [Required(ErrorMessage = "Please Input a publisher"), MaxLengthAttribute(40)]
        public string Publisher { get; set; }
        public string BookReviews { get; set; }
        [NotMapped]
        public virtual List<Author> Authors { get; set; }
        public virtual List<Comment> Comment { get; set; }//added this
        [Required(ErrorMessage = "Please Input a rating")]
        public float Rating { get; set; }
        public List<BookCode> BookCode { get; set; }
        //  public string BookImage { get; set; }
        //  [Required(ErrorMessage = "Please upload an image")]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public string Link { get; set; }

    }
}