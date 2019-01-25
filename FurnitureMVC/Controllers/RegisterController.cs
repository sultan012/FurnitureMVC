using FurnitureMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FurnitureMVC.Controllers
{
    public class RegisterController : Controller
    {
        public static List<UserData> UserList = UserData.GetAllUsers();
        // GET: Register
        public ActionResult Index()
        {
            if (HttpContext.Request.RequestType == "POST")
            {
                var email = Request.Form["email"];
                var password = Request.Form["password"];
                var firstName = Request.Form["firstName"];
                var lastName = Request.Form["lastName"];
                var fullName = firstName + " " + lastName;
                var id = UserList.Count;

                UserList.Add(new UserData { Id = ++id, Email =email, Password = password, Name = fullName});

                UserData.SaveNewUser(UserList);
                return RedirectToAction("Index", "Login");

            }
           return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }
    }
    
}