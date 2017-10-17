using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            StartDate = DateTime.Today;

        }

        public String JournalTitle { get; set; }

        [Display(Name = "Search Term")]
        public String SearchString { get; set; }

        [Display(Name = "Start Date OR Single Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public int JournalId { get; set; }
        public bool ShowHidden { get; set; }
        public bool ShowDeleted { get; set; }
        public List<JournalEntry> Results { get; set; }

    }
}