using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SiteBuilder.Models
{
    public class SBContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<TypeMenu> TypeMenus { get; set; }
    }
}