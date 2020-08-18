using System;
namespace LibraryOnlineSystem.Models
{
    public class BookReview
    {
        public int BookReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
    }
}