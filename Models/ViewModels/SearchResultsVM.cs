using System.Collections.Generic;

namespace AlphaBlogging.Models.ViewModels
{
    public class SearchResultsVM
    {
        public string SearchTerm { get; set; }
        public List<Blog> Bloggies { get; set; }

        public List<Post> Posties { get; set; }

        public List<Tag> Tags { get; set; }

        public SearchResultsVM()
        {

        }

        public SearchResultsVM(string searchTerm, List<Blog> bloggies, List<Post> posties, List<Tag> tags)
        { 
            SearchTerm = searchTerm; 
            Bloggies = bloggies;    
            Posties = posties;  
            Tags = tags; 
           
        }
    }
}
