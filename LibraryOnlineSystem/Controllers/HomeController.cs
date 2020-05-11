using LibraryOnlineSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryOnlineSystem.Paypal;
using PayPal.Api;
using System.Runtime.Remoting.Contexts;
using System.Net.Mail;
using System.Security.Policy;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LibraryOnlineSystem.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db = new LibraryContext();
        Random rnd = new Random();
        private int? paymentTestId;
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
        public ActionResult ReviewBook(int bookId, int userId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReviewBook(BookReview bookReview) //TT
        {
            bookReview = new BookReview();
            bookReview.UserId = 1;
            bookReview.BookId = 1;
            bookReview.BookReviewId = 1;
            bookReview.Content = "testReview";
            bookReview.DatePosted = DateTime.Now;
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
            List<PaymentLibrary> paymentList = new List<PaymentLibrary>();
            foreach (Booking booking in bookingList)
            {
                paymentList = db.Payments.Where(a => a.BookingId == booking.BookingId).ToList();
                Book book = db.Books.Where(a => a.BookId == booking.BookId).Single();
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
            List<Booking> bookingList = db.Bookings.Where(a => a.User.UserId == userId).ToList();
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
            PaymentLibrary payment = db.Payments.Where(a => a.UserId == userId).FirstOrDefault();
            Booking booking = db.Bookings.Where(a => a.BookingId == payment.BookingId).Single();
            Book book = db.Books.Where(a => a.BookId == booking.BookId).Single();


            return View(payment);
        }

        [HttpPost]
        public ActionResult PayFine(int userId, int paymentId)
        {
            PaymentLibrary payment = db.Payments.Where(a => a.PaymentLibraryId == paymentId).FirstOrDefault();
            payment.DatePaid = DateTime.Now;
            payment.Status = "Paid";
            db.Payments.AddOrUpdate(payment);
            db.SaveChanges(); 
            return Redirect("Index");
        }
        public ActionResult Payments(int userId)
        {

            List<Booking> bookingList = db.Bookings.Where(a => a.User.UserId == userId && a.DateReturned < DateTime.Now).ToList();
            List<PaymentLibrary> paymentList = db.Payments.Where(a => a.UserId == userId).ToList();
            List<Booking> bookingListDisplay = db.Bookings.Where(a => a.User.UserId == userId).ToList();
            string paymentAmount = paymentList.FirstOrDefault().Amount.ToString();
            ViewBag.paymentAmount = paymentAmount;

            List<string> bookNames = new List<string>();
            List<DateTime?> datesReturned = new List<DateTime?>();
            foreach (var booking in bookingListDisplay)
            {
                Book book = db.Books.Where(a => a.BookId == booking.BookId).Single();

              
                bookNames.Add(book.Name);
                //if (booking.DateReturned == null)
                //{
                booking.DateReturned = booking.DateReturned;
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
       
        public ActionResult PaymentWithPaypal(int? paymentId,string Cancel = null)
        {
           
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    ViewBag.guid = guid;
                    if (paymentId != null)
                    {
                        PaymentLibrary paymentLibrary = db.Payments.Where(a => a.PaymentLibraryId == paymentId).Single();
                        paymentLibrary.guId = guid;
                        db.Payments.AddOrUpdate(paymentLibrary);
                        db.SaveChanges();
                    }
                   
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    ViewBag.guid = guid;
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        ViewBag.error = "you don't have enough funds";
                        return View("FailureView");
                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                Console.Write(ex.Response);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.ToString();

                return View("FailureView");
            }
            //on successful payment, show success page to user. 
            string guid1=ViewBag.guid;
            PaymentLibrary paymentLibrary1 = db.Payments.Where(a => a.guId == guid1).Single();
                paymentLibrary1.DatePaid = DateTime.Now;
                paymentLibrary1.Status = "Paid";
                db.Payments.AddOrUpdate(paymentLibrary1);
                db.SaveChanges();
            
           
            return View("SuccessView");
        }
        [HttpGet]
        public ActionResult SuccessView()
        {
            return View();
        }
       
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            LibraryRegulations libraryRegulations = new LibraryRegulations();
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {

                name = "Library fee",
                currency = "GBP",
                price = libraryRegulations.Fine.ToString(),
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            
            var amount = new Amount()
            {
                
                currency = "GBP",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                
                description = "Transaction description",
                invoice_number = rnd.Next(1, 1000).ToString()
                , //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
        [HttpPost]
        public JsonResult isUserExists(string email)
        {
          
           // db.Configuration.ValidateOnSaveEnabled = false;

            bool isExist = db.Users.Where(a => a.Email==email).Count()>0;
          //      db.Configuration.ValidateOnSaveEnabled = true;

                return Json(!isExist,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
             user=new User();
            var hash = SecurePasswordHasher.Hash(Request["Password"]);
            
           // user.ConfirmPassword = hash;
            user.Email = Request["Email"];
            user.HouseNo = Request["HouseNo"];
            user.DateOfBirth = Request["DateOfBirth"].AsDateTime();
            user.ZipCode = Request["ZipCode"];
            user.Name = Request["Name"];
            user.Surname = Request["SurName"];
            user.Password = hash;
            user.UserRole = "User";
          //  bool test = Equals(user.Password, user.ConfirmPassword);
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
         //       user.ConfirmPassword = user.Password;
                db.Users.Add(user);
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                Session["isAdmin"] = "0";
                Session["UserName"] = "User " + user.Name;
                Session["UserId"] = user.UserId.ToString();
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
           
        }


        [HttpGet]
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return Redirect("/Home/Login");
        }

      

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var context = new LibraryContext())
            {
                User user1 = new User();

                //int x = context.Users.Count();
                string email = Request["Email"];
               
                string password = Request["Password"];
                // db.Configuration.ValidateOnSaveEnabled = false;
                // user1.ConfirmPassword = password;
                if (context.Users.Where(a => a.Email == email).Count()>0)
                {
                    user1 = context.Users.Where(a => a.Email == email).Single();//error because confirm password doesn't match

                }
                else
                {
                    View();
                }
                //db.Configuration.ValidateOnSaveEnabled = true;
                if (password == null)
                {
                    return Redirect("/Home/Login");
                }
                string hash = SecurePasswordHasher.Hash(password);
                bool result = SecurePasswordHasher.Verify(password, user1.Password);
                if (context.Users.Where(a => a.Email == email).Count() > 0 && result == true)
                {
                    user = context.Users.Find(1);

                    user1 = context.Users.Where(a => a.Email == email).Single();
                    if (user1.UserRole == "Admin")
                    {
                        Session["isAdmin"] = "1";
                        Session["UserId"] = user1.UserId.ToString();
                        Session["UserName"] = "Admin " + user.Name;
                    }
                    else
                    {
                        Session["isAdmin"] = "0";
                        Session["UserName"] = "User " + user.Name;
                        Session["UserId"] = user1.UserId.ToString();
                    }
                    return Redirect("/Home/Index");
                }
                else
                {
                    Response.Redirect("/home/login", true);
                    Response.End();
                }

                return View("Error");
            }

           
        }
        public ActionResult ListOfReservations(int userId)
        {
            BookCode bookCode = new BookCode();
            List<BookReserve> listOfBookReserves = db.BookReserves.Where(a=>a.UserId==userId).ToList();
            foreach (var bookReserve in listOfBookReserves)
            {
                bookCode = db.BookCodes.Where(a => a.BookCodeId == bookReserve.BookCodeId).Single();
                ViewBag.BookSerialNumber = bookCode.BookSerialNumber;
                ViewBag.IsInLibrary = bookCode.IsInLibrary;
            }
            return View(listOfBookReserves);
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(User user)
        {
            string email = Request["Email"];
            if (db.Users.Where(a => a.Email == email).Count() > 0)
            {

                try
                {
                    Random random=new Random();
                  //generate token
                  string tokenString = random.Next(1, 1000).ToString();
                   // var token = WebSecurity.GeneratePasswordResetToken(email);
                    //save token and user to db
                    Token token=new Token();
                    token.TokenId = tokenString;
                    token.Email = email;
                    db.Tokens.Add(token);
                    db.SaveChanges();
                   // var resetLink = "<a href='" + Url.Action("ResetPassword", "Home", new { token=tokenString }, "http") + "'>Reset Password</a>";
                   var resetLink = "https://localhost:44362/Home/ResetPassword?token=" + tokenString;
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("hahefhguas1234@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Password Reset Token";
                    mail.Body = "Please find the link to Reset your Password" + resetLink;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("hahefhguas1234@gmail.com", "ZAQ13wsx");
                    SmtpServer.EnableSsl = true;
                    
                    SmtpServer.Send(mail);
                    return View("EmailSent");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
                return View("EmailSent");    //send email
            }
            else
            {

                return View("UserDoesntExist");
            }

            
        }
        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            if (db.Tokens.Where(a => a.TokenId == token).Count() > 0)
            {
                Token _token = db.Tokens.Where(a => a.TokenId == token).Single();
                User user = db.Users.Where(a => a.Email == _token.Email).Single();
                db.Tokens.Remove(_token);
                db.SaveChanges();
                return View(user);
            }
            else
            {
                return View("Error");
            }
           

          
        }

        [HttpPost]
        public ActionResult ResetPassword(User user)
        {
            var hash = SecurePasswordHasher.Hash(Request["Password"]);

            user.Password = hash;
            if (ModelState.IsValid)
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
                return View("SuccessfullyChangedPassword");
            }
            else
            {
                return View("Error");
            }

        }

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
            user.UserRole = "User";
            user.Password = db.Users.Where(a => a.Email == user.Email).Single().Password;
            if (ModelState.IsValid)
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
                return Redirect("/Home/Index");

            }
            else
            {
                return View();
            }
        }
    }
    }

