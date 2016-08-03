using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Page
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
    }
}