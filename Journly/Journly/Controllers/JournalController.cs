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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Journal journal)
        {
            if (journal.Title == null)
            {
                return View("New");
            }

            Journal jrn = new Journal { Title = journal.Title,
                                        CreationDate = DateTime.Now,
                                        UserName = User.Identity.Name};
            
            _context.Journals.Add(jrn);
            _context.SaveChanges();

            return RedirectToAction("ViewJournals", "Journal");
        }

        public ActionResult Details(int? id, bool? showHidden, bool? showDeleted)
        {
            var entries = _context.JournalEntries.ToList();
            var data = new JournalDetails();
            data.Journal = _context.Journals.SingleOrDefault(j => j.Id == id);

            if (data.Journal == null)
            {
                return HttpNotFound();
            }

            foreach (JournalEntry jrnEntry in entries)
            {
                if (jrnEntry.JournalId == id && jrnEntry.Flag == JournalEntry.EntryFlag.N)
                {
                    data.Entries.Add(jrnEntry);
                }
                if (jrnEntry.JournalId == id && showHidden == true && jrnEntry.Flag == JournalEntry.EntryFlag.H)
                {
                    data.ShowHidden = true;
                    data.Entries.Add(jrnEntry);
                }
                if (jrnEntry.JournalId == id && showDeleted == true && jrnEntry.Flag == JournalEntry.EntryFlag.D)
                {
                    data.ShowDeleted = true;
                    data.Entries.Add(jrnEntry);
                }
            }

            return View(data);
        }
    }
}