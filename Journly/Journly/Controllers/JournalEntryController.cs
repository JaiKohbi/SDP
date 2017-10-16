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

        public ActionResult UpdateEntry(int id)
        {
            JournalEntry entry = _context.JournalEntries.SingleOrDefault(j => j.Id == id);

            if (entry == null)
            {
                return HttpNotFound();
            }
            //  Get the Entry and pass it to the view
            //  render it in the view
            //  allow changes to be made
            //  pass both back to Update

            var model = new EntryFormModel()
            {
                entry = entry,
                journal = _context.Journals.SingleOrDefault(j => j.Id == entry.JournalId)
            };

            return View("EntryForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EntryFormModel model)
        {
            var oldVersion = _context.JournalEntries.Single(e => e.Id == model.entry.Id);
            if (oldVersion == null)
            {
                return HttpNotFound();
            }
            oldVersion.Flag = JournalEntry.EntryFlag.E;
            var newVersion = new JournalEntry()
            {
                CreatedOn = DateTime.Now,
                EntryBody = model.entry.EntryBody,
                Flag = JournalEntry.EntryFlag.N,
                EntryEditedReason = model.entry.EntryEditedReason,
                EntryEditedId = model.entry.Id,
                JournalId = model.journal.Id,
                Title = model.entry.Title,
                Version = (model.entry.Version + 1)
            };
            _context.JournalEntries.Add(newVersion);
            _context.SaveChanges();

            return RedirectToAction("Details", "Journal", new {id = model.journal.Id});
        }

        // GET: JournalEntry
        public ActionResult New(int? id)
        {
            EntryFormModel model = new EntryFormModel();
            model.journal = _context.Journals.SingleOrDefault(j => j.Id == id);
            return View("EntryForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntryFormModel model)
        {
            if (model.entry.Title == null || model.entry.EntryBody == null)
            {
                return View("EntryForm", model);
            }

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

            var entries = new List<JournalEntry>();

            if (entry == null)
            {
                return HttpNotFound();
            }

            entries.Add(entry);

            if (entry.Version != 1)
            {
                var allEntries = _context.JournalEntries.ToList();
                for (int i = entry.Version; i >= 1; i--)
                {
                    foreach (var e in allEntries)
                    {
                        if (e.Id == entry.EntryEditedId && e.Version == i)
                        {
                            entries.Add(e);
                        }
                    }
                }
            }

            //  entry version is not 1, find other versions and pass them through too.

            return View(entries);
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

        public ActionResult SearchSelect()
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

        public ActionResult SearchResults(SearchFormModel search, int? id)
        {
            if (id != null && id != 0)
            {
                var jrn = _context.Journals.SingleOrDefault(j => j.Id == id);
                search.JournalId = (int)id;
                search.JournalTitle = jrn.Title;
            }

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
                    search.Results.Add(ent);
                }
            }

            if (search.SearchString != null)
            {
                search.Results = FilterByString(search.Results, search.SearchString);
            }
            if (search.StartDate.Date != new DateTime().Date)
            {
                search.Results = search.EndDate.Date != new DateTime().Date ? FilterByDateRange(search.Results, search.StartDate, search.EndDate) : FilterByDate(search.Results, search.StartDate);
            }
            if (search.JournalId != 0)
            {
                search.Results = FilterByJournal(search.Results, search.JournalId);
            }

            return View(search);
        }

        public List<JournalEntry> FilterByJournal(List<JournalEntry> entries, int journalId)
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

        public List<JournalEntry> FilterByDate(List<JournalEntry> entries, DateTime date)
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

        public List<JournalEntry> FilterByDateRange(List<JournalEntry> entries, DateTime startDate, DateTime endDate)
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

        public List<JournalEntry> FilterByString(List<JournalEntry> entries, String searchString)
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