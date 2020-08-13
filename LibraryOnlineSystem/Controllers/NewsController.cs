using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using LibraryOnlineSystem.Models;

namespace LibraryOnlineSystem.Controllers
{
    public class NewsController : BaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: News
        public ActionResult RecentNews()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PostNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostNews(News news)
        {
            List<User> listOfUser = new List<User>();
            listOfUser = db.Users.ToList();

            User user =listOfUser.Where(a => a.UserId == Convert.ToInt32(Session["UserId"])).Single();
            string authorName = user.Name +" "+ user.Surname;



            

            news.NewsContent = Request.Params["NewsContent"];
            news.NewsPublicationDate = DateTime.Today;
            news.NewsAuthor = authorName;
            news.NewsTitle = Request.Params["NewsTitle"];
            news.IsPinned = Request.Form.Get("isPinned").AsBool();
            news.DisplayOnNews = Request.Form.Get("displayOnNews").AsBool();
            if (ModelState.IsValid)
            {
                db.News.Add(news);

                db.SaveChanges();

            }

            return Redirect("/News");
        }
    }
}