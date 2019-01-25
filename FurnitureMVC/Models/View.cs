using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureMVC.Models
{
    public class ViewModel
    {
        public List<Product> ProductList { get; set; }
        public UserData UserData { get; set; }

        public static ViewModel viewmodel(List<Product> productList, UserData userdata)
        {
            ViewModel VM = new ViewModel();
            VM.ProductList = productList;
            VM.UserData = userdata;
            return VM;
        }

    }
}