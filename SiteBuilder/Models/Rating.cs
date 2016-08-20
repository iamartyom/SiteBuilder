using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string UserId { get; set; }
        public bool Like { get; set; }

        public virtual Site Site { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}