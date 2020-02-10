using LibraryOnlineSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnlineSystem.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db=new LibraryContext();
        
        public void CheckLogin()
        {
         
            if (Session["UserId"] == null)
            {
                Response.Redirect("/home/login", true);
                Response.End();

            }
           
        }
        public ActionResult Index()
        {
          //  Session["isAdmin"] = "3";
            //Session["UserName"] = "test";
            CheckLogin();
            return View();
        }
        //---------------Book
        [HttpGet]
        public ActionResult AddBook()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return Redirect("/Home/BookDatabase");
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
            return Redirect("/Home/BookDatabase");
        }
        [HttpGet]
        public ActionResult ReviewBook( int bookId,int userId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReviewBook(BookReview bookReview) //TT
        {
            bookReview=new BookReview();
            bookReview.UserId = 1;
            bookReview.BookId = 1;
            bookReview.BookReviewId = 1;
            bookReview.Content = "testReview";
            bookReview.DatePosted=DateTime.Now;
            return Redirect("/Home/Books");
        }
        //------------------------ Loan History
       

        public ActionResult LoanDetails()
        {
            return View();
        }
        //------------------------
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoanHistory(int userId)
        {
            User user = db.Users.Where(a => a.UserId == userId).Single();
            List<Booking> bookingList = db.Bookings.Where(a => a.UserId == userId).ToList();
            user.Bookings = bookingList;
            return View(bookingList);
        }
        public ActionResult Account(int userId)
        {

            User user = db.Users.Where(a => a.UserId == userId).Single();
            List<Booking> bookingList = db.Bookings.Where(a => a.UserId==userId).ToList();
            user.Bookings = bookingList;
            return View(user);
        }
        [HttpGet]
        public ActionResult EditUser(int userId)
        {
            User user = db.Users.Where(a => a.UserId == userId).Single();

            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(int userId,User user)
        {
          
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return Redirect("Account/?userId="+userId);
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult UsersAdmin()
        {
            List<User> userList = db.Users.ToList();
            return View(userList);
        }
        public ActionResult PaymentsAdmin()
        {

             List<Booking> bookingList = new List<Booking>();
            List<Payment> paymentList = new List<Payment>();
            
            bookingList = db.Bookings.Where(a =>a.DateDue < DateTime.Now).ToList();
            foreach (var booking in bookingList)
            {
                Payment payment = new Payment();
                payment.UserId = booking.UserId;
                payment.Amount = 2;
                payment.DatePaid = new DateTime(0001, 1, 1);
                payment.Status = "Unpaid";
                payment.Booking = booking;

                paymentList.Add(payment);

            }

            return View(paymentList);
        }


        public ActionResult Payments(int userId)
        {
            List<Booking> bookingList = new List<Booking>();
            List<Payment> paymentList=new List<Payment>();
            bookingList = db.Bookings.Where(a => a.UserId == userId && a.DateDue < DateTime.Now).ToList();
            foreach (var booking in bookingList)
            {
                Payment payment = new Payment();
                payment.UserId = userId;
                payment.Amount = 2;
                payment.DatePaid = new DateTime(0001, 1, 1);
                payment.Status = "Unpaid";
                payment.Booking = booking;
            
                paymentList.Add(payment);

            }

            return View(paymentList);
        }
        //------------------ADMIN
        public ActionResult BookDatabase()
        {
            List<Book> listOfBooks=new List<Book>();
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
            return View(listOfBooks);
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var context = new LibraryContext())
            {
               //int x = context.Users.Count();
                string email = Request["Email"];
                string password = Request["Password"];
                User user1=new User();
                if (context.Users.Where(a => a.Email == email).Count() >0)
                {
                     user1 = context.Users.Where(a => a.Email == email).Single();

                }
                else
                {
                    //textbox display "user does not exist"
                }


                int x = context.Users.Where(a=>a.Email==email ).Count();//&& a.Password==Request["Password"]
                if (user1.UserRole == "Admin")
                {
                    Session["isAdmin"] = "1";
                    Session["UserId"] = user1.UserId.ToString();
                    Session["UserName"] ="Admin "+ user.Name;
                }
                else
                {
                    Session["isAdmin"] = "0";
                    Session["UserName"] = "User "+user.Name;
                    Session["UserId"] = user1.UserId.ToString();
                }
                // user = context.Users.Where(a => a.UserId).Where((a => a.Email == email));
                user = context.Users.Find(1);
                // context.SaveChanges();
                if(x ==0)
                {
                    Response.Redirect("/home/login", true);
                    Response.End();
                }
             
            }
            return Redirect("/Home/Index");
        }
    }
}