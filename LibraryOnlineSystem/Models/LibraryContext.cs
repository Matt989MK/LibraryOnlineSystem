using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LibraryOnlineSystem.Models
{
    public class LibraryContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BooksToAuthors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentReply> CommentReply { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<BookCode> BookCodes { get; set; }
        public DbSet<BookReserve> BookReserves { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<LibraryRegulations> LibraryRegulations { get; set; }
        public LibraryContext()
            :base ("name=LibraryDBConnectionString")
        {
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        //    modelBuilder.Entity<Payment>()
        //        .HasRequired(f => f.UserId)
        //        .WithRequiredDependent()
        //        .WillCascadeOnDelete(false);

        //}
    }

}