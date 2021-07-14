using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class CalendarController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Calendar());
        }

        public JsonResult GetEvents(DateTime Start, DateTime End)
        {
            var viewModel = new Calendar();
            var events = new List<Calendar>();
            Start = DateTime.Today.AddDays(-14);
            End = DateTime.Today.AddDays(-14);
            foreach (var item in hm.GetList())
            {
                events.Add(new Calendar()
                {
                    Title = item.HeadingName,
                    Start = item.HeadingDate,
                    End = item.HeadingDate.AddDays(-14),
                    AllDay = false
                });
                Start = Start.AddDays(7);
                End = End.AddDays(7);
            }
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}