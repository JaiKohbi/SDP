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
        }

        public String SearchString { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int JournalId { get; set; }
        public List<JournalEntry> Results { get; set; }

    }
}