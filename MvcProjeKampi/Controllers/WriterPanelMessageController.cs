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

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator validator = new MessageValidator();
        public ActionResult Inbox(string p)
        {
            p = (string)Session["WriterMail"];
            var messageList = mm.GetListInbox(p);
            return View(messageList);
        }
        public ActionResult Sendbox(string p)
        {
            p = (string)Session["WriterMail"];
            var messageList = mm.GetListSendbox(p);
            return View(messageList);
        }
        public PartialViewResult MessageListMenu(string p)
        {
            p = (string)Session["WriterMail"];
            var messageInboxValues = mm.GetListInbox(p);
            var messageSendboxValues = mm.GetListSendbox(p);
            var messageDraftValues = mm.GetListDraft(p);
            var messageTrashValues = mm.GetListTrash(p);
            ViewBag.MessageInboxCount = messageInboxValues.Count();
            ViewBag.MessageSendboxCount = messageSendboxValues.Count();
            ViewBag.messageDraftCount = messageDraftValues.Count();
            ViewBag.messageTrashCount = messageTrashValues.Count();
            return PartialView();
        }
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            if (values.IsRead == false)
            {
                values.IsRead = true;
            }
            mm.MessageUpdate(values);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
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
            string sender = (string)Session["WriterMail"];
            ValidationResult results = validator.Validate(p);
            if (button == "sendmessage")
            {
                if (results.IsValid)
                {
                    p.SenderMail = sender;
                    p.IsDraft = false;
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAdd(p);
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
                    mm.MessageAdd(p);
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
        public ActionResult Trash(string p)
        {
            p = (string)Session["WriterMail"];
            var messageList = mm.GetListTrash(p);
            return View(messageList);
        }
        public ActionResult Draft(string p)
        {
            p = (string)Session["WriterMail"];
            var messageList = mm.GetListDraft(p);
            return View(messageList);
        }
        public ActionResult IsRead(int id)
        {
            var result = mm.GetByID(id);
            if (result.IsRead == false)
            {
                result.IsRead = true;
            }
            else
            {
                result.IsRead = false;
            }
            mm.MessageUpdate(result);
            return RedirectToAction("Inbox");
        }
        public ActionResult MoveToTrash(int id)
        {
            var values = mm.GetByID(id);
            values.Trash = true;
            mm.MessageUpdate(values);
            return RedirectToAction("Inbox");
        }
        public ActionResult RestoreMessage(int id)
        {
            var values = mm.GetByID(id);
            values.Trash = false;
            mm.MessageUpdate(values);
            return RedirectToAction("Trash");
        }
        public ActionResult DeleteMessage(int id)
        {
            var values = mm.GetByID(id);
            mm.MessageDelete(values);
            return RedirectToAction("Trash");
        }
    }
}