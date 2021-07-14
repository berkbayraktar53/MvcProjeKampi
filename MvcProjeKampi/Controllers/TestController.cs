using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test2()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Test3()
        {
            return View();
        }
        public ActionResult SweetAlert()
        {
            return View();
        }
    }
}