﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Journly.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] //allows anon users to access this page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Journly - An Overview";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to contact us";

            return View();
        }

        public ActionResult FAQs()
        {
            ViewBag.Message = "Getting Started";

            return View();
        }
    }
}