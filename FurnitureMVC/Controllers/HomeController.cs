using FurnitureMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace FurnitureMVC.Controllers
{
    public class HomeController : Controller
    {
        public List<Models.Product> productlist = Models.Product.GetData(); // models if vi inte änvand using BibliotekMVC.Models; uppe
        public UserData userdata;

        // GET: Home
        public ActionResult Index()
        {
           
              //  userdata = UserData.GetUserData((int)Session["UserId"]);
            
            
            
            ViewModel VM = ViewModel.viewmodel(productlist, userdata); //VM är en objekt som inngåller båda product list och userdata

            return View(VM);          // visa product lista när starta sidan
        }

        
        public ActionResult AddToCart(int id)
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            foreach (Models.Product product in productlist)
            {
                if (product.Id == id)
                {
                    product.Count--;
                 
                    Models.Product.SaveData(productlist);
                    userdata = UserData.GetUserData((int)Session["UserId"]); // hämta user data
                    if (userdata.ShoppingCartList == null)
                    {
                        userdata.ShoppingCartList = new List<Models.UserData.CartItem>(); //skapa ny lista på lånade böcker om det finns inte redan
                    }
                    userdata.ShoppingCartList.Add(new Models.UserData.CartItem { ProductID = product.Id, Price = product.Price, ProductName = product.Name, Quantity=1});//skapa instans av objekt + 30 dagar lånade dagar från idag
                    Models.UserData.SaveUserData(userdata);
                }
            }

            ViewModel VM = ViewModel.viewmodel(productlist, userdata); //VM är en objekt som innhåller båda product list och userdata
            return View("Index",VM);
        }


        public ActionResult RemoveItem(int id)
        {
            foreach (Models.Product product in productlist)
            {
                if (product.Id == id)
                {
                    product.Count++;
                    Models.Product.SaveData(productlist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    var itemToRemove = userdata.ShoppingCartList.FirstOrDefault(r => r.ProductID == id);
                    if (itemToRemove != null)
                    {
                        userdata.ShoppingCartList.Remove(itemToRemove);
                        Models.UserData.SaveUserData(userdata);
                    }

                }
            }

            ViewModel VM = ViewModel.viewmodel(productlist, userdata); //VM är en objekt som inngåller båda product list och userdata
            return RedirectToAction("Index", "Cart");
           // return View("Index", VM);
        }

        

        public ActionResult Cart()
        {
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Register()
        {
            return RedirectToAction("Index", "Register");
        }
    }
}