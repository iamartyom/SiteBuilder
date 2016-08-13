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
        public ActionResult Info(string user)
        {
            if (db.Users.Any(c => c.UserName == user))
            {
                var model = new UserInfo
                {
                    Profile = db.Users.Where(c => c.UserName == user).First(),
                    Sites = db.Sites.OrderByDescending(c => c.Id).Select(c => new ShowSite { NameSite = c.Name, NameUser = c.User.UserName, Description = c.Description, NamePage = c.Pages.FirstOrDefault().Name }).ToList()
            };

                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}