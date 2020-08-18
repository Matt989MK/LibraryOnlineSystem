using LibraryOnlineSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryOnlineSystem.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        private readonly LibraryContext db = new LibraryContext();
        // GET: Base


        public class MyPropertyActionFilter : ActionFilterAttribute
        {
            private readonly LibraryContext db = new LibraryContext();

            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {




                List<News> listOfNews = db.News.ToList();
                List<string> pinnedNews = new List<string>();
                List<string> libraryNews = new List<string>();
                List<int> newsId = new List<int>();
                foreach (var news in listOfNews)
                {
                    if (news.IsPinned)
                    {
                        pinnedNews.Add(news.NewsTitle);
                    }

                    if (news.DisplayOnNews)
                    {
                        libraryNews.Add(news.NewsTitle);
                        newsId.Add(news.NewsId);
                    }
                }


                filterContext.Controller.ViewBag.PinnedNews = pinnedNews;
                filterContext.Controller.ViewBag.libraryNews = libraryNews;
                filterContext.Controller.ViewBag.newsIds = newsId;

            }
        }


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