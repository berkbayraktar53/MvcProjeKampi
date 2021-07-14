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
    public class MessageController : Controller
    {
        MessageManager msm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox(string p)
        {
            p = (string)Session["AdminUserName"];
            var messageList = msm.GetListInbox(p);
            return View(messageList);
        }
        public ActionResult Sendbox(string p)
        {
            p = (string)Session["AdminUserName"];
            var messageList = msm.GetListSendbox(p);
            return View(messageList);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = msm.GetByID(id);
            if (values.IsRead == false)
            {
                values.IsRead = true;
            }
            msm.MessageUpdate(values);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = msm.GetByID(id);
            if (values.IsRead == false)
            {
                values.IsRead = true;
            }
            msm.MessageUpdate(values);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p, string button)
        {
            string sender = (string)Session["AdminUserName"];
            ValidationResult results = messageValidator.Validate(p);
            if (button == "sendmessage")
            {
                if (results.IsValid)
                {
                    p.SenderMail = sender;
                    p.IsDraft = false;
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    msm.MessageAdd(p);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "draft")
            {
                if (results.IsValid)
                {
                    p.SenderMail = sender;
                    p.IsDraft = true;
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    msm.MessageAdd(p);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }
        public ActionResult DeleteMessage(int id)
        {
            var values = msm.GetByID(id);
            msm.MessageDelete(values);
            return RedirectToAction("Trash");
        }
        public ActionResult MoveToTrash(int id)
        {
            var values = msm.GetByID(id);
            values.Trash = true;
            msm.MessageUpdate(values);
            return RedirectToAction("Inbox");
        }
        public ActionResult RestoreMessage(int id)
        {
            var values = msm.GetByID(id);
            values.Trash = false;
            msm.MessageUpdate(values);
            return RedirectToAction("Trash");
        }
        public ActionResult Trash(string p)
        {
            var messageList = msm.GetListTrash(p);
            return View(messageList);
        }
        public ActionResult Draft(string p)
        {
            var messageList = msm.GetListDraft(p);
            return View(messageList);
        }
        public ActionResult IsRead(int id)
        {
            var result = msm.GetByID(id);
            if (result.IsRead == false)
            {
                result.IsRead = true;
            }
            else
            {
                result.IsRead = false;
            }
            msm.MessageUpdate(result);
            return RedirectToAction("Inbox");
        }
    }
}