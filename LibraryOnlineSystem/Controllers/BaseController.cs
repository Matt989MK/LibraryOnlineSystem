using System;
using LibraryOnlineSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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

        public List<Recommendation> getRecommendations()
        {
            List<User> listOfUser = db.Users.ToList();
            User user = listOfUser.Where(a => a.UserId == Convert.ToInt32(Session["UserId"])).Single();
            List<Booking> userLoans = db.Bookings.Where(a => a.UserId == user.UserId).ToList();
            
            List<Recommendation> recommendationData = db.Recommendations.OrderByDescending(x=>x.lift).ToList();
            List<Recommendation> recommendationForUser = new List<Recommendation>();
           
            foreach (var item in recommendationData)
            {

                for (int i = 0; i < userLoans.Count; i++)
                {
                    if (item.bookBase == userLoans[i].BookId)
                    {
                        recommendationForUser.Add(recommendationData[i]);
                    }
                }
                
            }
         
            List<Recommendation>  sorderRecommendations =recommendationForUser.OrderByDescending(a => a.lift).ToList();

           
            return sorderRecommendations;
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

        public void SetRecommendations()
        {
            Book book;
            List<Book> listOfBooks = db.Books.ToList();
            List<Book> recommendedBooks = new List<Book>();
            List<Recommendation> recommendations = getRecommendations();
            if (recommendations.Count != 0)
            {
                for (int i = 0; i < recommendations.Count; i++)
                {
                    book = listOfBooks.Where(a => a.BookId == recommendations[i].bookRecommended).Single();
                    recommendedBooks.Add(book);
                }

                
                ViewBag.RecommendedBooks = recommendedBooks.Take(5);
               
            }
           
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