using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactValues = cm.GetList();
            return View(contactValues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactValues = cm.GetByID(id);
            return View(contactValues);
        }
        public PartialViewResult MessageListMenu(string p)
        {
            p = (string)Session["AdminUserName"];
            var contactValues = cm.GetList();
            var messageInboxValues = mm.GetListInbox(p);
            var messageSendboxValues = mm.GetListSendbox(p);
            var messageDraftValues = mm.GetListDraft(p);
            var messageTrashValues = mm.GetListTrash(p);
            ViewBag.ContactCount = contactValues.Count();
            ViewBag.MessageInboxCount = messageInboxValues.Count();
            ViewBag.MessageSendboxCount = messageSendboxValues.Count();
            ViewBag.messageDraftCount = messageDraftValues.Count();
            ViewBag.messageTrashCount = messageTrashValues.Count();
            return PartialView();
        }
    }
}