using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AchievementTypeId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual AchievementType AchievementType { get; set; }
    }
}