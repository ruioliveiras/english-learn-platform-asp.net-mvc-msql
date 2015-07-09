using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using RocketEnglishv2.Services;
using System.IO;
using System.Web.Script.Serialization;

namespace RocketEnglishv2.Controllers
{
    public class TranslateController : Controller
    {
        public JsonResult PtEn()
        {
            StreamReader sr = new StreamReader(Request.InputStream);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic json = js.Deserialize<dynamic>(sr.ReadLine());
            string word = json["word"];
            word = TranslatorContainer.TranslateBing(word, "en", "pt-pt");

            Object ret = new {word = word};
             
            return Json(ret, JsonRequestBehavior.AllowGet); ;
        }
    }
}
