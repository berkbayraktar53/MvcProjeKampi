using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllContent(int page = 1)
        {
            var values = cm.GetList().ToPagedList(page, 5);
            return View(values);
        }
        [HttpPost]
        public ActionResult GetAllContent(string p, int page = 1)
        {
            var values = cm.GetList(p).ToPagedList(page, 5);
            return View(values);
        }
        [AllowAnonymous]
        public ActionResult ContentByHeading(int id)
        {
            var contentValues = cm.GetListByHeadingID(id);
            return View(contentValues);
        }
    }
}