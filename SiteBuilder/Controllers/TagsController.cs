using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SiteBuilder.Controllers
{
    public class TagsController : ApiController
    {
        // GET: api/Tags
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tags/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tags
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tags/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tags/5
        public void Delete(int id)
        {
        }
    }
}
