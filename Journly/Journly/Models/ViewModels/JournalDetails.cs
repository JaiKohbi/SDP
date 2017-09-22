using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journly.Models.ViewModels
{
    public class JournalDetails
    {
        public Journal journal;
        public List<JournalEntry> entries;
    }
}