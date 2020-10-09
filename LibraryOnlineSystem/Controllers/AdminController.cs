using LibraryOnlineSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace LibraryOnlineSystem.Controllers
{
    public class AdminController : BaseController
    {
        private readonly LibraryContext db = new LibraryContext();

        // GET: Admin
        public ActionResult Index()
        {
            createCSV();
            GetData();
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
            User user = new User();
            BookCode bookCode = new BookCode();
            user = db.Users.Where(a => a.UserId == userId).Single();
            List<BookReserve> bookReserves = db.BookReserves.Where(a => a.UserId == userId).ToList();
            List<Booking> bookingList = db.Bookings.Where(a => a.UserId == userId).ToList();
            // List<PaymentLibrary> paymentLibrary = db.Payments.Where(a => a.UserId == userId).ToList();

            string bookName;
            List<string> bookNameList = new List<string>();
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


                book.BookCode = bookCodesList.Where(a => a.BookId == book.BookId && a.IsInLibrary == true).ToList();
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

            return Redirect("/Admin/DisplayCopies/" + bookCode.BookId);
        }







        public ActionResult PrintLabel(string serialNumber)
        {


            List<BookCode> bookCodeList = db.BookCodes.ToList();
            // Add exception if doesnt exist
            try
            {
                int bookId = bookCodeList.Where(a => a.BookSerialNumber == serialNumber).Single().BookId;
                string bookName = db.Books.Where(a => a.BookId == bookId).Single().Name;
                ViewBag.SerialNumber = serialNumber;
                //Create an instance of PdfDocument.
                // Printer IP Address and communication port
                string ipAddress = "127.0.0.1";
                int port = 9100;

                // ZPL Command(s)
                string ZPLString =
                    "^XA" +

                    "^FX Top section with logo, name and address." +
                    "^CF0,60" +
                    "^FO50,50^GB100,100,100^FS" +
                    "^FO75,75^FR^GB100,100,100^FS" +
                    "^FO93,93^GB40,40,40^FS" +
                    "^FO220,50^FD Leicester Library^FS" +
                    "^CF0,30" +
                    "^FO220,115^FD54 Gateway Street 50^FS" +
                    "^FO220,155^FDLeicester LE27DP^FS" +
                    "^FO220,195^FDUnited Kingdom (UK)^FS" +
                    "^FO50,250^GB700,1,3^FS" +

                    "^FX Second section with recipient address and permit information." +
                    "^CFA,60" +
                    "^FO50,300^FD" + bookName + "^FS" +


                    "^FO50,500^GB700,1,3^FS" +

                    "^FX Third section with barcode." +
                    "^BY5,2,270" +
                    "^FO100,550^BC^FD" + serialNumber + "^FS" +


                    "^XZ";


                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer =
                    new System.IO.StreamWriter(client.GetStream());
                writer.Write(ZPLString);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
            }

            catch (Exception)
            {
                // Catch Exception
            }

            return View();
            // barcode.Print();

        }




        public ActionResult MarkAsPaid(int userId, int paymentLibraryId)
        {
            PaymentLibrary payment = db.Payments.Where(a => a.UserId == userId && a.PaymentLibraryId == paymentLibraryId).Single();

            payment.Status = "Paid";
            payment.DatePaid = DateTime.Today;
            db.Payments.AddOrUpdate(payment);
            db.SaveChanges();
            return Redirect("PaymentsAdmin");
        }










        public ActionResult DisplayCopies(int id)
        {
            List<BookCode> bookCodes = new List<BookCode>();
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
            foreach (Genre genre in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                listOfUnit.Add(genre.ToString());
            }
            ViewBag.DictionaryPackages = listOfUnit;

            return View("AddBook", new Book());//"AddBook", new Book()
        }
        [HttpPost]
        public ActionResult AddBook(Book book, HttpPostedFileBase image)
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
                int lastBook = db.Books.Select(a => a.BookId).Max();
                return Redirect("/Book/AddAuthorsToBook?bookId=" + lastBook + "&" + "authorId=0");
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
            List<User> listOfUser = new List<User>();
            listOfUser = db.Users.ToList();

            User user = listOfUser.Where(a => a.UserId == Convert.ToInt32(Session["UserId"])).Single();
            string authorName = user.Name + " " + user.Surname;
            News news = new News();
            LibraryRegulations libraryRegulations = new LibraryRegulations();
            news.NewsTitle = "Changes in ";
            if (libraryRegulations.BorrowTime != borrowTime)
            {
                news.NewsContent = "You can borrow your book up to: " + borrowTime.ToString() + " days now." + Environment.NewLine;
                news.NewsTitle += " Loan Length ";
            }

            if (libraryRegulations.Fine != fine)
            {
                news.NewsContent += "Fine for not returning a book on time is now : £" + fine.ToString();
                news.NewsTitle += " Late Fees ";
            }

            news.IsPinned = true;
            news.DisplayOnNews = true;
            news.NewsAuthor = authorName;
            news.NewsPublicationDate = DateTime.Today; ;
            libraryRegulations.BorrowTime = borrowTime;
            libraryRegulations.Fine = fine;
            db.News.Add(news);
            db.LibraryRegulations.AddOrUpdate(libraryRegulations);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult ReturnBook()
        {
            // string sn = RouteData.Values["SerialNumber"] + Request.Url.Query;
            // BookCode bookCode=db.BookCodes.Where(a=>a.BookSerialNumber==bookSerialNumber).Single();
            return View();
        }

        [HttpPost]
        public ActionResult ReturnBook(string bookSerialNumber)
        {

            //    bookSerialNumber = RouteData.Values["SerialNumber"] + Request.Url.Query;
            LibraryRegulations libraryRegulations = new LibraryRegulations();
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
            string bookReview = db.Books.Where(a => a.BookId == bookId).Single().BookReviews;
            Book book = listOfBook.Where(a => a.BookId == bookId).Single();
            book.BookReviews = bookReview;

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


            return Redirect("Stocks?userId" + Session["UserId"]);
        }
        [HttpGet]
        public ActionResult DeleteBook(int? id)
        {

            if (id == null)
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
            user.JoinDate = db.Users.Where(a => a.Email == user.Email).Single().JoinDate;
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
            user.Name = Request["Name"];
            user.Surname = Request["SurName"];
            user.Email = Request["Email"];
            user.Password = hash;
            user.HouseNo = Request["HouseNo"];
            user.DateOfBirth = Request["DateOfBirth"].AsDateTime();
            user.ZipCode = Request["ZipCode"];
            user.UserRole = Request["UserRole"];
            user.JoinDate = DateTime.Today;
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
            DateTime dateBeginning = Request["beginningDate"].AsDateTime();
            DateTime dateEnding = Request["endingDate"].AsDateTime();

            if (dateEnding == DateTime.MinValue) { dateEnding = DateTime.MaxValue; }

            int bookCodeCount = 0;
            List<BookCode> bookCodes = db.BookCodes.ToList();
            int maxBookId = 0;
            if (db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).GroupBy(a => a.BookId).Select(a => a.Count()).Count() > 0)
            {
                bookCodeCount = db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).GroupBy(a => a.BookId).Select(a => a.Count()).Max();// most popular book

            }

            List<int> newBookingsTest = db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).Select(a => a.BookId).ToList();
            List<int> newBookingsTestAll = db.Bookings.Select(a => a.BookId).ToList();

            List<int> newBookings = db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).Select(a => a.BookId).Distinct().ToList();
            ViewBag.BookBorrowCount = newBookingsTest.Count;
            List<Book> books = db.Books.ToList();
            List<string> genre = new List<string>();
            foreach (var book in books)
            {
                genre.Add(db.Books.Where(a => a.BookId == book.BookId).FirstOrDefault().Genre.ToString());
            }


            foreach (int bookId in newBookings)
            {
                int countOfBook = db.Bookings.Where(a => a.BookId == bookId && a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).Count();
                //db.Bookings.Where(a => a.DateCreated >= dateBeginning && a.DateCreated <= dateEnding).GroupBy(a => a.BookId).Select(a=>a.Count());
                if (countOfBook == bookCodeCount)
                {
                    maxBookId = db.Bookings.Where(a => a.BookId == bookId).First().BookId;
                }
            }

            var genreGroup = genre.GroupBy(x => x);
            var maxCount = genreGroup.Max(g => g.Count());
            var mostCommons = genreGroup.Where(x => x.Count() == maxCount).Select(x => x.Key).First();
            ViewBag.MostCommonCategory = mostCommons;
            if (books.Where(a => a.BookId == maxBookId).Count() > 0)
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
            int usersCount = 0;
            if (dateEnding == DateTime.MinValue) { dateEnding = DateTime.Today; }
            if (db.Users.Where(a => a.JoinDate >= dateBeginning && a.JoinDate <= dateEnding).GroupBy(a => a.UserId).Select(a => a.Count()).Count() > 0)
            {
                usersCount = db.Users.Where(a => a.JoinDate >= dateBeginning && a.JoinDate <= dateEnding).Count();

            }
            List<User> users = db.Users.ToList();
            List<Comment> comments = db.Comment.ToList();
            ViewBag.UserCount = usersCount;
            ViewBag.CommentCount = comments.Count();
            ViewBag.BlockedUsers = users.Where(a => a.IsBanned == true).Count();
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
            List<PaymentLibrary> payments = db.Payments.Where(a => a.DatePaid > dateBeginning && a.DatePaid < dateEnding).ToList();
            List<PaymentLibrary> totalPaymentsList = db.Payments.ToList();
            int lateCounter = 0;
            int totalPayments = 0;
            int missedPayments = 0;
            int outstandingPayments = 0;
            int allPayments = 0;
            List<Booking> booking = db.Bookings.ToList();
            foreach (var payment in totalPaymentsList)
            {

                allPayments++;
                if (payment.Status == "Unpaid") { missedPayments++; outstandingPayments += payment.Amount; lateCounter++; }
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
            User user = db.Users.Where(a => a.UserId == bookReserve.UserId).Single();
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
            BookCode bookCode = new BookCode();
            string bookName;
            List<BookReserve> listOfBookReserves = db.BookReserves.ToList();
            List<string> listOfSerialNumbers = new List<string>();
            List<bool> listOfIsInLibrary = new List<bool>();
            List<String> listOfNames = new List<string>();
            foreach (var bookReserve in listOfBookReserves)
            {
                bookCode = db.BookCodes.Where(a => a.BookCodeId == bookReserve.BookCodeId).Single();
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
            BookCode bookCode = new BookCode();
            List<Booking> listOfBookings = db.Bookings.ToList();
            List<string> listOfSerialNumbers = new List<string>();
            List<bool> listOfIsInLibrary = new List<bool>();
            List<String> listOfNames = new List<string>();
            List<String> listOfFirstNames = new List<string>();
            List<String> listOfSurNames = new List<string>();
            List<string> userNames = new List<string>();
            List<User> listofUsers = db.Users.ToList();
            string bookName;
            foreach (var booking in listOfBookings)
            {
                bookCode = db.BookCodes.Where(a => a.BookCodeId == booking.BookCodeId).Single();
                bookName = db.Books.Where(a => a.BookId == bookCode.BookId).Single().Name;
                listOfNames.Add(bookName);
                listOfSerialNumbers.Add(bookCode.BookSerialNumber);
                listOfIsInLibrary.Add(bookCode.IsInLibrary);

                userNames.Add(db.Users.Where(a => a.UserId == booking.UserId).Single().Name + " " + db.Users.Where(a => a.UserId == booking.UserId).Single().Surname);
            }

            ViewBag.UserNames = userNames;
            ViewBag.BookSerialNumber = listOfSerialNumbers;
            ViewBag.ListOfNames = listOfNames;
            ViewBag.ListOfFirstNames = listOfFirstNames;
            ViewBag.ListOfSurNames = listOfSurNames;
            ViewBag.IsInLibrary = listOfIsInLibrary;
            return View(listOfBookings);
        }

        public void GetData()
        {
            DataOperations dataOperations = new DataOperations();
           RecommendationData recommendationData = dataOperations.GetJsonData();
           for (int i = 0; i < recommendationData.count; i++)
           {
               db.Recommendations.AddOrUpdate(recommendationData.recommendations[i]);
               db.SaveChanges();
           }
        }
        public void createCSV()
        {
            //List<int> listOfBookingsTEST = db.Bookings.Select(a=>a.BookId).Distinct().ToList();

            List<Booking> listOfBookings = db.Bookings.Distinct().ToList();
            Dictionary<int,string> usersBooks = new Dictionary<int, string>();
            List<int> listOfUsers = listOfBookings.Select(a => a.UserId).ToList();
            foreach (var user in listOfUsers)
            {
                string listOfBooks = "";
                foreach (var booking in listOfBookings)
                {
                    if (booking.UserId == user && !listOfBooks.Contains(booking.BookId.ToString()) )
                    {
                        listOfBooks += booking.BookId+",";

                    }
                }

                if (!usersBooks.ContainsKey(user) )
                {
                    usersBooks.Add(user, listOfBooks);

                }

            }
            String csv = String.Join(
                Environment.NewLine,
                usersBooks.Select(d => $"{d.Key},{d.Value},")
            );
            System.IO.File.WriteAllText(Server.MapPath(@"~/csvData.csv"), csv);
         
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
