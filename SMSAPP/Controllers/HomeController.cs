using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSAPP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["ULIDC"] = "3";
            Session["ULCC"] = "ac7310b5-ff8b-4ea7-a125-bdb75ab434a7";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Número 1 Marketing Ltda. EPP.";

            return View();
        }
    }
}