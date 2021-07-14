using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class AuthorizationController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            var adminValues = adminManager.GetList();
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> valueRole = (from x in adminManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.AdminRole,
                                                      Value = x.AdminID.ToString()
                                                  }).ToList();
            ViewBag.vlr = valueRole;
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            AdminValidator adminValidator = new AdminValidator();
            ValidationResult results = adminValidator.Validate(p);
            if (results.IsValid)
            {
                adminManager.AdminAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> valueRole = (from x in adminManager.GetList()
                                              select new SelectListItem
                                              {
                                                  Text = x.AdminRole,
                                                  Value = x.AdminRole.ToString()
                                              }).ToList();
            ViewBag.vlr = valueRole;

            var adminValue = adminManager.GetByID(id);
            return View(adminValue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            adminManager.AdminUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAdmin(int id)
        {
            var values = adminManager.GetByID(id);
            adminManager.AdminDelete(values);
            return RedirectToAction("Index");
        }
        public ActionResult Activate(int id)
        {
            var values = adminManager.GetByID(id);
            values.AdminStatus = true;
            adminManager.AdminUpdate(values);
            return RedirectToAction("Index");
        }
        public ActionResult Deactivate(int id)
        {
            var values = adminManager.GetByID(id);
            values.AdminStatus = false;
            adminManager.AdminUpdate(values);
            return RedirectToAction("Index");
        }
        public PartialViewResult AdminName(string p)
        {
            p = (string)Session["AdminUserName"];
            ViewBag.name = p;
            return PartialView();
        }
    }
}