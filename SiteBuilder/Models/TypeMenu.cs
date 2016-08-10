using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class TypeMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}