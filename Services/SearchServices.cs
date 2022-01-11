using AlphaBlogging.Data;
using System.Collections.Generic;
using System.Linq;

namespace AlphaBlogging.Services
{
    public class SearchServices : ISearchServices
    {
        private ApplicationDbContext _db;

        public SearchServices(ApplicationDbContext context)
        {
            _db = context;

        }
        public List<int> FindBlogsByTerm(string term)
        {
            var query = _db.Blogs.Where(b =>b.Title.Contains(term)).Select(b => b.Id).ToList();

            return query;
            
            //throw new System.NotImplementedException();
        }

        public List<int> FindPostsByTagTerm(string term)
        {
            var query = _db.Posts.Where(p => p.Title.Contains(term)).Select(p => p.Id).ToList();

            return query;

            //throw new System.NotImplementedException();
        }

        public List<int> FindPostsByTerm(string term)
        {
            var query = _db.Posts.Where(p => p.Title.Contains(term)).Select(p => p.Id).ToList();

            return query;

            //throw new System.NotImplementedException();
        }
    }
}
