using LibraryOnlineSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationShutdownReason = System.Web.ApplicationShutdownReason;

namespace LibraryOnlineSystem.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        private LibraryContext db = new LibraryContext();
        // GET: Base
   
        public void LaunchVariables()
        {
            List<News> listOfNews = db.News.ToList();
            List<string> pinnedNews = new List<string>();
            List<string> libraryNews = new List<string>();
            foreach (var news in listOfNews)
            {
                if (news.IsPinned)
                {
                    pinnedNews.Add(news.NewsTitle);
                }

                if (news.DisplayOnNews)
                {
                    libraryNews.Add(news.NewsTitle);
                }
            }

            ViewBag.PinnedNews = pinnedNews;
            ViewBag.libraryNews = libraryNews;

        

        }

        public List<string> LaunchLibraryNews()
        {
            List<News> listOfNews = db.News.ToList();
            List<string> libraryNews = new List<string>();

            foreach (var news in listOfNews)
            {
              

                if (news.DisplayOnNews)
                {
                    libraryNews.Add(news.NewsTitle);
                }
            }

            return libraryNews;
        }
    }
}