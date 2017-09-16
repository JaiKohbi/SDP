using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Journly.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String Title { get; set; }
        public DateTime CreationDate { get; set; }
    }
}