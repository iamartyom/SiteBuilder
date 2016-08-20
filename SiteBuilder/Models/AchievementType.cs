using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class AchievementType
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }

        public virtual IList<Achievement> Achievements { get; set; }
    }
}