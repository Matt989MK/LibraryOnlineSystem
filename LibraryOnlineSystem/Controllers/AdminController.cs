using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
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
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return Redirect("/Home/Login");
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

       
        public FileContentResult GetImage(int bookId)
        {
            Book book = db.Books.FirstOrDefault(a => a.BookId == bookId);
            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ActionResult PaymentsAdmin()
        {

          //  List<Booking> bookingList = new List<Booking>();
            List<Payment> paymentList = new List<Payment>();

          //  bookingList = db.Bookings.Where(a => a.DateReturned < DateTime.Now).ToList();
            paymentList = db.Payments.ToList();
            //foreach (var booking in bookingList)
            //{
            //    Payment payment = new Payment();
            //    payment.UserId = booking.userId;
            //    payment.Amount = 2;
            //    payment.DatePaid = new DateTime(0001, 1, 1);
            //    payment.Status = "Unpaid";
            //    payment.Booking = booking;

            //    paymentList.Add(payment);

            //}

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
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateCopy(BookCode bookCode)
        {
            db.BookCodes.Add(bookCode);
            db.SaveChanges();
            return Redirect("/Admin/DisplayCopies/"+bookCode.BookId);
        }
        public ActionResult DisplayCopies(int id)
        {
            List<BookCode> bookCodes= new List<BookCode>();
            bookCodes = db.BookCodes.Where(a => a.BookId == id).ToList();

            return View(bookCodes);
        }
        [HttpGet]
        public ActionResult AddBook()
        {

            return View("AddBook", new Book());
        }
        [HttpPost]
        public ActionResult AddBook(Book book,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                db.Books.Add(book);
                db.SaveChanges();
                TempData["message"] = string.Format("Saved {0}", book.Name);
                return Redirect("/Admin/BookDatabase");
            }
            else
            {
                
                return View(book);
            }
            
           
        }
        [HttpGet]
        public ActionResult Regulations()
        {
            LibraryRegulations libraryRegulations = new LibraryRegulations();

            return View(libraryRegulations);
        }
        [HttpPost]
        public ActionResult Regulations(int fine, int borrowTime)
        {
            LibraryRegulations libraryRegulations = new LibraryRegulations();
            libraryRegulations.BorrowTime = borrowTime;
            libraryRegulations.Fine = fine;
            db.LibraryRegulations.AddOrUpdate(libraryRegulations);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult ReturnBook()
        {
           
            
            return View();
        }

        [HttpPost]
        public ActionResult ReturnBook(string bookSerialNumber)
        {
            LibraryRegulations libraryRegulations= new LibraryRegulations();
            List<BookCode> bookCode = db.BookCodes.ToList();
            List<Booking> bookings = db.Bookings.ToList();
            BookCode bookCode1 = bookCode.Where(a => a.BookSerialNumber == bookSerialNumber).Single();
          
            int bookingId = Int32.Parse(db.Bookings.Where(a => a.BookCodeId == bookCode1.BookCodeId && a.DateReturned==null).Single().BookingId.ToString());
            Booking booking = db.Bookings.Where(a => a.BookingId == bookingId).Single();

            User user = db.Users.Where(a => a.UserId == booking.UserId).Single();
            foreach (var book in bookCode)
            {
                if (book.BookSerialNumber == bookSerialNumber)
                {
                    booking.DateReturned = DateTime.Today;
                    book.IsInLibrary = true;
                    if ((DateTime.Today - booking.DateCreated).TotalDays>libraryRegulations.BorrowTime)
                    {
                        //create a fee for user for being late
                        Payment payment = new Payment()
                        {
                            UserId = user.UserId,
                            Amount = libraryRegulations.Fine,
                            BookingId = booking.BookingId,
                            Status = "Unpaid",
                            DatePaid = null
                        };
                        db.Payments.Add(payment);
                      
                    }
                }
                db.BookCodes.AddOrUpdate(book);
            }

            
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
            book.BookReviews = listOfBookReviews;
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId == bookId).ToList();
            book.BookCode = bookCodesList;


            return View(book);
           
        }
      
        [HttpPost]
        public ActionResult EditBook(Book book, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                db.Books.AddOrUpdate(book);
                db.SaveChanges();
                TempData["message"] = string.Format("Saved {0}", book.Name);
                return Redirect("/Admin/BookDatabase");
            }
            else
            {

                return View(book);
            }
            
          
        }
        [HttpGet]
        public ViewResult EditBook(int bookId)
        {
            Book book = db.Books.FirstOrDefault(a => a.BookId == bookId);
            return View(book);
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
        
        public ActionResult ReportBook()
        {
            DateTime dateBeginning= Request["beginningDate"].AsDateTime();
            DateTime dateEnding= Request["endingDate"].AsDateTime();
            
            if (dateEnding == DateTime.MinValue) { dateEnding = DateTime.Today; }

            // List<int> bookings=db.Bookings.Select(a=>a.BookId).Distinct().ToList();
            List<BookCode> bookCodes = db.BookCodes.ToList();
            int maxBookId=0;
            int bookCodeCount = db.Bookings.Where(a => a.DateCreated >dateBeginning  && a.DateCreated < dateEnding).GroupBy(a=>a.BookId).Select(a=>a.Count()).Max();// most popular book

           
            List<int> newBookings=db.Bookings.Where(a=>a.DateCreated> dateBeginning && a.DateCreated< dateEnding).Select(a=>a.BookId).Distinct().ToList();
            List<Book> books=db.Books.ToList();
          List<string> genre= new List<string>();
            foreach (var book in books)
            {
                genre.Add(db.Books.Where(a => a.BookId == book.BookId).FirstOrDefault().Genre.ToString());
            }



            foreach (int bookId in newBookings)
            {
                int countOfBook = db.Bookings.Where(a => a.BookId == bookId).Count();
                if (countOfBook == bookCodeCount)
                {
                    maxBookId = db.Bookings.Where(a => a.BookId == bookId).FirstOrDefault().BookId;
                }
            }



            var genreGroup = genre.GroupBy(x => x);
            var maxCount = genreGroup.Max(g => g.Count());
            var mostCommons = genreGroup.Where(x => x.Count() == maxCount).Select(x => x.Key).Single();
            ViewBag.MostCommonCategory = mostCommons;
            ViewBag.MostPopularBookName = books.Where(a=>a.BookId==maxBookId).FirstOrDefault().Name;
            ViewBag.MaxBooking = bookCodeCount;
            ViewBag.BookCount = bookCodes.Count;
            return View(bookCodes);
        }
        public ActionResult ReportUser()
        {
            DateTime dateBeginning = Request["beginningDate"].AsDateTime();
            DateTime dateEnding = Request["endingDate"].AsDateTime();
            if (dateEnding == DateTime.MinValue) { dateEnding = DateTime.Today; }
            List<User> users = db.Users.ToList();
            List<Comment> comments = db.Comment.ToList();
            ViewBag.UserCount = users.Count();
            ViewBag.CommentCount = comments.Count();
            //Add to User when they joined?


            return View();
        }
        public ActionResult ReportBooking()
        {
           List<Booking> bookings = db.Bookings.ToList();
            return View();
        }
        public ActionResult ReportPayment()
        {
            List<Payment> payments = db.Payments.ToList();
            int lateCounter = 0;
            int totalPayments = 0;
            int missedPayments = 0;
            int outstandingPayments=0;
            int allPayments = 0;
            List<Booking> booking = db.Bookings.ToList();
            foreach (var payment in payments)
            {

                allPayments++;
                if (payment.Status == "Paid") { totalPayments += payment.Amount; }
                else if (payment.Status == "Unpaid"){missedPayments++;outstandingPayments += payment.Amount; lateCounter++;}
            }

            ViewBag.totalPayment = totalPayments;
            ViewBag.lateCounter = lateCounter;
            ViewBag.missedPayments = missedPayments;
            ViewBag.outstandingPayments = outstandingPayments;
            ViewBag.allPayments = allPayments;
            return View();
        }
        public ActionResult ReportStock()
        {
            List<Stock> stocks = db.Stocks.ToList();
            return View();
        }
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
