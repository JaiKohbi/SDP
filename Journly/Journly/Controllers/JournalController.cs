using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Journly.Models;

namespace Journly.Controllers
{
    public class JournalController : Controller
    {
        // GET: Journal
        public ActionResult ViewJournals()
        {
            var Journal = new Journal() {Title = "My First Journal"};
            return View(Journal);
        }

        public ActionResult New()
        {
            return View();
        }
    }
}