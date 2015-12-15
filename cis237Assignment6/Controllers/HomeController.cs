using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cis237Assignment6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Beverage List Editor Features";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "For assitance with this website, please contact me via email.";

            return View();
        }
    }
}