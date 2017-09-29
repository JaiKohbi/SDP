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

        public ActionResult SearchResults(SearchFormModel search)
        {
            search.Results = new List<JournalEntry>();

            var myJournals = (from j in _context.Journals
                              where j.UserName == User.Identity.Name
                              select j).ToList();

            var entries = new List<JournalEntry>();

            foreach (var jrn in myJournals)
            {
                var myEntries = (from e in _context.JournalEntries
                    where e.JournalId == jrn.Id
                    select e).ToList();

                foreach (var ent in myEntries)
                {
                    entries.Add(ent);
                }
            }

            if (search.SearchString != null)
            {
                search.Results = FilterByString(entries, search.SearchString);
            }
            if (search.StartDate.Date != new DateTime().Date)
            {
                search.Results = search.EndDate.Date != new DateTime().Date ? FilterByDateRange(entries, search.StartDate, search.EndDate) : FilterByDate(entries, search.StartDate);
            }
            if (search.JournalId != 0)
            {
                search.Results = FilterByJournal(entries, search.JournalId);
            }

            return View(search);
        }

        private List<JournalEntry> FilterByJournal(List<JournalEntry> entries, int journalId)
        {
            var validEntries = new List<JournalEntry>();

            foreach (var jrn in entries)
            {
                if (jrn.JournalId == journalId)
                {
                    validEntries.Add(jrn);
                }
            }

            return validEntries;
        }

        private List<JournalEntry> FilterByDate(List<JournalEntry> entries, DateTime date)
        {
            var validEntries = new List<JournalEntry>();

            foreach (var jrn in entries)
            {
                if (jrn.CreatedOn.Date == date.Date)
                {
                    validEntries.Add(jrn);
                }
            }

            return validEntries;
        }

        private List<JournalEntry> FilterByDateRange(List<JournalEntry> entries, DateTime startDate, DateTime endDate)
        {
            var validEntries = new List<JournalEntry>();

            foreach (var jrn in entries)
            {
                if (jrn.CreatedOn.Date > startDate && jrn.CreatedOn.Date < endDate.Date)
                {
                    validEntries.Add(jrn);
                }
            }

            return validEntries;
        }

        private List<JournalEntry> FilterByString(List<JournalEntry> entries, String searchString)
        {
            var validEntries = new List<JournalEntry>();

            foreach (var jrn in entries)
            {
                if (jrn.EntryBody.Contains(searchString) || jrn.Title.Contains(searchString))
                {
                    validEntries.Add(jrn);
                }
            }
            return validEntries;
        }
    }
}