using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journly.Models.ViewModels
{
    public class JournalDetails
    {
        public JournalDetails()
        {
            Journal = new Journal();
            Entries = new List<JournalEntry>();
        }
        public Journal Journal;
        public List<JournalEntry> Entries;
        public bool ShowHidden { get; set; }
        public bool ShowDeleted { get; set; }
    }
}