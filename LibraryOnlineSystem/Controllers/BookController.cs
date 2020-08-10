using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryOnlineSystem.Models;
using Microsoft.Ajax.Utilities;

namespace LibraryOnlineSystem.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext db = new LibraryContext();
        // GET: Book
        //public ActionResult Index()
        //{
        //    CheckLogin();

        //    List<Book> listOfBook = new List<Book>();
        //    listOfBook = db.Books.ToList();
        //    //List<Author> listOfAuthor=new List<Author>();




        //    //listOfBook.Add(book);
        //    return View(listOfBook);
        //}
        public ActionResult Index(string Name, string Genre, string Rating)
        {
            CheckLogin();
            List<Author> listOfAuthor = new List<Author>();
            List<Book> lstBooks = new List<Book>();
            if (Name != null && Genre == "Any")
            {
                lstBooks = db.Books.Where(a => a.Name.Contains(Name)).ToList();
            }
            else
            {
                lstBooks = db.Books.ToList();
            }




            foreach (Book book in lstBooks)
            {
                try
                {
                    book.Rating = (float)db.Comment.Where(a => a.BookId == book.BookId).Select(a => a.UserRating).Average();
                    book.Rating = (float)Math.Round(book.Rating, 2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    book.Rating = db.Books.Where(a => a.BookId == book.BookId).Select(a => a.Rating).Single();
                }



            }
            if (Genre != null && Genre != "Any")
            {
                lstBooks = lstBooks.Where(i => i.Genre.ToString() == Genre).ToList();

            }
            if (Rating == "Worst") { lstBooks = lstBooks.OrderBy(i => i.Rating).ToList(); }
            else if (Rating == "Best") { lstBooks = lstBooks.OrderByDescending(i => i.Rating).ToList(); }
            //else
            //{ lstBooks = db.Books.Where(i => i.Genre.ToString() == Genre).ToList(); }
            //if (Rating == "Worst") { lstBooks = lstBooks.OrderBy(i => i.Rating).ToList(); }
            //else if (Rating == "Best") { lstBooks = lstBooks.OrderByDescending(i => i.Rating).ToList(); }

            return View(lstBooks);
        }


        public ActionResult ReservedBook(int bookCodeId, int userId)
        {
            BookReserve bookReserve = new BookReserve();
            bookReserve.BookCodeId = bookCodeId;
            bookReserve.ReservationRequestTime = DateTime.Today;
            bookReserve.UserId = userId;
            db.BookReserves.Add(bookReserve);
            db.SaveChanges();

            return View(bookReserve);
        }

        [HttpGet]
        public ActionResult DeleteComment(int? commentId, int commentReplyId)
        {

            Icomments iComments;


            if (commentId != null)
            {
                //Comment comment = db.Comment.Where(a => a.CommentId == commentId).Single();
                ViewBag.ReplyCommentCheck = 0;

                iComments = db.Comment.Where(a => a.CommentId == commentId).Single();
                return View(iComments);
                
            }
            else
            {
                //CommentReply replyComment = db.CommentReply.Where(a => a.CommentReplyID == commentReplyId).Single();

                ViewBag.ReplyCommentCheck = 1;
                iComments = db.CommentReply.Where(a => a.CommentReplyID == commentReplyId).Single();
                ViewBag.CommentReplyDelete = iComments.GetContent();
                return View(iComments);
            }

        }

        [HttpPost]
        public ActionResult DeleteComment(int? commentId,int? commentReplyId)
        {

            //List<CommentReply> commentReplyList = db.CommentReply.ToList();
            //int replyCommentID = commentReplyList.Where(a => a.CommentID == commentId).Single().CommentReplyID;
            if (commentId != null)
            {
                Comment comment = db.Comment.Where(a => a.CommentId == commentId).Single();

                db.Comment.Remove(comment);
            }else 
            {
                CommentReply replyCommentID = db.CommentReply.Where(a => a.CommentReplyID == commentReplyId).Single();
                db.CommentReply.Remove((replyCommentID));
            }
          
            db.SaveChanges();
            return Redirect("/Book/Index");
        }

        [HttpGet]
        public ActionResult CommentReply(int id)
        {
            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            Book book = listOfBook.Where(a => a.BookId == id).Single();
            book.BookReviews = listOfBookReviews;
          


            return View(book);
        }


        [HttpPost]
        public ActionResult CommentReply()
        {
            int id = Convert.ToInt32(Request.Params["BookId"]);
            int commentID = Convert.ToInt32(Request.Params["CommentId"]);
            List <User> listOfUser= db.Users.ToList();
            User user = listOfUser.Where(a => a.UserId == Convert.ToInt32(Session["UserId"])).Single();
            Book book = db.Books.Find(id);//
            string authorName = user.Name+" " + user.Surname;
            CommentReply commentReply = new CommentReply();
            commentReply.Content = Request.Params["NewReply"];
            commentReply.CommentID = commentID;

            commentReply.AuthorID = authorName;
            commentReply.PostID = 1;
            commentReply.BookID = id;
            commentReply.PersonID = 1;
            float.TryParse(Request.Params["NewUserRating"], out float results);

            db.CommentReply.Add(commentReply);
            db.SaveChanges();



            List<Comment> lstComment = db.Comment.Where(c => c.BookId == id).ToList();
            foreach (Comment item in lstComment)
            {

                List<CommentReply> lstCommentReply = new List<CommentReply>();
                if (db.CommentReply.Where(c => c.CommentID == item.CommentId).ToList() != null)//
                {
                    lstCommentReply = db.CommentReply.Where(c => c.CommentID == item.CommentId).ToList();
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            book.BookReviews = listOfBookReviews;

            return RedirectToAction("Details", new { booksId = id });


        }

        // GET: Book/Details/5
        [HttpGet]
        public ActionResult Details(int booksId)
        {
            //DAOBook daoBook=new DAOBook();
            //Book book = daoBook.getSelectedBook(id);
            ViewBag.gaveRating = false;
            List<User> listOfUser=new List<User>();
            listOfUser = db.Users.ToList();
            User user = listOfUser.Where(a => a.UserId ==Convert.ToInt32(Session["UserId"])).Single(); // GET USER ID
           
            List<Comment> listOfComments = db.Comment.Where(a=>a.BookId==booksId).ToList();

            foreach (var comment in listOfComments)
            {
                if (comment.PersonId == user.UserId)
                {
                    ViewBag.gaveRating = true;
                }
            }


          
            

            // CHECK IF USER BANNED




            int counter = 0;
            foreach (var _payment in user.ListOfPayment)
            {
                if (_payment.DatePaid.HasValue)
                {
                    counter++;
                }
            }

            if (counter == user.ListOfPayment.Count)
            {
                user.IsBanned = false;
            }
            else
            {
                user.IsBanned = true;
            }

            db.Users.AddOrUpdate(user);




            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == booksId).ToList();
            Book book = listOfBook.Where(a => a.BookId == booksId).Single();
            book.BookReviews = listOfBookReviews;
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId == booksId ).ToList();//&& a.IsInLibrary == true
            book.BookCode = bookCodesList;
            int bookCurrentlyStocked = bookCodesList.Count;
            List<BookAuthors> bookAuthors = db.BookAuthors.Where(a => a.BookId == booksId).ToList();
            List<Author> authorList = new List<Author>();
            foreach (var bookAuthor in bookAuthors)
            {
                Author author = db.Authors.Where(a => a.Id == bookAuthor.AuthorId).Single();
                authorList.Add(author);
            }
            book.Authors = authorList;
            foreach (var bookCode in bookCodesList)
            {
                if (bookCode.IsInLibrary == false)
                {
                    bookCurrentlyStocked--;
                }
            }

            if (user.IsBanned==true)
            {
                ViewBag.isBanned = 1;
            }
            else
            {
                ViewBag.isBanned = 0;
            }
            //ViewBag.Test = db.BookCodes.Where(a => a.BookId == booksId).First();

            ViewBag.bookInStock = bookCurrentlyStocked;
            return View(book);
        }

        [HttpPost]
        public ActionResult Details()
        {
            List<User> listOfUser = new List<User>();
            listOfUser = db.Users.ToList();
            User user = listOfUser.Where(a => a.UserId == Convert.ToInt32(Session["UserId"])).Single();
            int id = Convert.ToInt32(Request.Params["BookId"]);
            Book book = db.Books.Find(id);//
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId == id).ToList();
            book.BookCode = bookCodesList;
            Comment comment = new Comment();
            comment.Content = Request.Params["NewComment"];
            comment.AuthorId = user.Name+" "+user.Surname;
            
            comment.PostId = 1;
            comment.BookId = id;
            comment.PersonId = 1;
            string checkNull = Request.Params["NewUserRating"];
            float.TryParse(Request.Params["NewUserRating"], out float results);
            if (checkNull != null)
            {
                comment.UserRating = results;

            }
            else
            {
                comment.UserRating = null;
            }
         






            List<Comment> lstComment = db.Comment.Where(c => c.BookId == id).ToList();
            foreach (Comment item in lstComment)
            {

                CommentReply commentReply = new CommentReply();
                List<CommentReply> lstCommentReply = new List<CommentReply>();
                if (db.CommentReply.Where(c => c.CommentID == item.CommentId).ToList() != null)//
                {
                    lstCommentReply = db.CommentReply.Where(c => c.CommentID == item.CommentId).ToList();
                }
            }



            if ((comment.UserRating <= 10 && comment.UserRating >= 0.0) || comment.UserRating==null)
            {
                db.Comment.Add(comment);

                db.SaveChanges();

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            book.BookReviews = listOfBookReviews;

            return View(book);

        }






        public ActionResult BorrowBook(int userId, int bookId)
        {
            Book book = new Book();
            book = db.Books.Where(a => a.BookId == bookId).Single();
            User user = new User();
            user = db.Users.Where(a => a.UserId == userId).Single();



            Booking booking = new Booking();
            BookCode bookCode = new BookCode();

            bookCode = db.BookCodes.Where(a => a.BookId == bookId && a.IsInLibrary == true).First();
            booking.Book = book;
            booking.BookId = bookId;
            booking.User = user;
            booking.DateCreated = DateTime.Now;
            booking.DateReturned = null;
            booking.BookCodeId = bookCode.BookCodeId;
            bookCode.IsInLibrary = false;
            db.Bookings.Add(booking);
            db.BookCodes.AddOrUpdate(bookCode);
            db.SaveChanges();
            return View(booking);



        }


        public ActionResult ReserveBook(int userId, int bookId)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author author)
        {

            author.Name = Request["Name"];
            author.Surname = Request["Surname"];

            db.Authors.Add(author);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult AddAuthorsToBook(int bookId, int authorId)
        {
            if (bookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Author> authorList = db.Authors.ToList();
            if (authorId != 0)
            {
                Book book = db.Books.Find(bookId);
                List<BookAuthors> bookAuthorses = new List<BookAuthors>();
              //  bookAuthorses = db.BookAuthors.Where(a => a.BookId == bookId).ToList();
                

                ViewBag.BookId = book.BookId;
                int bookID = book.BookId;
                BookAuthors bookAuthor = new BookAuthors();
                bookAuthor.BookId = bookID;
                bookAuthor.AuthorId = authorId;
                // book.Authors.Where(a=>a.Id==bookAuthor.AuthorId).Count()
                if (book.Authors == null)
                {
                  //  Author author = db.Authors.Where(a => a.Id == bookAuthor.AuthorId).Single();
                    //int authorIdTest = db.Authors.Where(a => a.Id == bookAuthor.AuthorId).Single().Id;
                        // book.Authors.Add(author);
                        var testi = db.BookAuthors.Where(a => a.BookId == bookID).ToList();
                        var testi2 = testi.Select(a => a.AuthorId).Contains(bookAuthor.AuthorId);
                   // db.Books.AddOrUpdate(book);
                    if (!testi2)//!db.BookAuthors.Select(a=>a.AuthorId).Contains(bookAuthor.AuthorId)
                    {
                        db.BookAuthors.Add(bookAuthor);
                        
                    }
                 
                  //  db.BookAuthors.Distinct();
                    db.SaveChanges();
                }
              
                db.BookAuthors.Distinct();
                db.SaveChanges();
            }

            var ListOfBookAuthors = db.BookAuthors.Where(a => a.BookId == bookId).ToList();
            List<Author> ListOfAuthors=new List<Author>();
            foreach (var author in ListOfBookAuthors)
            {
                ListOfAuthors.Add(author.Author);
            }

            ViewBag.ListOfAuthors = ListOfAuthors;
          
            return View(authorList);
        }

        public ActionResult RemoveAuthorsFromBook(int bookId, int authorId)
        {
            if (bookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Author> authorList = db.Authors.ToList();
            if (authorId != 0)
            {
                Book book = db.Books.Find(bookId);
                ViewBag.BookId = book.BookId;
                int bookID = book.BookId;
                BookAuthors bookAuthor = db.BookAuthors.Where(a => a.BookId == bookID && a.AuthorId == authorId).Single();

                db.BookAuthors.Remove(bookAuthor);
                db.SaveChanges();
                book.Authors = authorList;
            }

            return View("AddAuthorsToBook", authorList);
        }

        public void CheckLogin()
        {

            if (Session["UserId"] == null)
            {
                Response.Redirect("/home/login", true);
                Response.End();

            }
        }
    }
}
