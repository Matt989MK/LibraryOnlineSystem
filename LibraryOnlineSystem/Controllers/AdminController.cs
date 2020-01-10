using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Edit(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "UserId,Name,Surname,DateOfBirth,Email,HouseNo,ZipCode,UserRole,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            db.Users.Add(user);
            db.SaveChanges();
           // return RedirectToAction("Index","User");
           return View("AddedUser");
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
