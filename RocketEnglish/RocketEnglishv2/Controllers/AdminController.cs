using RocketEnglishv2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RocketEnglishv2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpandDB()
        {
            if (Request.Files.Count > 0)
            {
                string pathToSave = Server.MapPath("~/Content/csv");
                string filename = Request.Files.Get(0).FileName;
                if (filename == "")
                {
                    throw new Exception("Empty file");
                }
                string completePath = Path.Combine(pathToSave, filename);
                Request.Files.Get(0).SaveAs(completePath);

                DBexpander dbe = new DBexpander();
                dbe.processFile(completePath);
                return View("Sucess");
            }
            return View();
        }
    }
}