using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {
        AdminManager cm = new AdminManager(new EfAdminDal());
        AdminValidator validator = new AdminValidator();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            ValidationResult results = validator.Validate(p);
            var adminUserInfo = cm.GetAdmin(p.AdminUserName, p.AdminPassword);
            if (adminUserInfo != null && results.IsValid)
            {
                FormsAuthentication.SetAuthCookie(adminUserInfo.AdminUserName, false);
                Session["AdminUserName"] = adminUserInfo.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                if (!results.IsValid)
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        return View();
                    }
                }
                ViewBag.Error = "Kullanıcı adı veya şifreniz hatalı";
            }
            return View();
        }
    }
}