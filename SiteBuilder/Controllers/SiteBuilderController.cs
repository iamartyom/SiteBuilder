using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SiteBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteBuilder.Controllers
{
    [Authorize]
    public class SiteBuilderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: SiteBuilder
        [HttpGet]
        public ActionResult CreateSite()
        {
            ViewBag.TypeMenus = db.TypeMenus.Select(c => c).ToList();

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult CreateSite(Site site)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            site.UserId = user.Id;

            db.Sites.Add(site);
            db.SaveChanges();

            return RedirectToAction("CreatePage", "SiteBuilder", new { id = site.Id });
        }

        [HttpGet]
        public ActionResult CreatePage(int id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            ViewBag.UserId = user.Id;
            ViewBag.SiteId = id;
            ViewBag.Pages = db.Pages.Select(c => c).Where(c => c.SiteId == id).ToList();

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult CreatePage(Page page)
        {
            db.Pages.Add(page);
            db.SaveChanges();

            return RedirectToAction("CreatePage", "SiteBuilder", new { id = page.SiteId });
        }

        public ActionResult Show (string user, string nameSite)
        {
            int a = Int32.Parse(nameSite);

            ViewBag.Pages = db.Pages.Select(c => c).Where(c => c.Id == a).ToList();

            return View();
        }
    }
}