using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RocketEnglishv2.Models;
using System.IO;
using System.Web.Script.Serialization;

namespace RocketEnglishv2.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Answer(int id)
        {
            readLoadArgs();
            
            StreamReader sr = new StreamReader(Request.InputStream);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic answerJson = js.Deserialize<dynamic>(sr.ReadLine());
            string answer = answerJson["answer"];

            Question q = ViewBag.Challenge.questions[ViewBag.NumQuestion];
            Object ret;
            if (q.Answer == "" || !(ViewBag.Challenge is Lesson) )
            {
                q.answer(answer);
                if (ViewBag.Challenge is Lesson)
                {
                    ret = new { isCorrect = q.isCorrect() };
                }
                else
                {
                    ret = new { };
                }
            }
            else
            {
                ret = new { error = "anwsered", anwser = q.Answer};
            }
           
            

            return Json(ret, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult Test(int id)
        {
            readLoadArgs();

            if (!(ViewBag.Challenge is CategoryTest))
            { throw new Exception("Error this token is not from a CategoryTest, do introduction first"); }

            CategoryTest l = ViewBag.Challenge as CategoryTest;

            ViewBag.Action = "Test";
            ViewBag.id = l.CategoryLevel.Level;
            return View("Lesson",l.questions);
        }

        public ActionResult Global(int id)
        {
            readLoadArgs();

            if (!(ViewBag.Challenge is GlobalTest))
            { throw new Exception("Error this token is not from a GlobalTest, do introduction first"); }

            GlobalTest l = ViewBag.Challenge as GlobalTest;

            ViewBag.Action = "Global";
            ViewBag.id = l.Level;
            return View("Lesson", l.questions);
        }

        public ActionResult Lesson(int id)
        {
            readLoadArgs();

            if (!(ViewBag.Challenge is Lesson))
            { throw new Exception("Error this token is not from a Lesson, do introduction first"); }

            Lesson l = ViewBag.Challenge as Lesson;

            ViewBag.Action = "Lesson";
            ViewBag.id = l.Id;
            return View(l.questions);
        }

        private void readLoadArgs()
        {
            sessionUser();
            String token = Request.QueryString["token"];
            if (token == null) 
            { throw new Exception("Error no available token - do Introduction first"); }

            String strNQuestion = Request.QueryString["NumQuestion"];
            int numQuestion = (strNQuestion == null) ? 0 : Convert.ToInt32(strNQuestion);

            HttpContext context = System.Web.HttpContext.Current;
            if (context.Session[token] == null)
            { throw new Exception("Error wrong token, do introduction first"); }

            ViewBag.NumQuestion = numQuestion;
            ViewBag.Token = token;
            dynamic v = context.Session[token];
            ViewBag.Challenge = v.challenge;
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