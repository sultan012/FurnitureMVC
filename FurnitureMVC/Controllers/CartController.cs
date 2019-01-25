using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureMVC.Models;

namespace FurnitureMVC.Controllers
{
    public class CartController : Controller

    {
        public List<Models.Product> productlist = Models.Product.GetData();

        public UserData userdata;
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            ViewModel VM = ViewModel.viewmodel(productlist, userdata);
            return View(VM);
        }

        public ActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ClearProductList()
        {

           UserData userdata = UserData.GetUserData((int)Session["UserId"]);
            userdata.ShoppingCartList.Clear();
            UserData.SaveUserData(userdata);

            return RedirectToAction("Index", "Home");
        }
    }  

}