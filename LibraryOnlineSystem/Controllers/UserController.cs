using System.Web.Mvc;

namespace LibraryOnlineSystem.Controllers
{
    public class UserController : BaseController
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        // GET: LogIn/Details/5
        public ActionResult MyAccount(int id)
        {
            return View();
        }

        public ActionResult Account(int id)
        {
            return View();
        }
        // GET: LogIn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogIn/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogIn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogIn/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogIn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogIn/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
