using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FurnitureMVC.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }           
        public List<CartItem> ShoppingCartList { get; set; }    // list på alla product som änvander har köpt

        public int BuyCount { get; set; } //Antal Köpt /bok
        public int Points { get; set; }

        public static List<UserData> UserList = GetAllUsers();

        public static UserData GetUserData(string Email)
        {
            var selected = UserList.Where(x => x.Email == Email).FirstOrDefault();
            return selected;
        }



        public static UserData GetUserData(int id) //
        {
            UserData userdata;
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + id + " .json");


            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                userdata = JsonConvert.DeserializeObject<UserData>(json, settings);
            }
            else
            {
                userdata = UserList.Where(x => x.Id == id).FirstOrDefault();
            }

            return userdata;
        }

        

        public static void SaveUserData(UserData user)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + user.Id + " .json");
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(user, settings);
            System.IO.File.WriteAllText(filepath, json);

        }



        public static List<UserData> GetUsers()
        {
            List<UserData> UserList = new List<UserData>();
            UserList.Add(new UserData { Id = 1, Email = "a_asultan@hotmail.com", Password = "hejsan", Name = "Adam Sultan" });
            UserList.Add(new UserData { Id = 2, Email = "sara@hotmail.com", Password = "Sara", Name = "Sara Larson" });
            return UserList;
        }

        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/users.json");


        public static bool SaveNewUser(List<UserData> userlist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(userlist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }

        public static List<UserData> GetAllUsers()
        {
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/users.json");
            List<UserData> users;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                users = JsonConvert.DeserializeObject<List<UserData>>(json, settings);
            }
            else
            {
                users = GetUsers();
            }           
            

            return users;
        }


        public class CartItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }

        }

    }
}