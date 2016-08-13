using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class UserInfo
    {
        public ApplicationUser Profile { get; set; }
        public IEnumerable<ShowSite> Sites { get; set; }
    }
}