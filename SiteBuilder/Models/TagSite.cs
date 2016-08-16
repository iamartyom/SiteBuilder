using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class TagSite
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int TagId { get; set; }

        public virtual Site Site { get; set; }
        public virtual Tag Tag { get; set; }
    }
}