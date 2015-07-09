using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RocketEnglishv2.Models;

namespace RocketEnglishv2.Controllers
{
    public class IntroductionController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nivel"></param>
        // GET: Introduction/Test/5
        public ActionResult Test(int id)
        {
            User u = sessionUser();
            // get arguments
            String strIdCategory = Request.QueryString["idCategory"];
            if (strIdCategory == null)
            {
                // error
            }
            int idCategory = Convert.ToInt32(strIdCategory);
            string token = getNewToken();

            // contruct objects
            CategoryLevel cl = u.Categories[idCategory].ElementAt(id);
            CategoryTest ct = cl.CreateCategoryTest();
            
            // save in session Objects
            HttpContext context = System.Web.HttpContext.Current;
            context.Session[token] = new { challenge = ct, CategoryLevel = cl };

            // view
            ViewBag.token = token;
            ViewBag.level = id;
            return View(ct);
        }

        public ActionResult Global(int id)
        {
            string token = getNewToken();
            User u = sessionUser();

            GlobalTest ct = new GlobalTest(id);
            ct.pollQuestions();

            // save in session Objects
            HttpContext context = System.Web.HttpContext.Current;
            context.Session[token] = new { challenge = ct };

            ViewBag.token = token;
            ViewBag.nivel = id;
            return View(ct);
        }

        public ActionResult Lesson(int id)
        {
            int level = id;
            User u = sessionUser();
            string token = getNewToken();
            String strIdCategory = Request.QueryString["idCategory"];
            if (strIdCategory == null) throw new Exception("Wrong Url, must have idCategory");
            int idCategory = Convert.ToInt32(strIdCategory);
            String strIdLesson = Request.QueryString["idLesson"];
            if (strIdLesson == null) throw new Exception("Wrong Url, must have idLesson");
            int idLesson = Convert.ToInt32(strIdLesson);

            CategoryLevel cl = u.Categories[idCategory].ElementAt(level - 1);
            Lesson l = cl.LessonInfos[idLesson].fetchLesson();

            // save in session Objects
            HttpContext context = System.Web.HttpContext.Current;
            context.Session[token] = new { challenge = l, CategoryLevel = cl };

            ViewBag.token = token;
            ViewBag.nivel = id;
            return View(l);
        }

        private string getNewToken()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }

        public Models.User sessionUser()
        {
            HttpContext context = System.Web.HttpContext.Current;
            if (context.Session["user"] == null || !(context.Session["user"] is Models.User))
            {
                throw new Exception("Do login first");
            }
            ViewBag.User = context.Session["user"];
            return context.Session["user"] as Models.User;
        }
    }
}