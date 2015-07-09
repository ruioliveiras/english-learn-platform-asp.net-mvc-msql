using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using RocketEnglishv2.DAO;
using RocketEnglishv2.Services;
using RocketEnglishv2.Models;
using System.IO;


namespace RocketEnglishv2.Controllers
{




    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            RocketEnglish a = new RocketEnglish();
            Categoria c = a.Categoria
                .Where(b => b.id == 1)
                .First();
            Debug.WriteLine("Categoria" + c.ToString());


            return View();
        }

        public ActionResult CleanDB()
        {
            string s = Server.MapPath("~/Content/");
            InstallService install = new InstallService(s);

            install.DBClean();

            return View("Debug");
        }

        // GET: Test
        public ActionResult Init()
        {
            string s = Server.MapPath("~/Content/");
            InstallService install = new InstallService(s);

            install.DBClean();

            //DBexpander dbe = new DBexpander();
            //dbe.processFile(s+"csv/example.csv");

            install.DBLoadSample();

            return View();
        }

        public ActionResult Init2()
        {
            string s = Server.MapPath("~/Content/");
            InstallService install = new InstallService(s);

            //install.DBClean();

            //DBexpander dbe = new DBexpander();
            //dbe.processFile(s+"csv/example.csv");

            //install.DBLoadSample();
            install.UserDoLessons();

            return View("init");
        }

        // GET: Test
        public ActionResult Model()
        {
           
            //Lesson l = new Lesson(1, "Descricao", "A tua prima");
            //l.pollQuestions();

            //GlobalTest cg = new GlobalTest(1);
            //cg.pollQuestions();

            //CategoryLevel cl = new CategoryLevel(1, "a tua prima", 1, 0);
            //CategoryTest ct = cl.CreateCategoryTest();
            
            return View("Debug");
        }
        
        public ActionResult Home()
        {
            RocketEnglishv2.DAO.RocketEnglish a = new RocketEnglish();
            Utilizador u = a.Utilizador.First();

            RocketEnglishv2.Models.User user = new Models.User(u.id, u.nome, u.username, u.email, u.passsword, u.nivelGlobal, u.loginSeguidosMaximo, u.loginSeguidosAtual, u.dataUltimoLogin, u.tutorialFeito);

            HttpContext context = System.Web.HttpContext.Current;
            context.Session["user"] = user;

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Video()
        {

            return View("Debug");
        }
        
        public ActionResult Image()
        {
            return View("Debug");
        }

        public ActionResult TestSave()
        {

            return View("Debug");
        }

        public ActionResult Translator()
        {
            String word = TranslatorContainer.TranslateBing("Bus", "en", "pt-pt");
            return View("Debug");
        }
    }
}
