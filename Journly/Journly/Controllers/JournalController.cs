using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Journly.Models;
using Journly.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace Journly.Controllers
{
    public class JournalController : Controller
    {
        private ApplicationDbContext _context;

        public JournalController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Journal
        public ActionResult ViewJournals()
        {
            var jrnls = _context.Journals.ToList();
            var userJournals = new List<Journal>();

            foreach (Journal jrn in jrnls)
            {
                if (jrn.UserName.Equals(User.Identity.Name))
                {
                    userJournals.Add(jrn);
                }
            }

            return View(userJournals);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(String title)
        {
            Journal jrn = new Journal { Title = title,
                                        CreationDate = DateTime.Now,
                                        UserName = User.Identity.Name};
            _context.Journals.Add(jrn);
            _context.SaveChanges();

            return RedirectToAction("ViewJournals", "Journal");
        }

        public ActionResult Details(int? id)
        {
            var entries = _context.JournalEntries.ToList();
            var data = new JournalDetails();
            data.journal = _context.Journals.SingleOrDefault(j => j.Id == id);

            foreach (JournalEntry jrnEntry in entries)
            {
                if (jrnEntry.JournalId == id)
                {
                    data.entries.Add(jrnEntry);
                }
            }

            return View(data);
        }
    }
}