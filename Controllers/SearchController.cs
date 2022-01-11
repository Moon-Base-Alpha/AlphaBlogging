using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlphaBlogging.Controllers
{
    public class SearchController : Controller
    {

        private ISearchServices _searchservice;
        
        public SearchController(ISearchServices searchservice)
        {
            _searchservice = searchservice;   
        }

        [HttpGet]
        public IActionResult Search()
        {            
            
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            var result = searchTerm;

            return RedirectToAction("SearchResult","Search", new { searchTerm = result });
        }
        public IActionResult SearchResult(string searchTerm)
        {
            var result = new SearchResultsVM(searchTerm, _searchservice.FindBlogsByTerm(searchTerm), _searchservice.FindPostsByTerm(searchTerm));
            //result.BlogIds =_searchservice.FindBlogsByTerm(searchTerm);
            //result.PostIds = _searchservice.FindPostsByTerm(searchTerm);

            return View(result);
        }
    }
}
