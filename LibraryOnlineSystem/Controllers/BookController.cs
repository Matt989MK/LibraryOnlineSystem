using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryOnlineSystem.Models;

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


            if (Genre != null && Genre != "Any")
            {
                lstBooks = lstBooks.Where(i => i.Genre.ToString() == Genre).ToList();
                //if (Rating == "Worst") { lstBooks = lstBooks.OrderBy(i => i.Rating).ToList(); }
                //else if (Rating == "Best") { lstBooks = lstBooks.OrderByDescending(i => i.Rating).ToList(); }
            }

            //else
            //{ lstBooks = db.Books.Where(i => i.Genre.ToString() == Genre).ToList(); }
            //if (Rating == "Worst") { lstBooks = lstBooks.OrderBy(i => i.Rating).ToList(); }
            //else if (Rating == "Best") { lstBooks = lstBooks.OrderByDescending(i => i.Rating).ToList(); }

            return View(lstBooks);
        }


        public ActionResult ReservedBook(int bookCodeId, int userId)
        {
           BookReserve bookReserve= new BookReserve();
           bookReserve.BookCodeId = bookCodeId;
           bookReserve.ReservationRequestTime = DateTime.Today;
           bookReserve.UserId = userId;
           

            return View(bookReserve);
        }


        [HttpGet]
        public ActionResult CommentReply(int id)
        {
            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            Book book = listOfBook.Where(a => a.BookId == id).Single();
            book.bookReviews = listOfBookReviews;



            return View(book);
        }


        [HttpPost]
        public ActionResult CommentReply()
        {
            int id = Convert.ToInt32(Request.Params["BookID"]);
            int commentID = Convert.ToInt32(Request.Params["CommentID"]);
            Book book = db.Books.Find(id);//
            CommentReply commentReply = new CommentReply();
            commentReply.Content = Request.Params["NewReply"];
            commentReply.CommentID = commentID;
            commentReply.AuthorID = User.Identity.Name;
            commentReply.PostID = 1;
            commentReply.BookID = id;
            commentReply.PersonID = 1;
            float.TryParse(Request.Params["NewUserRating"], out float results);

            db.CommentReply.Add(commentReply);
            db.SaveChanges();



            List<Comment> lstComment = db.Comment.Where(c => c.BookID == id).ToList();
            foreach (Comment item in lstComment)
            {

                List<CommentReply> lstCommentReply = new List<CommentReply>();
                if (db.CommentReply.Where(c => c.CommentID == item.CommentID).ToList() != null)//
                {
                    lstCommentReply = db.CommentReply.Where(c => c.CommentID == item.CommentID).ToList();
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            book.bookReviews = listOfBookReviews;

            return RedirectToAction("Details/" + id);


        }

        // GET: Book/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            //DAOBook daoBook=new DAOBook();
            //Book book = daoBook.getSelectedBook(id);

            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            Book book = listOfBook.Where(a => a.BookId == id).Single();
            book.bookReviews = listOfBookReviews;
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId == id).ToList();
            book.BookCode = bookCodesList;


            return View(book);
        }


        [HttpPost]
        public ActionResult Details()
        {
            int id = Convert.ToInt32(Request.Params["BookID"]);
            Book book = db.Books.Find(id);//
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId==id).ToList();
            book.BookCode = bookCodesList;
            Comment comment = new Comment();
            comment.Content = Request.Params["NewComment"];
            comment.AuthorID = User.Identity.Name;
            comment.PostID = 1;
            comment.BookID = id;
            comment.PersonID = 1;
            float.TryParse(Request.Params["NewUserRating"], out float results);
            comment.UserRating = results;
            if (comment.UserRating <= 10 && comment.UserRating >= 0.0)
            {
                db.Comment.Add(comment);
                db.SaveChanges();

            }

            List<Comment> lstComment = db.Comment.Where(c => c.BookID == id).ToList();
            foreach (Comment item in lstComment)
            {
                CommentReply commentReply = new CommentReply();
                List<CommentReply> lstCommentReply = new List<CommentReply>();
                if (db.CommentReply.Where(c => c.CommentID == item.CommentID).ToList() != null)//
                {
                    lstCommentReply = db.CommentReply.Where(c => c.CommentID == item.CommentID).ToList();
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == id).ToList();
            book.bookReviews = listOfBookReviews;

            return View(book);

        }
  

      

  

        public ActionResult BorrowBook(int userId, int bookId)
        {
            Book book = new Book();
            book = db.Books.Where(a => a.BookId == bookId).Single();
            User user = new User();
            user = db.Users.Where(a => a.UserId == userId).Single();
            


                Booking booking = new Booking();
               BookCode bookCode=new BookCode();
               bookCode = db.BookCodes.Where(a => a.BookId ==bookId && a.IsInLibrary==true).First();
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
