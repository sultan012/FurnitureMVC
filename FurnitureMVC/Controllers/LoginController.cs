using FurnitureMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.Request.RequestType == "POST")
            {
                var Email = Request.Form["email"];
                var Password = Request.Form["password"];

                var CurrentUser = UserData.GetUserData(Email);

                if (CurrentUser != null && CurrentUser.Password == Password)
                {
                    Session["UserId"] = CurrentUser.Id;
                    Session["UserName"] = CurrentUser.Name;

                    return RedirectToAction("Index", "Home");   // index i home controller 
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return RedirectToAction("Index", "Register");
        }

        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
    }


}
