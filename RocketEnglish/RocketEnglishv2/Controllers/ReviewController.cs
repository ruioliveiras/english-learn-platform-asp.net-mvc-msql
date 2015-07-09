using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RocketEnglishv2.DAO;
using RocketEnglishv2.Models;

namespace RocketEnglishv2.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Global(int id)
        {
            readLoadArgs();

            if (!(ViewBag.Challenge is GlobalTest))
            { throw new Exception("Error this token is not from a GlobalTest, do introduction first"); }

            GlobalTest gt = ViewBag.Challenge as GlobalTest;

            //check bool to inform user
            bool isCompleted = gt.saveGlobalTestResults(sessionUser().Id);
            if (isCompleted)
            {
                ((User)ViewBag.User).GlobalLvl += (((User)ViewBag.User).GlobalLvl < gt.Level + 1) ? 1:0;
            }

            ViewBag.id = gt.Level;

            return View(gt);
        }
        
        public ActionResult Test(int id)
        {
            readLoadArgs();

            if (!(ViewBag.Challenge is CategoryTest))
            { throw new Exception("Error this token is not from a CategoryTest, do introduction first"); }

            CategoryTest ct = ViewBag.Challenge as CategoryTest;

            //check bool to inform user
            bool isCompleted = ct.saveCategoryTestResults(sessionUser().Id);

            ViewBag.id = ct.CategoryLevel.Level;
            return View(ct);
        }

        public ActionResult Lesson(int id)
        {
            readLoadArgs();

            if (!(ViewBag.Challenge is Lesson))
            { throw new Exception("Error this token is not from a Lesson, do introduction first"); }

            Lesson l = ViewBag.Challenge as Lesson;

            //check bool to inform user
            bool isCompleted = l.saveResults(sessionUser().Id);

            ViewBag.id = l.Id;
            return View(l);
        }

        private void readLoadArgs()
        {
            String token = Request.QueryString["token"];
            if (token == null)
            { throw new Exception("Error no available token - do Introduction first"); }

            HttpContext context = System.Web.HttpContext.Current;
            if (context.Session[token] == null)
            { throw new Exception("Error wrong token, do introduction first"); }

            ViewBag.Token = token;
            dynamic v = context.Session[token];
            ViewBag.Challenge = v.challenge;
            if (v.GetType().GetProperty("CategoryLevel") != null)
            {
                ViewBag.CategoryLevel = v.CategoryLevel;
            }
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