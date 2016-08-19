using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SiteBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ViewBag.TypeMenus = db.TypeMenus.ToList();
            ViewBag.StyleTypes = db.StyleTypes.ToList();

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
                    StyleTypeId = site.StyleTypeId,
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

                if (pageList.Count == 0)
                {
                    return RedirectToAction("CreatePage", "SiteBuilder", new { parameter1 = siteList.First(c => c.Name == nameSite).Id });
                }

                var contentList = pageList.Where(c => c.Name == page).Select(c => c.Contents).FirstOrDefault().OrderBy(c => c.Position).ToList();                
                var siteInfo = db.Sites.Where(c => c.Name == nameSite).FirstOrDefault();

                var ratingInfo = db.Ratings.Where(c => c.SiteId == siteInfo.Id).ToList();
                var rating = ratingInfo.Count(c => c.Like == true) - ratingInfo.Count(c => c.Like == false);
                var userId = UserId();
                var ratingEnabled = ratingInfo.FirstOrDefault(c => c.UserId == userId);

                ViewBag.ratingEnabled = "disabled";

                if (ratingEnabled == default(Rating))
                {
                    ViewBag.ratingEnabled = "";
                }

                ViewBag.page = page;
                ViewBag.id = db.Pages.First(c => c.Name == page).Id;
                ViewBag.pages = pageList;
                ViewBag.contentList = contentList;
                ViewBag.user = parameter1;
                ViewBag.userId = db.Users.First(c => c.UserName == parameter1).Id;
                ViewBag.nameSite = nameSite;
                ViewBag.siteInfo = siteInfo;
                ViewBag.rating = rating;

                ViewBag.templateData = pageList.Where(c => c.Name == page).Select(c => c.Template).FirstOrDefault();
            }
            catch (System.ArgumentNullException)
            {
                return HttpNotFound();
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult EditSite(int parameter1)
        {
            ViewBag.id = parameter1;
            ViewBag.typeMenus = db.TypeMenus.ToList();
            ViewBag.styleTypes = db.StyleTypes.ToList();

            Site site = db.Sites.First(c => c.Id == parameter1);

            return View(site);
        }

        public ActionResult ListPagesEdit(int parameter1)
        {
            ViewBag.pages = db.Pages.Where(c => c.SiteId == parameter1).ToList();

            return View();
        }

        public ActionResult EditPage(int parameter1)
        {
            var query = db.Pages.First(c => c.Id == parameter1);
            var nameSite = query.Site.Name;
            var nameUser = db.Sites.First(c => c.Id == query.SiteId).User.UserName;
            var namePage = query.Name;

            Show(nameUser, nameSite, namePage);

            return View();
        }

        [HttpPost]
        public ActionResult EditSite(Site site)
        {
            if (ModelState.IsValid)
            {
                db.Sites.Attach(site);
                var entry = db.Entry(site);
                entry.State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Info", "User", new { parameter1 = db.Users.First(c => c.Id == site.UserId).UserName });
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

        public void DeleteSite(int parameter1)
        {
            Site record = db.Sites.First(c => c.Id == parameter1);

            db.Sites.Remove(record);
            db.SaveChanges();

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DeletePage(int parameter1)
        {
            var idSite = db.Pages.First(c => c.Id == parameter1).SiteId;

            db.Pages.Remove(db.Pages.First(c => c.Id == parameter1));
            db.SaveChanges();

            return RedirectToAction("ListPagesEdit", "SiteBuilder", new { parameter1 = idSite });
        }

        public string UpdatePageName(int id, string name)
        {
            var page = db.Pages.First(c => c.Id == id);
            page.Name = name;
            db.SaveChanges();

            return "Success";
        }

        public string UpdateContent(int id, int position, string data, int contentTypeId)
        {
            var content = db.Contents.First(c => c.PageId == id && c.Position == position);

            content.Data = data;
            content.ContentTypeId = contentTypeId;

            db.SaveChanges();

            return "Success";
        }

        public ActionResult ShowComments(int id)
        {
            ViewBag.comments = db.Comments.Where(c => c.PageId == id).ToList();

            return PartialView("Comments");
        }

        public string SaveComment(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();

            return "Success";
        }

        public string AddRating(Rating rating)
        {
            db.Ratings.Add(rating);
            db.SaveChanges();

            return "Success";
        }
    }
}