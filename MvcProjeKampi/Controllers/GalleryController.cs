using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class GalleryController : Controller
    {
        ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());
        public ActionResult Index()
        {
            var values = ifm.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddImage(ImageFile p)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string path = "~/AdminLTE-3.0.4/images/" + fileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                p.ImagePath = "/AdminLTE-3.0.4/images/" + fileName;
                ifm.ImageFileAdd(p);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public PartialViewResult AddImageModal()
        {
            return PartialView();
        }
    }
}