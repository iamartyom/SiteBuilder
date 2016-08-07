using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
using SiteBuilder.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SiteBuilder.Controllers
{
    public class PagesController : ApiController
    {
        private Storage db = new Storage();

        // GET api/Articles
        public ODataResult<Page> GetArticles(ODataQueryOptions options)
        {
            var items = db.Articles;
            var count = items.Count();
            var res = (IEnumerable<Page>)options.ApplyTo(items);

            return new ODataResult<Page>(res, null, count);
        }

        // GET api/Articles/5
        public Page GetArticle(int id)
        {
            Page article = db.Articles.Find(id);
            if (article == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return article;
        }

        // PUT api/Articles/5
        public HttpResponseMessage PutArticle(int id, Page article)
        {
            if (ModelState.IsValid && id == article.Id)
            {
                db.Entry(article).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Articles
        public HttpResponseMessage PostArticle(Page article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, article);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = article.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Articles/5
        public HttpResponseMessage DeleteArticle(int id)
        {
            Page article = db.Articles.Find(id);
            if (article == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Articles.Remove(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, article);
        }
    }
}
