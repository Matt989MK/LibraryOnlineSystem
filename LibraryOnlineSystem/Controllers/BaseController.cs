using Autofac.Core;
using LibraryOnlineSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        //protected override ViewResult View(IView view, object model)
        //{


        //    List<News> listOfNews = db.News.ToList();
        //    List<string> pinnedNews = new List<string>();
        //    List<string> libraryNews = new List<string>();
        //    foreach (var news in listOfNews)
        //    {
        //        if (news.IsPinned)
        //        {
        //            pinnedNews.Add(news.NewsTitle);
        //        }

        //        if (news.DisplayOnNews)
        //        {
        //            libraryNews.Add(news.NewsTitle);
        //        }
        //    }

        //    this.ViewBag.PinnedNews = pinnedNews;
        //    this.ViewBag.libraryNews = libraryNews;


        //    return base.View(view, model);
        //}


        //public class ViewBagPropertyModule : Module
        //{
        //    protected override void AttachToComponentRegistration(IComponentRegistry cr,
        //        IComponentRegistration reg)
        //    {
        //        Type limitType = reg.Activator.LimitType;
        //        if (typeof(Controller).IsAssignableFrom(limitType))
        //        {
        //            registration.Activated += (s, e) =>
        //            {
        //                dynamic viewBag = ((Controller)e.Instance).ViewBag;
        //                viewBag.MyProperty = "value";
        //            };
        //        }
        //    }
        //}

        public class MyPropertyActionFilter : ActionFilterAttribute
        {
            private LibraryContext db = new LibraryContext();

            public override void OnResultExecuting(ResultExecutingContext filterContext)
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


                filterContext.Controller.ViewBag.PinnedNews = pinnedNews;
                filterContext.Controller.ViewBag.libraryNews = libraryNews;

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