using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Page
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int TemplateId { get; set; }
        public int PageNumber { get; set; }
        public string Name { get; set; }

        public virtual Site Site { get; set; }
        public virtual Template Template { get; set; }
        public virtual IList<Content> Contents { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}