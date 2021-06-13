﻿using BusinessLayer.Concrete;
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
        public PartialViewResult MessageListMenu()
        {
            var contactValues = cm.GetList();
            var messageInboxValues = mm.GetListInbox();
            var messageSendboxValues = mm.GetListSendbox();
            var messageDraftValues = mm.GetListDraft();
            ViewBag.ContactCount = contactValues.Count();
            ViewBag.MessageInboxCount = messageInboxValues.Count();
            ViewBag.MessageSendboxCount = messageSendboxValues.Count();
            ViewBag.messageDraftValues = messageDraftValues.Count();
            return PartialView();
        }
    }
}