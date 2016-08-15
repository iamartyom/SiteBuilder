using SiteBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteBuilder.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Tag(string parameter1)
        {
            if (db.Tags.Any(c => c.Name == parameter1))
            {
                var idTag = db.Tags.Where(c => c.Name == parameter1).FirstOrDefault().Id;
                var sitesTag = db.TagSites.Where(c => c.TagId == idTag).Select(c => c.SiteId).ToList();
                ViewBag.sites = db.Sites.Where(c => sitesTag.Contains(c.Id)).OrderByDescending(c => c.Id).Select(c => new ShowSite { NameSite = c.Name, NameUser = c.User.UserName, NamePage = c.Pages.FirstOrDefault().Name }).ToList();
            }
            else
            {
                return HttpNotFound();
            }

            return View();
        }
    }
}