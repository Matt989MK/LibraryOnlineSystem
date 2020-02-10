using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryOnlineSystem.Models;

namespace LibraryOnlineSystem.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext db=new LibraryContext();
        // GET: Book
        public ActionResult Index()
        {
            CheckLogin();

            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            //List<Author> listOfAuthor=new List<Author>();




            //listOfBook.Add(book);
            return View(listOfBook);
        }

       

        public ActionResult RequestBook(int id)
        {

            return View(id);
        }
        // GET: Book/Details/5
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
       
        public ActionResult BorrowBook(int userId,int bookId)
        {
            Book book=new Book();
            book.BookId = bookId;
            if (book.Quantity>0)
            {


                Booking boooking = new Booking();

                boooking.BookId = bookId;
                boooking.UserId = userId;
                boooking.DateCreated = DateTime.Now;
                boooking.DateDue = DateTime.Now.AddDays(7);

                return View(boooking);
            }

            return View("book is not in stock");
        }
     

        public ActionResult ReserveBook(int userId,int bookId)
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
