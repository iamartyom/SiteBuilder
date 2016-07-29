using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}