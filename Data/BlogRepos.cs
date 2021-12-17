using AlphaBlogging.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlphaBlogging.Data
{
    public class BlogRepos
    {
        private static ApplicationDbContext _context;
        public BlogRepos(ApplicationDbContext aaa)
        {
            _context = aaa;
        }

        public static List<Blog> GetBlogs() // returns all blogs as a list
        {
            var blogs = from x in _context.Blogs select x;

            return blogs.ToList();
        }
    }
}
