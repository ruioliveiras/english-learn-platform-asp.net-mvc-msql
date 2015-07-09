using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RocketEnglishv2.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.Script.Serialization;
using RocketEnglishv2.Services;

namespace RocketEnglishv2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            User u = sessionUser();
            u.loadCategories();
            var e = u.Categories.GetEnumerator();
            e.MoveNext();

//            List<List<CategoryLevel>> categorys = CategoryLevel.findAll();
            ViewBag.User = u;
            ViewBag.Categories = u.Categories;
            ViewBag.GlobalLvl = u.GlobalLvl;
            ViewBag.NewGlobalLvlAvaliable = true;
            foreach(List<CategoryLevel> l in u.Categories.Values){
                ViewBag.NewGlobalLvlAvaliable = ViewBag.NewGlobalLvlAvaliable
                    && l[u.GlobalLvl + 1].IsDone();
            }
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public Models.User sessionUser()
        {
            HttpContext context = System.Web.HttpContext.Current;
            if (context.Session["user"] == null || !(context.Session["user"] is Models.User))
            {
                throw new Exception("Do login first");
            }
            return context.Session["user"] as Models.User;
        }
    }
}