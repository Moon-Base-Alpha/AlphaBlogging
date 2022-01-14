using AlphaBlogging.Data;
using AlphaBlogging.Models;
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
        public List<Blog> FindBlogsByTerm(string term)
        {
            var query = _db.Blogs.Where(b =>b.Title.Contains(term)).ToList();

            return query;
            
            //throw new System.NotImplementedException();
        }

        public List<Tag> FindTagsByTerm(string term)
        {
            var query = _db.Tags.Where(t => t.HashTag.Contains(term)).ToList();


            return query;

            //throw new System.NotImplementedException();
        }

        public List<Post> FindPostsByTerm(string term)
        {
            var query = _db.Posts.Where(p => p.Title.Contains(term)).ToList();

            return query;

            //throw new System.NotImplementedException();
        }

        List<int> ISearchServices.FindPostsByTagTerm(string term)
        {
            throw new System.NotImplementedException();
        }
    }
}
