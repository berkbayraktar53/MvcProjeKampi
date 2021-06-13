using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class StatisticController : Controller
    {
        StatisticManager sm = new StatisticManager(new EfCategoryDal(), new EfHeadingDal(), new EfWriterDal());
        public ActionResult Index()
        {
            var categoryList = sm.GetCategoryList();
            var headingList = sm.GetHeadingList();
            var writerList = sm.GetWriterList();

            // 1- Toplam kategori sayısı
            ViewBag.TotalCategory = categoryList.Count();

            // 2- Başlık tablosunda "yazılım" kategorisine ait başlık sayısı
            ViewBag.SoftwareCategory = headingList.Count(x => x.CategoryID == 4);

            // 3- Yazar adında 'a' harfi geçen yazar sayısı
            ViewBag.WriterName = writerList.Count(x => x.WriterName.Contains("A") || x.WriterName.Contains("a"));

            // 4- En fazla başlığa sahip kategori adı
            var categoryName = categoryList.Where(x => x.CategoryID == headingList.Max(y => y.CategoryID)).Select(x => x.CategoryName).ToList();
            ViewBag.CategoryName = categoryName[0].ToString();

            // 5- Kategori tablosunda durumu true olan ile false olan kategoriler arasındaki sayısal fark
            ViewBag.Difference = categoryList.Count(x => x.CategoryStatus == true) - categoryList.Count(x => x.CategoryStatus == false);
            
            return View();
        }
    }
}