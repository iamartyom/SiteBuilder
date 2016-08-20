using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using SiteBuilder.Lucene;
using SiteBuilder.Models;

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

        public ActionResult CommentToPage(int parameter1)
        {
            var userName = db.Comments.FirstOrDefault(c => c.Id == parameter1).User.UserName;
            var pageId = db.Comments.FirstOrDefault(c => c.Id == parameter1).PageId;
            var siteName = db.Pages.FirstOrDefault(c => c.Id == pageId).Site.Name;
            var pageName = db.Pages.FirstOrDefault(c => c.Id == pageId).Name;


            return RedirectToAction("Show", "SiteBuilder", new { parameter1 = userName, nameSite = siteName, page = pageName });
        }

        public ActionResult Index(string parameter1)
        {
            var searchTerm = parameter1;
            if (!Directory.Exists(LuceneSearch._luceneDir))
            {
                Directory.CreateDirectory(LuceneSearch._luceneDir);
            }

            SearchTags.AddUpdateLuceneIndex(db.Tags.ToList());
            SearchComments.AddUpdateLuceneIndex(db.Comments.ToList());

            List<Tag> _searchResultsTags;
            _searchResultsTags = SearchTags.Search(searchTerm).ToList();

            List<Comment> _searchResultsComments;
            _searchResultsComments = SearchComments.Search(searchTerm).ToList();

            if (string.IsNullOrEmpty(searchTerm) && !_searchResultsTags.Any())
                _searchResultsTags = SearchTags.GetAllIndexRecords().ToList();

            
            //var limitDb = 50;

            //List<Tag> allTags;

            //if (limitDb > 0)
            //{
            //    allTags = db.Tags.Take(limitDb).ToList();
            //    ViewBag.Limit = db.Tags.ToList().Count - limitDb;
            //}
            //else allTags = db.Tags.ToList();

            //ViewBag.allTags = allTags;
            ViewBag.searchResultsTags = _searchResultsTags;
            ViewBag.searchResultsComments = _searchResultsComments;

            return View();
        }

        [HttpGet]
        public ActionResult Search(string searchTerm)
        { 
            return RedirectToAction("Index", "Search", new { parameter1 = searchTerm});
        }
        public ActionResult CreateIndex()
        {
            SearchTags.AddUpdateLuceneIndex(db.Tags.ToList());
            TempData["Result"] = "Search index was created successfully!";
            return RedirectToAction("Index");
        }
    }
}