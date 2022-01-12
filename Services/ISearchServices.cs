using System.Collections.Generic;

namespace AlphaBlogging.Services
{
    public interface ISearchServices
    {
        // Return list of blogIds
        public List<int> FindBlogsByTerm(string term);

        // Return list of postIds
        public List<int> FindPostsByTerm(string term);

        // Return list of postIds
        public List<int> FindPostsByTagTerm(string term);
    }
}
