using SiteBuilder.Filters;
using SiteBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteBuilder.Controllers
{
    [Culture]
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

                var userId = db.Users.First(c => c.UserName == parameter1).Id;

                AchievementCheck(userId);
                AchievementShow(userId);

                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public ActionResult LoadEditPanel(string nameSite, string user)
        {
            ViewBag.site = db.Sites.Where(c => c.Name == nameSite && c.User.UserName == user).First().Id;

            return PartialView("EditPanelProfile");
        }

        public void AchievementShow(string userId)
        {
            ApplicationDbContext db2 = new ApplicationDbContext();
            ViewBag.achievements = db2.Achievements.Where(c => c.UserId == userId).ToList();
        }

        public void AchievementCheck(string userId)
        {
            var userAchievements = db.Achievements.Where(c => c.UserId == userId).ToList();

            if (userAchievements.FirstOrDefault(c => c.AchievementTypeId == 1) == default(Achievement))
            {
                if (db.Comments.FirstOrDefault(c => c.UserId == userId) != default(Comment))
                {
                        Achievement achievement = new Achievement()
                        {
                            UserId = userId,
                            AchievementTypeId = 1,
                        };

                        db.Achievements.Add(achievement);
                        db.SaveChanges();
                }
            }

            if (userAchievements.FirstOrDefault(c => c.AchievementTypeId == 2) == default(Achievement))
            {
                if (db.Ratings.FirstOrDefault(c => c.UserId == userId) != default(Rating))
                {
                    Achievement achievement = new Achievement()
                    {
                        UserId = userId,
                        AchievementTypeId = 2,
                    };

                    db.Achievements.Add(achievement);
                    db.SaveChanges();
                }
            }
        }
    }
}