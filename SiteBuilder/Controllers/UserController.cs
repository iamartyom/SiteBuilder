using SiteBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteBuilder.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Info(string parameter1)
        {
            if (db.Users.Any(c => c.UserName == parameter1))
            {
                var model = new UserInfo
                {
                    Profile = db.Users.Where(c => c.UserName == parameter1).FirstOrDefault(),
                    Sites = db.Sites.OrderByDescending(c => c.Id).Select(c => new ShowSite { NameSite = c.Name, NameUser = c.User.UserName, Description = c.Description, NamePage = c.Pages.FirstOrDefault().Name, TagSite = c.TagSites }).Where(c => c.NameUser == parameter1).ToList(),
            };

                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult LoadEditPanel(string nameSite, string user)
        {
            ViewBag.site = db.Sites.Where(c => c.Name == nameSite).First().Id;

            return PartialView("EditPanelProfile");
        }
    }
}