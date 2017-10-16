using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journly.Models.ViewModels
{
    public class SearchFormModel
    {
        public SearchFormModel()
        {
            Results = new List<JournalEntry>();
            ShowHidden = true;
            ShowDeleted = true;
        }

        public String JournalTitle { get; set; }
        public String SearchString { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int JournalId { get; set; }
        public bool ShowHidden { get; set; }
        public bool ShowDeleted { get; set; }
        public List<JournalEntry> Results { get; set; }

    }
}