using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Journly.Models;
using Journly.Models.ViewModels;

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
        public ActionResult New(int? id)
        {
            EntryFormModel model = new EntryFormModel();
            model.journal = _context.Journals.SingleOrDefault(j => j.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EntryFormModel model)
        {
            model.entry.JournalId = model.journal.Id;
            model.entry.CreatedOn = DateTime.Now;
            model.entry.Version = 1;
            model.entry.EntryEditedId = 0;
            model.entry.EntryEditedReason = null;
            model.entry.Flag = JournalEntry.EntryFlag.N;

            _context.JournalEntries.Add(model.entry);
            _context.SaveChanges();
            
            return RedirectToAction("Details", "Journal", new {Id = model.journal.Id});
        }

        public ActionResult Details(int id)
        {
            JournalEntry entry = _context.JournalEntries.SingleOrDefault(e => e.Id == id);

            if (entry == null)
            {
                return HttpNotFound();
            }

            return View(entry);
        }

        public ActionResult FlagHidden(int id)
        {
            var entryInDb = _context.JournalEntries.Single(e => e.Id == id);

            if (entryInDb != null)
            {
                entryInDb.Flag = JournalEntry.EntryFlag.H;

                _context.SaveChanges();

                return RedirectToAction("Details", "Journal", new { Id = entryInDb.JournalId });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult FlagDeleted(int id)
        {
            var entryInDb = _context.JournalEntries.Single(e => e.Id == id);

            if (entryInDb != null)
            {
                entryInDb.Flag = JournalEntry.EntryFlag.D;

                _context.SaveChanges();

                return RedirectToAction("Details", "Journal", new { Id = entryInDb.JournalId });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult FlagNormal(int id)
        {
            var entryInDb = _context.JournalEntries.Single(e => e.Id == id);

            if (entryInDb != null && entryInDb.Flag == JournalEntry.EntryFlag.H)
            {
                entryInDb.Flag = JournalEntry.EntryFlag.N;

                _context.SaveChanges();

                return RedirectToAction("Details", "Journal", new { Id = entryInDb.JournalId });
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}