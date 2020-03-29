using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        public ActionResult ReservedBook(int bookId, int userId)
        {
            Book book= new Book();
            User user=new User();
            book = db.Books.Where(a => a.BookId == bookId).Single();
            user = db.Users.Where(a => a.UserId == userId).Single();
            BookReserve bookReserve= new BookReserve();
            bookReserve.User = user;
            bookReserve.Book = book;
            bookReserve.ReservationRequestTime=DateTime.Today;
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



            return View(book);
        }


        [HttpPost]
        public ActionResult Details()
        {
            int id = Convert.ToInt32(Request.Params["BookID"]);
            Book book = db.Books.Find(id);//
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
        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BorrowBook(int userId, int bookId)
        {
            Book book = new Book();
            book = db.Books.Where(a => a.BookId == bookId).Single();
            User user = new User();
            user = db.Users.Where(a => a.UserId == userId).Single();
            if (book.Quantity > 0)
            {


                Booking booking = new Booking();
               
                booking.Book = book;
                booking.User = user;
                booking.DateCreated = DateTime.Now;
                booking.DateDue = DateTime.Now.AddDays(7);
                db.Bookings.Add(booking);
                db.SaveChanges();
                return View(booking);
            }

            return View("book is not in stock");
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
