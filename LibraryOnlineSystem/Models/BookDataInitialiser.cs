﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryOnlineSystem.Models;
namespace LibraryOnlineSystem
{
    public class BookDataInitialiser: DropCreateDatabaseAlways<LibraryContext>//CreateDatabaseIfNotExists<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            LibraryRegulations libraryRegulations = new LibraryRegulations()
            {
                Fine =3,
                BorrowTime = 7
            };
            //------------------------------------- PEOPLE
             Book book=new Book()
            {
                DateOfPublication = DateTime.Now,
                Genre = Genre.Action,
                Name = "Harry Potter",
                Overview = "good book",
                Publisher = "JKR",
                
                BookId = 1,
                Rating =5,
                //BookImage = "~/Images/BookImageTest.png",
             };
             context.Books.Add(book);
            Book book2 = new Book()
             {
                DateOfPublication = DateTime.Now,
                Genre = Genre.Horror,
                Name = "Lord of The Rings",
                Overview = "Fantasy book",
                Publisher = "Tolkien",
                BookId = 2,
                Rating = 5,
                //BookImage = "~/Images/BookImageTest.png",
                Link = "http://www.china.doingbusinessguide.co.uk/media/880543/Doing_Business_in_China_Guide_PDF.pdf",
                
                
            };
            context.Books.Add(book2);

           
            Book book3 = new Book()
            {
                DateOfPublication = DateTime.Now,
                Genre = Genre.Horror,
                Name = "James Bond",
                Overview = "Spy book",
                Publisher = "007",
                BookId = 3,
                Rating = 5,
                //BookImage = "~/Images/BookImageTest.png",
            };
            context.Books.Add(book3);
            Book book4 = new Book()
            {
                DateOfPublication = DateTime.Now,
                Genre = Genre.Horror,
                Name = "Doom",
                Overview = "Slayer book",
                Publisher = "Bethesda",
               // BookImage = "~/Images/BookImageTest.png",
                BookId = 6,
                Rating = 5,
            };
            context.Books.Add(book4);
           
            BookCode bookCode = new BookCode()
            {
                BookCodeId = 1,
                BookId = 1,
                BookSerialNumber = "33233",
                IsInLibrary =true
            };
            context.BookCodes.Add(bookCode);
            BookCode bookCode1 = new BookCode()
            {
                BookCodeId = 2,
                BookId = 1,
                BookSerialNumber = "33234",
                IsInLibrary = true
            };
            context.BookCodes.Add(bookCode1);
            BookCode bookCode2 = new BookCode()
            {
                BookCodeId = 3,
                BookId = 1,
                BookSerialNumber = "33235",
                IsInLibrary = false
            };
            context.BookCodes.Add(bookCode2);
            BookCode bookCode3 = new BookCode()
            {
                BookCodeId = 4,
                BookId = 2,
                BookSerialNumber = "33236",
                IsInLibrary = true
            };
            context.BookCodes.Add(bookCode3);
            BookCode bookCode4 = new BookCode()
            {
                BookCodeId = 5,
                BookId = 2,
                BookSerialNumber = "33237",
                IsInLibrary = false
            };
            context.BookCodes.Add(bookCode4);

            BookCode bookCode5 = new BookCode()
            {
                BookCodeId = 4,
                BookId = 3,
                BookSerialNumber = "33238",
                IsInLibrary = false
            };
            context.BookCodes.Add(bookCode5);
            BookCode bookCode6 = new BookCode()
            {
                BookCodeId = 5,
                BookId = 3,
                BookSerialNumber = "33239",
                IsInLibrary = false
            };
            context.BookCodes.Add(bookCode6);
            //=------------------ User

            User user=new User();
            {
                user.Name = "Matt";
                user.Surname="Sean";
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
                user2.Name = "Ethan";
                user2.Surname = "Jason";
                user2.DateOfBirth = DateTime.Now;
                user2.Email = "admin@gmail.com";
                user2.HouseNo = 35;
                user2.Password = "123";
                user2.UserRole = "Admin";
                user2.ZipCode = 9999;
                user2.UserId = 2;
                
            }
            context.Users.Add((user2));

            User user3 = new User();
            {
                user3.Name = "John";
                user3.Surname = "Smith";
                user3.DateOfBirth = DateTime.Now;
                user3.Email = "abc1234@gmail.com";
                user3.HouseNo = 22;
                user3.Password = "123";
                user3.UserRole = "User";
                user3.ZipCode = 1254;
                user3.UserId = 3;

            }
            context.Users.Add((user3));

            ////============ Payment
           
