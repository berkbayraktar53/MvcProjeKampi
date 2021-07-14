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
    public class SkillController : Controller
    {
        SkillManager sm = new SkillManager(new EfSkillDal());
        public ActionResult Index()
        {
            var values = sm.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSkill(Skill p)
        {
            SkillValidator skillValidator = new SkillValidator();
            ValidationResult results = skillValidator.Validate(p);
            if (results.IsValid)
            {
                sm.SkillAdd(p);
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
        public ActionResult EditSkill(int id)
        {
            var values = sm.GetByID(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditSkill(Skill skill)
        {
            SkillValidator skillValidator = new SkillValidator();
            ValidationResult results = skillValidator.Validate(skill);
            if (results.IsValid)
            {
                sm.SkillUpdate(skill);
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
        public ActionResult DeleteSkill(int id)
        {
            var value = sm.GetByID(id);
            sm.SkillDelete(value);
            return RedirectToAction("Index");
        }
    }
}