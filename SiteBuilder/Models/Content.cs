using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteBuilder.Models
{
    public class Content
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int Position { get; set; }
        public int ContentTypeId { get; set; }
        public string Data { get; set; }
    }
}