            BookReserve bookReserve = new BookReserve()
            {
                BookReserveId = 1,
                BookCodeId = 1,
                UserId = 1,
                ReservationRequestTime = DateTime.Now,
                
            };
            context.BookReserves.Add(bookReserve);
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
                booking.UserId = 1;
                booking.BookId = 1;
                booking.DateCreated = new DateTime(2019, 12, 05);
                booking.DateReturned = new DateTime(2019, 12, 12);
                
                }
            context.Bookings.Add(booking);
            Booking booking1 = new Booking();
            {
                booking1.BookCodeId = 1;
                booking1.UserId = 1;
                booking1.BookId = 2;
                booking1.DateCreated = new DateTime(2021, 12, 05);
                booking1.DateReturned = new DateTime(2021, 12, 12);

            }
            context.Bookings.Add(booking1);
            Booking booking2 = new Booking();
            {
                booking2.BookCodeId = 1;
                booking2.UserId = 2;
                booking2.BookId = 2;
                booking2.DateCreated = new DateTime(2021, 12, 05);
                booking2.DateReturned = new DateTime(2021, 12, 12);
                
            }
            context.Bookings.Add(booking2);
            Booking booking3 = new Booking();
            {
                booking3.BookCodeId = 3;
                booking3.UserId = 2;
                booking3.BookId = 1;
                booking3.DateCreated = new DateTime(2019, 12, 05);
                booking3.DateReturned = null;

            }
            context.Bookings.Add(booking3);
            //--------------------------Payment
            PaymentLibrary payment = new PaymentLibrary();
            {
              //  Booking bookingPayment1 = context.Bookings.Where(a => a.BookingId == 1).Single();
                payment.UserId = 1;
                payment.Amount = 20;
                payment.DatePaid = null;
                payment.PaymentLibraryId = 1;
                payment.Status = "Unpaid";
                payment.BookingId = 2;
                // payment.Booking = bookingPayment1;

            }
            context.Payments.Add(payment);
            PaymentLibrary payment1 = new PaymentLibrary();
            {
                //Booking bookingPayment2 = context.Bookings.Where(a => a.BookingId == 2).Single();
                payment1.UserId = 1;
                payment1.Amount = 10;
                payment1.DatePaid = DateTime.Now;
                payment1.PaymentLibraryId = 2;
                payment1.Status = "Paid";
                payment.BookingId = 1;
                //  payment.Booking = bookingPayment2;

            }
            context.Payments.Add(payment1);
            //Payment payment2 = new Payment();
            //{
            //    payment2.UserId = 1;
            //    payment2.Amount = 5;
            //    payment2.DatePaid = null;
            //    payment2.PaymentLibraryId = 3;
            //    payment2.Status = "Unpaid";
            //    payment.BookingId = 3;

            //}
            //context.Payments.Add(payment2);
            //------------------------------------------------------ COMMENTS
            Comment com2 = new Comment();
            com2.CommentId = 4;
            com2.AuthorId = "1";
            com2.Content = " Great book! ";
            com2.PersonId = 3;
            com2.BookId = 1;
            com2.PostId = 1;
            com2.IsBlocked = false;
            com2.UserRating = 6f;
            context.Comment.Add(com2);

            Comment com3 = new Comment();
            com3.CommentId = 1;
            com3.AuthorId = "1";
            com3.Content = " Great book! ";
            com3.PersonId = 1;
            com3.BookId = 2;
            com3.PostId = 1;
            com3.IsBlocked = false;
            com3.UserRating = 6f;
            context.Comment.Add(com3);

            Comment com4 = new Comment();
            com4.CommentId = 4;
            com4.AuthorId = "1";
            com4.Content = " Great book! ";
            com4.PersonId = 1;
            com4.BookId =3;
            com4.PostId = 1;
            com4.IsBlocked = false;
            com4.UserRating = 6f;
            context.Comment.Add(com4);

            Comment com5 = new Comment();
            com5.CommentId = 5;
            com5.AuthorId = "3";
            com5.Content = " Great book! ";
            com5.PersonId = 3;
            com5.BookId = 4;
            com5.PostId = 1;
            com5.IsBlocked = false;
            com5.UserRating = 5f;
            context.Comment.Add(com5);
            //------------------------------------------------------ 

            //-------------------------------------------------------------CommentReply


            //CommentReply comReply3 = new CommentReply() { CommentId = 2, CommentReplyID = 4, AuthorId = "1", PersonId = 1, BookId = 1, PostId = 1, Content = "this is a reply" };
            //context.CommentReply.Add(comReply3);
            //CommentReply comReply4 = new CommentReply() { CommentId = 4, CommentReplyID = 5, AuthorId = "1", PersonId = 1, BookId = 1, PostId = 1, Content = "reply1-2" };
            //context.CommentReply.Add(comReply4);
            //CommentReply comReply5 = new CommentReply() { CommentId = 2, CommentReplyID = 6, AuthorId = "1", PersonId = 1, BookId = 1, PostId = 1, Content = "this is a reply1-3" };
            //context.CommentReply.Add(comReply5);

            //base.Seed(context);


            //--------------------------------------------------------------Loan info

          
        }

        
    }
}