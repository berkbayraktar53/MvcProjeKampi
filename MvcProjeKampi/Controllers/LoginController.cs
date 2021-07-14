using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager cm = new AdminManager(new EfAdminDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterLoginManager wlm = new WriterLoginManager(new EfWriterDal());
        AdminValidator validator = new AdminValidator();
        WriterValidator writerValidator = new WriterValidator();
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
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            ValidationResult results = writerValidator.Validate(p);
            var writerUserInfo = wlm.GetWriter(p.WriterMail, p.WriterPassword);
            if (writerUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(writerUserInfo.WriterMail, false);
                Session["WriterMail"] = writerUserInfo.WriterMail;
                Session["WriterName"] = writerUserInfo.WriterName;
                Session["WriterSurName"] = writerUserInfo.WriterSurName;
                Session["WriterImage"] = writerUserInfo.WriterImage;
                return RedirectToAction("WriterProfile", "WriterPanel");
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
                ViewBag.Error = "Mail adresiniz veya şifreniz hatalı";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Writer writer)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string path = "~/AdminLTE-3.0.4/images/writer_images/" + fileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                writer.WriterImage = "/AdminLTE-3.0.4/images/writer_images/" + fileName;
                writer.WriterStatus = true;
                wm.WriterAdd(writer);
                return RedirectToAction("WriterLogin");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult LogOutWriter()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("WriterLogin", "Login");
        }
    }
}