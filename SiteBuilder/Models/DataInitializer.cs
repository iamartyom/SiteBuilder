using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class DataInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.TypeMenus.Add(new TypeMenu() { Name = "horizontal" });
            context.TypeMenus.Add(new TypeMenu() { Name = "horizontal" });
            context.TypeMenus.Add(new TypeMenu() { Name = "horizontal" });

            base.Seed(context);
        }
    }
}