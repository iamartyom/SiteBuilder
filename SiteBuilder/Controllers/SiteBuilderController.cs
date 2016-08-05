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
            ViewBag.UserId = UserId();
            ViewBag.TypeMenus = db.TypeMenus.Select(c => c).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateSite(Site site)
        {
            ViewBag.TypeMenus = db.TypeMenus.Select(c => c).ToList();

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            site.UserId = user.Id;

            if (ModelState.IsValid)
            {
                db.Sites.Add(site);
                db.SaveChanges();

                return RedirectToAction("CreatePage", "SiteBuilder", new { id = site.Id });
            }
            else
            {
                return CreateSite();
            }
        }

        [HttpGet]
        public ActionResult CreatePage(int id)
        {
            ViewBag.UserId = UserId();
            ViewBag.SiteId = id;
            ViewBag.Pages = db.Pages.Select(c => c).Where(c => c.SiteId == id).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreatePage(Page page)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Add(page);
                db.SaveChanges();

                return RedirectToAction("CreatePage", "SiteBuilder", new { id = page.SiteId });
            }
            else
            {
                return CreatePage(page.SiteId);
            }
        }

        public ActionResult Show (string user, string nameSite)
        {
            int a = Int32.Parse(nameSite);

            ViewBag.Pages = db.Pages.Select(c => c).Where(c => c.Id == a).ToList();

            return View();
        }

        public string UserId()
        {
            ApplicationUser User = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return User.Id;
        }

        [HttpPost]
        public ActionResult LoadTemplate(string nameTemplate)
        {
            return PartialView("Template/" + nameTemplate);
        }
    }
}