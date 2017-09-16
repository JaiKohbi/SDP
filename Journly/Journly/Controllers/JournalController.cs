using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        [HttpPost]
        public ActionResult Create(String title)
        {
            //Journal jrn = new Journal {Title = title, CreationDate = DateTime.Now, UserId = Membership.GetUser().UserName}; //cannot pull userId from current user.
            return View();
        }
    }
}