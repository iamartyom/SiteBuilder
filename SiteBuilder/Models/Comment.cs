using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Page Page { get; set; }
    }
}