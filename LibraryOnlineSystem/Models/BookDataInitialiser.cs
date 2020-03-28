using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryOnlineSystem.Models;
namespace LibraryOnlineSystem
{
    public class BookDataInitialiser: DropCreateDatabaseAlways<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            //------------------------------------- PEOPLE
             Book book=new Book()
            {
                DateOfPublication = DateTime.Now,
                Genre = Genre.Action,
                Name = "Harry Potter",
                Overview = "good book",
                Publisher = "JKR",
                BookId = 3,
                Quantity=4
            };
             context.Books.Add(book);
            Book book2 = new Book()
             {
                DateOfPublication = DateTime.Now,
                Genre = Genre.Horror,
                Name = "Lord of The Rings",
                Overview = "Fantasy book",
                Publisher = "Tolkien",
                BookId = 4,
                Quantity = 5
            };
            context.Books.Add(book2);
            BookCode bookCode = new BookCode()
            {
                BookCodeId = 1,
                BookId = 2,
                BookSerialNumber = "33233"
            };
            context.BookCodes.Add(bookCode);
            BookCode bookCode1 = new BookCode()
            {
                BookCodeId = 2,
                BookId = 2,
                BookSerialNumber = "33234"
            };
            context.BookCodes.Add(bookCode1);
            //=------------------ User

            User user=new User();
            {
                user.Name = "Matt";
                user.Surname="S";
                user.DateOfBirth = DateTime.Now;
                user.Email = "abc123@gmail.com";
                user.HouseNo = 35;
                user.Password = "123";
                user.UserRole = "User";
                user.ZipCode = 9999;
                user.UserId = 1;
               
            }
            context.Users.Add((user));
            User user2 = new User();
            {
                user2.Name = "Matt2";
                user2.Surname = "S";
                user2.DateOfBirth = DateTime.Now;
                user2.Email = "admin@gmail.com";
                user2.HouseNo = 35;
                user2.Password = "123";
                user2.UserRole = "Admin";
                user2.ZipCode = 9999;
                user2.UserId = 2;
                
            }
            context.Users.Add((user2));
         
            //----------------- RequestBook
            RequestBook requestBook = new RequestBook();
            {
                requestBook.RequestBookId = 1;
                requestBook.BookId = 1;
                requestBook.UserId = 1;
                requestBook.Note = "heh";

            }
            context.RequestBooks.Add(requestBook);
            ////============ Payment
            Payment payment = new Payment();
            {
                payment.UserId = 1;
                payment.Amount = 20;
                payment.DatePaid = DateTime.Now;
                payment.PaymentId = 1;
                payment.Status = "Paid";
                
            }
            context.Payments.Add(payment);

            //=============Reviews
            BookReview bookReview = new BookReview();
            {
                bookReview.BookId = 1;
                bookReview.BookReviewId = 1;
                bookReview.Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                                     " Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
                                     "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident," +
                                     " sunt in culpa qui officia deserunt mollit anim id est laborum.";
                bookReview.UserId = 1;
                bookReview.DatePosted = DateTime.Now;

            }
            context.BookReviews.Add(bookReview);

            BookReview bookReview1 = new BookReview();
            {
                bookReview1.BookId = 2;
                bookReview1.BookReviewId = 2;
                bookReview1.Content = "content testing";
                bookReview1.UserId = 1;
                bookReview1.DatePosted = DateTime.Now;

            }
            context.BookReviews.Add(bookReview1);
            //------------------------------------------------
            Booking booking = new Booking();
            {
                booking.BookCodeId = 1;
                booking.userId = 1;
                booking.BookingId = 1;
                booking.Book =book2;
                booking.DateCreated = new DateTime(2019, 12, 05);
                booking.DateDue = new DateTime(2019, 12, 12);
                booking.User = user;
                booking.IsOverdue = true;
            }
            context.Bookings.Add(booking);

            //--------------------------Payment
          
            //------------------------------------------------------ COMMENTS



            Comment com4 = new Comment();
            com4.CommentID = 4;
            com4.AuthorID = "1";
            com4.Content = " Great book! ";
            com4.PersonID = 1;
            com4.BookID = 1;
            com4.PostID = 1;
            com4.isBlocked = true;
            com4.UserRating = 5.7f;
            context.Comment.Add(com4);


            //------------------------------------------------------ 

            //-------------------------------------------------------------CommentReply


            //CommentReply comReply3 = new CommentReply() { CommentID = 2, CommentReplyID = 4, AuthorID = "1", PersonID = 1, BookID = 1, PostID = 1, Content = "this is a reply" };
            //context.CommentReply.Add(comReply3);
            CommentReply comReply4 = new CommentReply() { CommentID = 4, CommentReplyID = 5, AuthorID = "1", PersonID = 1, BookID = 1, PostID = 1, Content = "reply1-2" };
            context.CommentReply.Add(comReply4);
            //CommentReply comReply5 = new CommentReply() { CommentID = 2, CommentReplyID = 6, AuthorID = "1", PersonID = 1, BookID = 1, PostID = 1, Content = "this is a reply1-3" };
            //context.CommentReply.Add(comReply5);

            //base.Seed(context);


            //--------------------------------------------------------------Loan info

          
        }

        
    }
}