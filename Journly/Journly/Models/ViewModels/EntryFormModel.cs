using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journly.Models.ViewModels
{
    public class EntryFormModel
    {

        public EntryFormModel()
        {
            entry = new JournalEntry();
            journal = new Journal();
        }

        public JournalEntry entry { get; set; }
        public Journal journal { get; set; }
    }
}