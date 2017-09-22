using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Journly.Models;

namespace Journly.Controllers
{
    public class JournalEntryController : Controller
    {
        private ApplicationDbContext _context;

        public JournalEntryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: JournalEntry
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(JournalEntry entry, int id)
        {
            entry.JournalId = id;
            entry.CreatedOn = DateTime.Now;
            entry.Version = 1;
            entry.EntryEditedId = 0;
            entry.EntryEditedReason = null;
            entry.Flag = JournalEntry.EntryFlag.N;

            _context.JournalEntries.Add(entry);
            _context.SaveChanges();
            
            return RedirectToAction("Details", "Journal", new {Id = id});
        }
    }
}