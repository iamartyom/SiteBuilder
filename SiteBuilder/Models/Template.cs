using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int countBlocks { get; set; }

        public virtual ICollection<Page> Pages { get; set; }
    }
}