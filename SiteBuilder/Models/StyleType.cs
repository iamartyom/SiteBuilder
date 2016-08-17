using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class StyleType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Site> Site { get; set; }
    }
}