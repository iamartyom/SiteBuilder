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
            ViewBag.Tags = db.Tags.Select(c => c.Name);
            ViewBag.UserId = UserId();
            ViewBag.TypeMenus = db.TypeMenus.Select(c => c).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateSite(AddSite site)
        {
            site.UserId = UserId();

            if (ModelState.IsValid)
            {
                Site newSite = new Site()
                {
                    UserId = site.UserId,
                    Name = site.Name,
                    Description = site.Description,
                    TypeMenuId =  site.TypeMenuId,
                    TagSites = new List<TagSite>(),
                };

                var listTags = site.Tags.ToString().Split(',');

                foreach(var tag in listTags)
                {
                    if (db.Tags.Any(c => c.Name == tag))
                    {
                        newSite.TagSites.Add(new TagSite { SiteId = newSite.Id, TagId = db.Tags.Where(c => c.Name == tag).FirstOrDefault().Id });
                    }
                };

                if (newSite.TagSites.Count() < 1)
                {
                    return CreateSite();
                }

                db.Sites.Add(newSite);
                db.SaveChanges();

                return RedirectToAction("CreatePage", "SiteBuilder", new { parameter1 = newSite.Id });
            }
            else
            {
                return CreateSite();
            }
        }

        [HttpGet]
        public ActionResult CreatePage(int parameter1)
        {
            ViewBag.UserId = UserId();
            ViewBag.SiteId = parameter1;
            var siteData = db.Sites.Where(c => c.Id == parameter1).Select(c => c).FirstOrDefault();
            string siteName = siteData.Name.ToString();
            ViewBag.SiteName = siteName;
            ViewBag.Pages = db.Pages.Select(c => c).Where(c => c.SiteId == parameter1).OrderBy(c => c.PageNumber).ToList();
            ViewBag.Templates = db.Templates.Select(c => c).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreatePage(Page page)
        {
            return CreatePage(page.SiteId);
        }

        public ActionResult Show (string parameter1, string nameSite, string page)
        {
            try
            {
                var siteList = db.Users.Where(c => c.UserName == parameter1).Select(c => c.Sites).FirstOrDefault().ToList();
                var pageList = siteList.Where(c => c.Name == nameSite).Select(c => c.Pages).FirstOrDefault().OrderBy(c => c.PageNumber).ToList();
                var contentList = pageList.Where(c => c.Name == page).Select(c => c.Contents).FirstOrDefault().OrderBy(c => c.Position).ToList();                
                var navbarType = db.Sites.Where(c => c.Name == nameSite).Select(c => c.TypeMenuId).FirstOrDefault();

                ViewBag.pages = pageList;
                ViewBag.contentList = contentList;
                ViewBag.user = parameter1;
                ViewBag.nameSite = nameSite;
                ViewBag.navbarType = navbarType;

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

        [HttpPost]
        public string SavePageNumber()
        {
            var json = Request.Form["PageValues"];
            int siteId = Convert.ToInt32(Request.Form["SiteId"]); 

            dynamic pageNumbers = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            var pages = db.Pages.Where(c => c.SiteId == siteId).ToList();
            for (int i = 0; i < pages.Count(); i++)
            {
                pages[i].PageNumber = (int)pageNumbers[i].id;
            }
            db.SaveChanges();
            return "SavePageNumber";
        }

        public string DeleteSite(string nameSite)
        {
            int idSite = db.Sites.Where(c => c.Name == nameSite).First().Id;

            Site record = db.Sites.First(c => c.Id == idSite);

            db.Sites.Remove(record);
            db.SaveChanges();

            return "Site is deleted.";
        }
    }
}