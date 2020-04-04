using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using LibraryOnlineSystem;
using LibraryOnlineSystem.Models;

namespace LibraryOnlineSystem.Controllers
{
    public class AdminController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult DetailsUser(int userId)
        {
            User user=new User();
            user=db.Users.Where(a => a.UserId == userId).Single();
            return View(user);
        }

        public ActionResult Stock()
        {
            List<Book> bookList = db.Books.ToList();
            return View(bookList);
        }

        public ActionResult StockDetails()
        {
            return View();
        }

        public ActionResult StockReport()
        {
            return View();
        }
        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult PaymentsAdmin()
        {

            List<Booking> bookingList = new List<Booking>();
            List<Payment> paymentList = new List<Payment>();

            bookingList = db.Bookings.Where(a => a.DateReturned < DateTime.Now).ToList();
            foreach (var booking in bookingList)
            {
                Payment payment = new Payment();
                payment.UserId = booking.userId;
                payment.Amount = 2;
                payment.DatePaid = new DateTime(0001, 1, 1);
                payment.Status = "Unpaid";
                payment.Booking = booking;

                paymentList.Add(payment);

            }

            return View(paymentList);
        }//------------------ADMIN
        public ActionResult BookDatabase()
        {
            List<Book> listOfBooks = new List<Book>();
            listOfBooks = db.Books.ToList();

            return View(listOfBooks);
        }

        public ActionResult OrderBooks()
        {

            return View();
        }

        public ActionResult UserReport()
        {
            return View();
        }
        public ActionResult Stocks()
        {
            List<Book> listOfBooks = new List<Book>();
            listOfBooks = db.Books.ToList();
            List<BookCode> bookCodesList = db.BookCodes.ToList();
            foreach (var book in listOfBooks)
            {
                book.BookCode = bookCodesList.Where(a => a.BookId == bookCodesList[book.BookId].BookId).ToList();
            }
            return View(listOfBooks);
        }

        [HttpGet]
        public ActionResult CreateCopy(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCopy(BookCode bookCode)
        {
            db.BookCodes.Add(bookCode);
            db.SaveChanges();
            return Redirect("/Admin/DisplayCopies");
        }
        public ActionResult DisplayCopies(int id)
        {
            List<BookCode> bookCodes= new List<BookCode>();
            bookCodes = db.BookCodes.Where(a => a.BookId == id).ToList();

            return View(bookCodes);
        }
        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return Redirect("/Admin/BookDatabase");
        }

        [HttpGet]
        public ActionResult DetailsBook(int bookId)
        {
            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == bookId).ToList();
            Book book = listOfBook.Where(a => a.BookId == bookId).Single();
            book.bookReviews = listOfBookReviews;
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId == bookId).ToList();
            book.BookCode = bookCodesList;


            return View(book);
           
        }
        [HttpGet]
        public ActionResult EditBook(int bookId)
        {
            Book book = db.Books.Where(a => a.BookId == bookId).Single();

            return View(book);
        }
        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Books.AddOrUpdate(book);
            db.SaveChanges();
            return Redirect("/Admin/BookDatabase");
        }
        [HttpGet]
        public ActionResult AddBook()
        {

            return View();
        }

        [HttpGet]
        public ActionResult DeleteBook(int? id)
        {

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
           return View(book);
        }
        [HttpPost]
        public ActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return Redirect("/Admin/BookDatabase");
        }
        [HttpPost]
        public ActionResult EditUser(int userId, User user)
        {

            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return View(user);
        }
        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,Surname,DateOfBirth,Email,HouseNo,ZipCode,UserRole,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Edit/5
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditUser([Bind(Include = "UserId,Name,Surname,DateOfBirth,Email,HouseNo,ZipCode,UserRole,Password")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

        // GET: Admin/Delete/5
        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Redirect("/Admin/UsersAdmin");
        }

        //---------Users

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            user=new User();
            user.Name =Request["Name"] ;
            user.Surname = Request["SurName"];
            user.Email = Request["Email"];
            user.Password = Request["Password"];
            user.HouseNo = int.Parse(Request["HouseNo"]);
            user.DateOfBirth = Request["DateOfBirth"].AsDateTime();
            user.ZipCode = int.Parse(Request["ZipCode"]);
            user.UserRole = Request["UserRole"];
            db.Users.Add(user);
            db.SaveChanges();
           // return RedirectToAction("Index","User");
           return View("AddedUser");
        }

        public ActionResult UsersAdmin()
        {
            List<User> userList = db.Users.ToList();
            return View(userList);
        }

        public ActionResult Account(int userId)
        {

            User user = db.Users.Where(a => a.UserId == userId).Single();
            List<Booking> bookingList = db.Bookings.Where(a => a.User.UserId == userId).ToList();
            user.Bookings = bookingList;
            return View(user);
        }
        public ActionResult ArchiveUser()
        {
            return View();
        }

        public ActionResult DisplayUsers()
        {
            return View();
        }

        public ActionResult DisplaySelectedUser()
        {
            return View();
        }
        //--------

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
