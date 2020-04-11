using LibraryOnlineSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
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
            List<Booking> bookingList = db.Bookings.Where(a => a.User.UserId == userId).ToList();
            List<Payment> paymentList=new List<Payment>();
            foreach (Booking booking in bookingList)
            {
                paymentList = db.Payments.Where(a => a.BookingId == booking.BookingId).ToList();
                Book book= db.Books.Where(a => a.BookId == booking.BookId).Single();
                booking.User = db.Users.Where(a => a.UserId == userId).Single();
                booking.Book = book;
            }

            user.ListOfPayment = paymentList;
            user.Bookings = bookingList;
            return View(bookingList);
        }
        public ActionResult Account(int userId)
        {

            User user = db.Users.Where(a => a.UserId == userId).Single();
            List<Booking> bookingList = db.Bookings.Where(a => a.User.UserId==userId).ToList();
            user.Bookings = bookingList;
            return View(user);
        }
       
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }



        [HttpGet]
        public ActionResult PayFine(int userId)
        {
            Payment payment = db.Payments.Where(a => a.UserId == userId).FirstOrDefault();
            Booking booking = db.Bookings.Where(a => a.BookingId == payment.BookingId).Single();
            Book book = db.Books.Where(a => a.BookId == booking.BookId).Single();
            

            return View(payment);
        }

        [HttpPost]
        public ActionResult PayFine(int userId,int paymentId)
        {
            Payment payment = db.Payments.Where(a => a.PaymentId == paymentId).FirstOrDefault();
            payment.DatePaid = DateTime.Now;
            payment.Status = "Paid";
            db.Payments.AddOrUpdate(payment);
            db.SaveChanges();
            return Redirect("Index");
        }
        public ActionResult Payments(int userId)
        {

            List<Booking> bookingList = db.Bookings.Where(a => a.User.UserId == userId && a.DateReturned < DateTime.Now).ToList();
            List<Payment> paymentList=db.Payments.Where(a=>a.UserId==userId).ToList();
            List<Booking> bookingListDisplay = db.Bookings.Where(a => a.User.UserId == userId).ToList();

            List<string> bookNames = new List<string>();
            List<DateTime?> datesReturned = new List<DateTime?>();
            foreach (var booking in bookingListDisplay)
            {
                Book book = db.Books.Where(a => a.BookId == booking.BookId).Single();

                //booking.User = db.Users.Where(a => a.UserId == userId).Single();
                //booking.Book = db.Books.Where(a => a.BookId == booking.BookCodeId).Single();
                //Payment payment = new Payment();
                //payment.UserId = userId;
                //payment.Amount = 2;
                //payment.DatePaid = null;
                //payment.Status = "Unpaid";
                //payment.PaymentId = 4;
                //payment.BookingId = booking.BookingId;
                bookNames.Add(book.Name);
                //if (booking.DateReturned == null)
                //{
                    booking.DateReturned = DateTime.MinValue;
                //}
                //else
                //{
                    datesReturned.Add(booking.DateReturned);
                //}
                
               // paymentList.Add(payment);

            }

            ViewBag.BookNames = bookNames;
            ViewBag.datesReturned = datesReturned;
            return View(paymentList);
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