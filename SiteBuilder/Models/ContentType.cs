using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class ContentType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Content> Contents { get; set; } 
    }
}