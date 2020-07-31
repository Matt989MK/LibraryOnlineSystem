using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.WebPages;
using Antlr.Runtime.Misc;
using LibraryOnlineSystem;
using LibraryOnlineSystem.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using BarcodeLib;
using Microsoft.Ajax.Utilities;
using Action = System.Action;
using OnBarcode.Barcode;

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
            BookCode bookCode = new BookCode();
            user =db.Users.Where(a => a.UserId == userId).Single();
            List<BookReserve> bookReserves = db.BookReserves.Where(a => a.UserId == userId).ToList();
            List<Booking> bookingList = db.Bookings.Where(a => a.UserId == userId).ToList();
           // List<PaymentLibrary> paymentLibrary = db.Payments.Where(a => a.UserId == userId).ToList();

            string bookName;
            List<string> bookNameList=new List<string>();
            foreach (var booking in bookingList)
            {
                bookCode = db.BookCodes.Where(a => a.BookCodeId == booking.BookCodeId).Single();
                bookName = db.Books.Where(a => a.BookId == bookCode.BookId).Single().Name;
                bookNameList.Add(bookName);
               
            }

            ViewBag.BookNames = bookNameList;
            user.ListOfReserves = bookReserves;
            user.ListOfReserves = db.BookReserves.Where(a => a.UserId == userId).ToList();
            user.Bookings = bookingList;
            user.ListOfPayment = db.Payments.Where(a => a.UserId == userId).ToList();
            return View(user);
        }

        public string GetSerialNumber(int id)
        {
            List<BookCode> bookCode = db.BookCodes.ToList();
            var test = bookCode.Where(a => a.BookCodeId == id).Single();
           // ViewBag.SerialNumber = test.BookSerialNumber;
            return test.BookSerialNumber;
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

        [HttpGet]
        public ActionResult News()
        {

            return View();
        }

        [HttpPost]
        public ActionResult News(int newsId, News news)
        {
            return View(news);
        }
        public ActionResult PaymentsAdmin()
        {

          //  List<Booking> bookingList = new List<Booking>();
            List<PaymentLibrary> paymentList = new List<PaymentLibrary>();

          //  bookingList = db.Bookings.Where(a => a.DateReturned < DateTime.Now).ToList();
            paymentList = db.Payments.ToList();
         

            return View(paymentList);
        }//------------------ADMIN

        public ActionResult BookDatabase(string Name, string Genre, string Rating)
        {
           
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
                    book.Rating = db.Comment.Where(a => a.BookId == book.BookId).Select(a => a.UserRating).Average();
                  book.Rating= (float)Math.Round(book.Rating,2);
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

            return View(lstBooks);
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
            ViewBag.TotalCountBook = new List<int>();
            ViewBag.BookCurrentlyInStock = new List<int>();
            foreach (var book in listOfBooks)
            {
                
                
                book.BookCode = bookCodesList.Where(a => a.BookId == book.BookId&&a.IsInLibrary==true).ToList();
                ViewBag.TotalCountBook.Add(bookCodesList.Where(a => a.BookId == book.BookId).Count());
                ViewBag.BookCurrentlyInStock.Add(bookCodesList.Where(a => a.BookId == book.BookId && a.IsInLibrary == true).Count());
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
            
                if (!db.BookCodes.Select(a => a.BookSerialNumber).Contains(bookCode.BookSerialNumber))
            {
                db.BookCodes.Add(bookCode);
                db.SaveChanges();
             
            }



                PrintLabel(bookCode.BookSerialNumber);

            return Redirect("/Admin/DisplayCopies/"+bookCode.BookId);
        }



   



        public ActionResult PrintLabel(string serialNumber)
        {
            // Create linear barcode object
            Linear barcode = new Linear();
            // Set barcode symbology type to Code-39
            barcode.Type = BarcodeType.CODE39;
            // Set barcode data to encode
            barcode.Data = serialNumber;
            // Set barcode bar width (X dimension) in pixel
            barcode.X = 1;
            // Set barcode bar height (Y dimension) in pixel
            barcode.Y = 60;
            // Draw & print generated barcode to png image file
            var path = Path.Combine(Server.MapPath("~/Labels/"), "label" + serialNumber);

            barcode.drawBarcode(path+".jpeg");//"~\\Labels\\" + "label" + serialNumber+".jpeg"
            //"D://csharp-code39.png"
            ViewBag.SerialNumber = serialNumber;                                                            // barcode.Print();
            return View();
            }















        public ActionResult DisplayCopies(int id)
        {
            List<BookCode> bookCodes= new List<BookCode>();
            bookCodes = db.BookCodes.Where(a => a.BookId == id).ToList();

            return View(bookCodes);
        }
        // get all the books

     
        //display both on one page
        [HttpGet]
        public ActionResult AddBook()
        {
         //   dynamic BooksAuthors=new ExpandoObject();
         List<Author> authors = db.Authors.ToList();

         ViewBag.Authors = authors;

           // BooksAuthors.Book = GetBook();
           // BooksAuthors.Authors = GetAuthors();



            List<string> listOfUnit = new List<string>();
            foreach (Genre genre in (Genre[]) Enum.GetValues(typeof(Genre)))
            {
                listOfUnit.Add(genre.ToString());
            }
            ViewBag.DictionaryPackages = listOfUnit;

            return View("AddBook", new Book());//"AddBook", new Book()
        }
        [HttpPost]
        public ActionResult AddBook(Book book,HttpPostedFileBase image)
        {
            List<string> listOfUnit = new List<string>();
            foreach (Genre genre in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                listOfUnit.Add(genre.ToString());
            }
            ViewBag.DictionaryPackages = listOfUnit;

            //book = new Book();
            //book.Name = Request["Name"];
            //book.Genre = (Genre)Enum.Parse(typeof(Genre), Request["Genre"]);
            //book.DateOfPublication = Request["DateOfPublication"].AsDateTime();
            //book.Overview = Request["Overview"];
            //book.Publisher = Request["Publisher"];
           
           
               
             
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
                int lastBook = db.Books.Select(a=>a.BookId).Max();
                return Redirect("/Book/AddAuthorsToBook?bookId=" + lastBook + "&"+ "authorId=0");
            }
            else
            {
                
                return View();//"Error"
            }
            
           
        }
        [HttpPost]
        public JsonResult isUserExists(string email)
        {

            // db.Configuration.ValidateOnSaveEnabled = false;

            bool isExist = db.Users.Where(a => a.Email == email).Count() > 0;
            //      db.Configuration.ValidateOnSaveEnabled = true;

            return Json(!isExist, JsonRequestBehavior.AllowGet);
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
            News news = new News();
            LibraryRegulations libraryRegulations = new LibraryRegulations();
            if (libraryRegulations.BorrowTime != borrowTime)
            {
                news.NewsContent = "You can borrow your book up to: " + borrowTime.ToString() + " days now." + Environment.NewLine;

            }

            if (libraryRegulations.Fine != fine)
            {
                news.NewsContent += "Fine for not returning a book on time is now : £" + fine.ToString();

            }
            libraryRegulations.BorrowTime = borrowTime;
            libraryRegulations.Fine = fine;

            db.LibraryRegulations.AddOrUpdate(libraryRegulations);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult ReturnBook(int bookCodeId, string bookSerialNumber)
        {
           // string sn = RouteData.Values["SerialNumber"] + Request.Url.Query;
           BookCode bookCode=db.BookCodes.Where(a=>a.BookCodeId==bookCodeId).Single();
            return View(bookCode);
        }

        [HttpPost]
        public ActionResult ReturnBook(string bookSerialNumber)
        {
        
        //    bookSerialNumber = RouteData.Values["SerialNumber"] + Request.Url.Query;
            LibraryRegulations libraryRegulations= new LibraryRegulations();
            List<BookCode> bookCode = db.BookCodes.ToList();
            List<Booking> bookings = db.Bookings.ToList();
            BookCode bookCode1 = bookCode.Where(a => a.BookSerialNumber == bookSerialNumber).Single();
            if (db.Bookings.Where(a => a.BookCodeId == bookCode1.BookCodeId && a.DateReturned == null).Count() > 0)
            {
                int bookingId = Int32.Parse(db.Bookings.Where(a => a.BookCodeId == bookCode1.BookCodeId && a.DateReturned == null).Single().BookingId.ToString());
                Booking booking = db.Bookings.Where(a => a.BookingId == bookingId).Single();

                User user = db.Users.Where(a => a.UserId == booking.UserId).Single();
                foreach (var book in bookCode)
                {
                    if (book.BookSerialNumber == bookSerialNumber)
                    {
                        booking.DateReturned = DateTime.Today;
                        book.IsInLibrary = true;
                        if ((DateTime.Today - booking.DateCreated).TotalDays > libraryRegulations.BorrowTime)
                        {
                            //create a fee for user for being late
                            PaymentLibrary payment = new PaymentLibrary()
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
            else
            {
                return View("Error");
            }
          
        }
        [HttpGet]
        public ActionResult DetailsBook(int bookId)
        {
            List<Book> listOfBook = new List<Book>();
            listOfBook = db.Books.ToList();
            List<BookReview> listOfBookReviews = db.BookReviews.Where(a => a.BookId == bookId).ToList();
            Book book = listOfBook.Where(a => a.BookId == bookId).Single();
            book.BookReviews = listOfBookReviews;
            
            List<BookCode> bookCodesList = db.BookCodes.Where(a => a.BookId == bookId && a.IsInLibrary == true).ToList();
            book.BookCode = bookCodesList;
            int bookCurrentlyStocked = bookCodesList.Count;
            List<BookAuthors> bookAuthors = db.BookAuthors.Where(a => a.BookId == bookId).ToList();
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


            ViewBag.bookInStock = bookCurrentlyStocked;
        

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
        public ActionResult DeleteSerialNumber(string SerialNumber)
        {
            BookCode bookCode = db.BookCodes.Where(a => a.BookSerialNumber == SerialNumber).Single();
            if (bookCode.IsInLibrary == true)
            {
                if (ModelState.IsValid)
                {
                    db.BookCodes.Remove(bookCode);
                    db.SaveChanges();
                }
            }
            else
            {
                return View("Error");
            }
           
            
            return Redirect("Stocks?userId"+Session["UserId"]);
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
           // var hash = SecurePasswordHasher.Hash(Request["Password"]);

            
            user.Name = Request["Name"];
            user.Surname = Request["SurName"];
            user.Email = Request["Email"];
            //user.Password = hash;
            user.HouseNo = Request["HouseNo"];
            user.DateOfBirth = Request["DateOfBirth"].AsDateTime();
            user.ZipCode = Request["ZipCode"];
            user.UserRole = Request["UserRole"];
            user.Password = db.Users.Where(a => a.Email == user.Email).Single().Password;
            if (ModelState.IsValid)
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
                return Redirect("/Admin/UsersAdmin");

            }
            else
            {
                return View();
            }
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

            List<string> listOfUnit = new List<string>();
            listOfUnit.Add("Admin");
            listOfUnit.Add("User");

            ViewBag.DictionaryPackages = listOfUnit; 
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
            List<string> listOfUnit = new List<string>();
            listOfUnit.Add("Admin");
            listOfUnit.Add("User");

            ViewBag.DictionaryPackages = listOfUnit;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            var hash = SecurePasswordHasher.Hash(Request["Password"]);

            user = new User();
            user.Name =Request["Name"] ;
            user.Surname = Request["SurName"];
            user.Email = Request["Email"];
            user.Password = hash;
            user.HouseNo = Request["HouseNo"];
            user.DateOfBirth = Request["DateOfBirth"].AsDateTime();
            user.ZipCode = Request["ZipCode"];
            user.UserRole = Request["UserRole"];
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

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

            int bookCodeCount = 0;
            List<BookCode> bookCodes = db.BookCodes.ToList();
            int maxBookId=0;
            if (db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).GroupBy(a => a.BookId).Select(a => a.Count()).Count() > 0)
            {
                 bookCodeCount = db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).GroupBy(a => a.BookId).Select(a => a.Count()).Max();// most popular book

            }

            List<int> newBookingsTest=db.Bookings.Where(a=>a.DateCreated> dateBeginning && a.DateCreated< dateEnding).Select(a=>a.BookId).ToList();
            List<int> newBookingsTestAll = db.Bookings.Select(a => a.BookId).ToList();

            List<int> newBookings=db.Bookings.Where(a=>a.DateCreated> dateBeginning && a.DateCreated< dateEnding).Select(a=>a.BookId).Distinct().ToList();
            ViewBag.BookBorrowCount = newBookingsTest.Count;
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
                    maxBookId = db.Bookings.Where(a => a.BookId == bookId).First().BookId;
                }
            }
            
            var genreGroup = genre.GroupBy(x => x);
            var maxCount = genreGroup.Max(g => g.Count());
            var mostCommons = genreGroup.Where(x => x.Count() == maxCount).Select(x => x.Key).Single();
            ViewBag.MostCommonCategory = mostCommons;
            if (books.Where(a => a.BookId == maxBookId).Count()>0)
            {
                ViewBag.MostPopularBookName = books.Where(a => a.BookId == maxBookId).First().Name;
                }
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

            DateTime dateBeginning = Request["beginningDate"].AsDateTime();
            DateTime dateEnding = Request["endingDate"].AsDateTime();

            if (dateEnding == DateTime.MinValue) { dateEnding = DateTime.Today; }

           // db.Bookings.Where(a => a.DateCreated > dateBeginning && a.DateCreated < dateEnding).Select(a => a.BookId).Distinct().ToList();
            List<PaymentLibrary> payments = db.Payments.Where(a=>a.DatePaid>dateBeginning && a.DatePaid<dateEnding).ToList();
            List<PaymentLibrary> totalPaymentsList = db.Payments.ToList();
            int lateCounter = 0;
            int totalPayments = 0;
            int missedPayments = 0;
            int outstandingPayments=0;
            int allPayments = 0;
            List<Booking> booking = db.Bookings.ToList();
            foreach (var payment in totalPaymentsList)
            {

                allPayments++;
                 if (payment.Status == "Unpaid"){missedPayments++;outstandingPayments += payment.Amount; lateCounter++;}
            }

            allPayments = 0;
            foreach (var payment in payments)
            {

                allPayments++;
                if (payment.Status == "Paid") { totalPayments += payment.Amount; }
                
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
       

        [HttpGet]
        public ActionResult FinalizeReservation(int reservationId)
        {
           
           
            BookReserve bookReserve = db.BookReserves.Where(a => a.BookReserveId == reservationId).Single();
            User user = db.Users.Where(a => a.UserId==bookReserve.UserId).Single();
            BookCode bookCode = new BookCode();
            bookCode = db.BookCodes.Where(a => a.BookCodeId == bookReserve.BookCodeId && a.IsInLibrary == true).First();
            bookCode.IsInLibrary = false;
            Booking booking = new Booking();
            booking.BookId = bookCode.BookId;
            booking.User = user;
            booking.DateCreated = DateTime.Now;
            booking.DateReturned = null;
            booking.BookCodeId = bookCode.BookCodeId;
            booking.Book = db.Books.Where(a => a.BookCode.FirstOrDefault().BookCodeId == bookCode.BookCodeId).Single();
            
            user.Bookings.Add(booking);
            if (ModelState.IsValid)
            {
                db.BookReserves.Remove(bookReserve);
                db.Bookings.AddOrUpdate(booking);
                db.BookCodes.AddOrUpdate(bookCode);
                db.SaveChanges();
                return RedirectToAction("ListOfReservations");
            }
            return View("Error");
        }
        public ActionResult ListOfReservations()
        {
            BookCode bookCode=new BookCode();
            string bookName;
            List<BookReserve> listOfBookReserves = db.BookReserves.ToList();
            List<string> listOfSerialNumbers=new List<string>();
            List<bool> listOfIsInLibrary = new List<bool>();
            List<String> listOfNames=new List<string>();
            foreach (var bookReserve in listOfBookReserves)
            {
                bookCode= db.BookCodes.Where(a => a.BookCodeId == bookReserve.BookCodeId).Single();
                bookName = db.Books.Where(a => a.BookId == bookCode.BookId).Single().Name;
                listOfNames.Add(bookName);
                listOfSerialNumbers.Add(bookCode.BookSerialNumber);
                listOfIsInLibrary.Add(bookCode.IsInLibrary);
                
            }
            ViewBag.BookSerialNumber = listOfSerialNumbers;
            ViewBag.ListOfNames = listOfNames;
            ViewBag.IsInLibrary = listOfIsInLibrary;
            return View(listOfBookReserves);
        }

        public ActionResult ListOfLoans()
        {
            BookCode bookCode=new BookCode();
            List<Booking> listOfBookings = db.Bookings.ToList();
            List<string> listOfSerialNumbers = new List<string>();
            List<bool> listOfIsInLibrary = new List<bool>();
            List<String> listOfNames = new List<string>();
            List<String> listOfFirstNames = new List<string>();
            List<String> listOfSurNames = new List<string>();

            string bookName;
            foreach (var booking in listOfBookings)
            {
                bookCode = db.BookCodes.Where(a => a.BookCodeId == booking.BookCodeId).Single();
                bookName = db.Books.Where(a => a.BookId == bookCode.BookId).Single().Name;
                listOfNames.Add(bookName);
                listOfSerialNumbers.Add(bookCode.BookSerialNumber);
                listOfIsInLibrary.Add(bookCode.IsInLibrary);
            }
            ViewBag.BookSerialNumber = listOfSerialNumbers;
            ViewBag.ListOfNames = listOfNames;
            ViewBag.ListOfFirstNames = listOfFirstNames;
            ViewBag.ListOfSurNames = listOfSurNames;
            ViewBag.IsInLibrary = listOfIsInLibrary;
            return View(listOfBookings);
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
