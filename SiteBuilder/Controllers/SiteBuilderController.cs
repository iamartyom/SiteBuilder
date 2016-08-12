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
            var siteData = db.Sites.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
            string siteName = siteData.Name.ToString();
            ViewBag.SiteName = siteName;
            ViewBag.Pages = db.Pages.Select(c => c).Where(c => c.SiteId == id).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreatePage(Page page)
        {
            return CreatePage(page.SiteId);
        }

        public ActionResult Show (string user, string nameSite, string page)
        {
            try
            {
                var siteList = db.Users.Where(c => c.UserName == user).Select(c => c.Sites).FirstOrDefault().ToList();
                var pageList = siteList.Where(c => c.Name == nameSite).Select(c => c.Pages).FirstOrDefault().ToList();
                var contentList = pageList.Where(c => c.Name == page).Select(c => c.Contents).FirstOrDefault().ToList();

                ViewBag.pages = pageList;
                ViewBag.contentList = contentList;
                ViewBag.user = user;
                ViewBag.nameSite = nameSite;

                ViewBag.templateData = pageList.Where(c => c.Name == page).Select(c => c.Template).FirstOrDefault();
            }
            catch (System.ArgumentNullException)
            {
                return HttpNotFound();
            }
            
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

        [HttpPost]
        public int SavePage(Page page)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Add(page);
                db.SaveChanges();

                return page.Id;
            }

            return -1;
        }

        [HttpPost]
        public void SaveData(Content content)
        {
            db.Contents.Add(content);
            db.SaveChanges();
        }
    }
}