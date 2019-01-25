using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FurnitureMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }

        public int Count { get; set; }
        public int InitialCount { get; set; }
        public string ImageUrl { get; set; }
        public int Points { get; set; }
        public int BuyCount { get; set; } //Antal Köpta product


        public static List<Product> CreateData()
        {
            List<Product> ProductList = new List<Product>();
            string imageUrl = "/Content/images/si3.jpg";

            ProductList.Add(new Bed { Id = 1, Name = "DUNVIK", Description = "En stilren kontinentalsäng med en skön huvudgavel som är klädd runtom – perfekt att ställa mitt i rummet. ch skulle olyckan vara framme kan du ta av tyget och tvätta det i maskin.", Color = "Mörkgrå ", Price = 7495, Count = 20, InitialCount =20, ImageUrl = "/Content/images/sa2.jpg" });
            ProductList.Add(new Bed { Id = 4, Name = "ANATOMIC", Description = "Den bästa nattsömnen får du i en säng, som anpassar sig efter din kropp. Därför väljer fler och fler att köpa en komfortabel ställbar säng, när de vill sova bättre. Denna typ av säng kan ställas in i huvud- och fotändan.", Color = "Grå ", Price = 35299, Count = 35, InitialCount = 35, ImageUrl = "/Content/images/sa1.jpg" });
            ProductList.Add(new Bed { Id = 2, Name = "LIDHULT", Description = "En välförtjänt tupplur på dagen och skönt häng med familj och vänner på kvällen. Soffan LIDHULT är designad för att vara så bekväm som möjligt med hög rygg och nackstöd. Omfamnande, inbjudande och generös.", Color = "Lejde grå/svart ", Price = 18295, Count = 25, InitialCount = 25, ImageUrl = "/Content/images/s1.jpg" });
            ProductList.Add(new Bed { Id = 3, Name = "AXELSTORP", Description = "En välförtjänt tupplur på dagen och skönt häng med familj och vänner på kvällen. Soffan LIDHULT är designad för att vara så bekväm som möjligt med hög rygg och nackstöd. Omfamnande, inbjudande och generös.", Color = "Grann/Bomstad svart ", Price = 12495, Count = 30, InitialCount = 30, ImageUrl = "/Content/images/s2.jpg" });
            ProductList.Add(new Bed { Id = 5, Name = "FRED", Description = "Det här bordet är utdragbart så du alltid har plats för gäster, och det dolda låset förhindrar irriterande glipor mellan iläggsskivorna. Ytan är lätt att torka av och hjälper dig hantera skvätt och spill.", Color = "Rökt ek/svart ", Price = 11995, Count = 40, InitialCount = 40, ImageUrl = "/Content/images/m1.jpg" });
            ProductList.Add(new Bed { Id = 6, Name = "VIENNA", Description = "Vienna matbord är ett snyggt och nätt bord från Rowico. Med sin tunna utformning i både ben och skiva i kombination med den ljusa färgen så bidrar även Vienna till att rummet känns större och mer rymligt.", Color = "Vit ", Price = 6995, Count = 45, InitialCount = 45, ImageUrl = "/Content/images/m2.jpg" });
                                                                                                
            return ProductList;
        }

        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/Product.json");


        public static bool SaveData(List<Product> productlist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(productlist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }


        public static List<Product> GetData()
        {
            List<Product> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Product>>(json, settings);
            }
            else
            {
                data = CreateData();
            }
            // Algoritm

            data = data.OrderBy(x => x.InitialCount).ToList(); //minst böcker från början 
            int points = 0;
            foreach (var d1 in data)
            {
                points = points + 5;
                d1.Points = points;
            }
            data = data.OrderBy(x => x.BuyCount).ToList();
            points = 0;
            foreach (var d2 in data)
            {
                points = points + 3;
                d2.Points += points;
            }

            data = data.OrderByDescending(x => x.Points).ToList(); // sortera mera point först


            return data;
        }

    }


    public class Bed : Product
    {
        public int Width { get; set; }
    }

    public class Couch : Product
    {
        public int NumberOfSeats { get; set; }
        public string Type { get; set; }
    }

    public class Table : Product
    {
        public string Size { get; set; }
    }


}