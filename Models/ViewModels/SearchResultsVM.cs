using System.Collections.Generic;

namespace AlphaBlogging.Models.ViewModels
{
    public class SearchResultsVM
    {
        public string SearchTerm { get; set; }
        public List<int> BlogIds { get; set; }

        public List<int> PostIds { get; set; }

        public List<int> TagPostIds { get; set; }

        public SearchResultsVM()
        {

        }

        public SearchResultsVM(string searchTerm, List<int> blogIds, List<int> postIds)
        { 
            SearchTerm = searchTerm;    
            BlogIds = blogIds;
            PostIds = postIds;
            TagPostIds = postIds;
        }
    }
}
