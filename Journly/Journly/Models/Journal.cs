using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Journly.Models
{
    public class Journal
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public String Title { get; set; }

        public DateTime CreationDate { get; set; }
    }
}