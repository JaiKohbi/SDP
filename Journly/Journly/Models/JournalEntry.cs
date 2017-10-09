using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Journly.Models
{
    public class JournalEntry
    {
        public enum EntryFlag
        {
            N, H, D, E
        }

        public int Id { get; set; }

        public int JournalId { get; set; }

        [Required]
        public String Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Version { get; set; }

        [Required]
        public String EntryBody { get; set; }

        public EntryFlag Flag { get; set; }

        public String EntryEditedReason { get; set; }

        public int EntryEditedId { get; set; }
    }
}