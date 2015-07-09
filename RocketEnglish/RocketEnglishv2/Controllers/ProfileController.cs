using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using RocketEnglishv2.DAO;
using RocketEnglishv2.Models;
using System.Web.Script.Serialization;
using System.IO;


namespace RocketEnglishv2.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            User u = sessionUser();

            ViewBag.UnlockableAll = Unlockable.FindAll() ;
            // change this to u.getUnlockablesDone() or something
            ViewBag.UnlockableDone = new int[] { };

            return View(u);
        }


        [HttpPost]
        public JsonResult Unlockble()
        {
            StreamReader sr = new StreamReader(Request.InputStream);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic answerJson = js.Deserialize<dynamic>(sr.ReadLine());
            string name = answerJson["name"];
            string type = answerJson["type"];


            User u = sessionUser();
            if (type == "body"){
                u.SetBody(name);
            }else if (type == "head"){
                u.SetHead(name);
            }

            return Json(new Object(), JsonRequestBehavior.AllowGet); ;
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