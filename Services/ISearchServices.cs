using AlphaBlogging.Models;
using System.Collections.Generic;

namespace AlphaBlogging.Services
{
    public interface ISearchServices
    {
        // Return list of blogIds
        public List<Blog> FindBlogsByTerm(string term);

        // Return list of postIds
        public List<Post> FindPostsByTerm(string term);

        // Return list of postIds
        public List<int> FindPostsByTagTerm(string term);
        public List<Tag> FindTagsByTerm(string term);
    }
}
