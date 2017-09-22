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
            journal = new Journal();
            entries = new List<JournalEntry>();
        }
        public Journal journal;
        public List<JournalEntry> entries;
    }
}