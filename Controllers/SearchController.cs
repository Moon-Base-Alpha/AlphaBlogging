using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AlphaBlogging.Controllers
{
    public class SearchController : Controller
    {

        private ISearchServices _searchservice;
        private IBlogsServices _blogsServices;
        private IPostServices _postServices;


        public SearchController(ISearchServices searchservice, IBlogsServices blogsServices, IPostServices postServices)
        {
            _searchservice = searchservice; 
            _blogsServices = blogsServices;
            _postServices = postServices; 
        }

        //[HttpGet]
        //public IActionResult Search()
        //{            
            
        //    return View();
        //}

     
        //public IActionResult Search(string searchTerm)
        //{

        //    return RedirectToAction("SearchResult",new { searchTerm = searchTerm });
        //}
        //[HttpGet]
        public IActionResult SearchResult(string searchTerm)
        {

            var blogResults = _searchservice.FindBlogsByTerm(searchTerm);
            var postsResults = _searchservice.FindPostsByTerm(searchTerm);
            var tagResults = _searchservice.FindTagsByTerm(searchTerm);
            var result = new SearchResultsVM(searchTerm, blogResults, postsResults, tagResults);
            return View(result);
        }
    }
}
