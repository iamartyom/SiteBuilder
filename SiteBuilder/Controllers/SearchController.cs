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
                return RedirectToAction("NotFound", "Error");
            }

            return View();
        }

        public ActionResult ToPagebyId(int parameter1)
        {
            var page = db.Pages.FirstOrDefault(c => c.Id == parameter1);
            return RedirectToAction("Show", "SiteBuilder", new { parameter1 = page.Site.User.UserName, nameSite = page.Site.Name, page = page.Name });
        }

        public ActionResult CommentToPage(int parameter1)
        {
            var pageId = db.Comments.FirstOrDefault(c => c.Id == parameter1).PageId;
            return RedirectToAction("ToPagebyId", "Search", new { parameter1 = pageId });
        }

        public ActionResult MarkdownToPage (int parameter1)
        {
            var pageId = db.Contents.FirstOrDefault(c => c.Id == parameter1).PageId;
            return RedirectToAction("ToPagebyId", "Search", new { parameter1 = pageId });
        }

        public ActionResult SiteToPage(int parameter1)
        {
            var page = db.Sites.FirstOrDefault( c=> c.Id == parameter1).Pages.FirstOrDefault();
            if (page == default(Page))
            {
                return RedirectToAction("CreatePage", "SiteBuilder", new { parameter1 = parameter1 });
            }
            else
            {
                return RedirectToAction("ToPagebyId", "Search", new { parameter1 = page.Id });
            }            
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
            SearchPage.AddUpdateLuceneIndex(db.Pages.ToList());
            SearchSites.AddUpdateLuceneIndex(db.Sites.ToList());
            SearchMarkdown.AddUpdateLuceneIndex(db.Contents.Where(c => c.ContentType.Name == "Markdown").ToList());

            List <Tag> _searchResultsTags;
            _searchResultsTags = SearchTags.Search(searchTerm).ToList();

            List<Comment> _searchResultsComments;
            _searchResultsComments = SearchComments.Search(searchTerm).ToList();

            List<Page> _searchResultsPages;
            _searchResultsPages = SearchPage.Search(searchTerm).ToList();

            List<Site> _searchResultsCites;
            _searchResultsCites = SearchSites.Search(searchTerm).ToList();

            List<Content> _searchResultsMarkdown;
            _searchResultsMarkdown = SearchMarkdown.Search(searchTerm).ToList();


            //if (string.IsNullOrEmpty(searchTerm) && !_searchResultsTags.Any())
            //    _searchResultsTags = SearchTags.GetAllIndexRecords().ToList();

            //ViewBag.aIRP = SearchMarkdown.GetAllIndexRecords().ToList();
            //ViewBag.aIRC = SearchComments.GetAllIndexRecords().ToList();


            ViewBag.searchResultsCites = _searchResultsCites;
            ViewBag.searchResultsPages = _searchResultsPages;
            ViewBag.searchResultsTags = _searchResultsTags;
            ViewBag.searchResultsComments = _searchResultsComments;
            ViewBag.searchResultsMarkdown = _searchResultsMarkdown;

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