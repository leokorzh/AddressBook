﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;
using WebMatrix.WebData;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        private DBManage _db;

        public HomeController()
        {
            _db = new DBManage();
        }

        public ActionResult Index()
        {
            return View(_db.GetAllContacts());
        }

        public ActionResult Details(int id)
        {
            return View(_db.GetContact(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TypeList = from c in _db.GetTypeList()
                               select new SelectListItem
                               {
                                   Value = c.TypeId.ToString(),
                                   Text = c.Name
                               };
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contacts newContact)
        {
            newContact.UserId = WebSecurity.CurrentUserId;
            TempData["ChangeDBInfo"] = _db.AddContact(newContact);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            TempData["ChangeDBInfo"] = _db.DeleteContact(_db.GetContact(id));
            return RedirectToAction("Index");
        }
    }
}
