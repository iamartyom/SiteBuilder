using System;
using System.IO;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;
using SiteBuilder.Models;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;

namespace SiteBuilder.Lucene
{
    public abstract class LuceneGeneric<T> : LuceneSearch
    {
        protected virtual void _addToLuceneIndex(T temp, IndexWriter writer)
        {
            // remove older index entry

            //var searchQuery = new TermQuery(new Term("Id", page.Id.ToString()));
            //writer.DeleteDocuments(searchQuery);
            _oldIndexRemover(temp, writer);

            // add new index entry
            var doc = new Document();


            // add lucene fields mapped to db fields
            //doc.Add(new Field("Id", temp.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new Field("Name", temp.Name, Field.Store.YES, Field.Index.ANALYZED));
            _fieldAdder(temp);

            // add entry to index
            writer.AddDocument(doc);
        }

        protected abstract void _oldIndexRemover(T temp, IndexWriter writer);

        protected abstract void _fieldAdder(T tepm);



        protected abstract T _mapLuceneDocumentToData(Document doc);
        //{
        //    return new T
        //    {
        //        Id = Convert.ToInt32(doc.Get("Id")),
        //        Name = doc.Get("Name")
        //    };
        //}

        protected IEnumerable<T> _search(string searchQuery, string searchField = "")
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<T>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                var hits_limit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);

                // search by single field
                if (!string.IsNullOrEmpty(searchField))
                {
                    var parser = new QueryParser(Version.LUCENE_30, searchField, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, hits_limit).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else
                {
                    return (MultiFieldSearch(analyzer, searchQuery, searcher, hits_limit));
                }
            }
        }

        protected virtual IEnumerable<T> MultiFieldSearch (StandardAnalyzer analyzer, string searchQuery, IndexSearcher searcher, int hits_limit)
        {
            var parser = new MultiFieldQueryParser
            (Version.LUCENE_30, new[] { "Id", "Name" }, analyzer);
            var query = parseQuery(searchQuery, parser);
            var hits = searcher.Search
            (query, null, hits_limit, Sort.RELEVANCE).ScoreDocs;
            var results = _mapLuceneToDataList(hits, searcher);
            analyzer.Close();
            searcher.Dispose();
            return results;
        }

        public void AddUpdateLuceneIndex(IEnumerable<T> pages)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var comment in pages)
                {
                    _addToLuceneIndex(comment, writer);
                }
                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public void AddUpdateLuceneIndex(T pages)
        {
            AddUpdateLuceneIndex(new List<T> { pages });
        }

        protected IEnumerable<T> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }

        protected IEnumerable<T> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        protected IEnumerable<T> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) return new List<T>();

            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }

        protected IEnumerable<T> GetAllIndexRecords()
        {
            // validate search index
            if (!System.IO.Directory.EnumerateFiles(_luceneDir).Any()) return new List<T>();

            // set up lucene searcher
            var searcher = new IndexSearcher(_directory, false);
            var reader = IndexReader.Open(_directory, false);
            var docs = new List<Document>();
            var term = reader.TermDocs();
            while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            reader.Dispose();
            searcher.Dispose();
            return _mapLuceneToDataList(docs);
        }
    }
}
