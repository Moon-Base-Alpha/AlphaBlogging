using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Mvc;
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
            var blogResults = _searchservice.FindBlogsByTerm(searchTerm);
            var postsResults = _searchservice.FindPostsByTerm(searchTerm);
            var tagResults = _searchservice.FindTagsByTerm(searchTerm);

            List<Blog> listBlogResults = new List<Blog>();

            foreach (var blogPost in blogResults)
                listBlogResults.Add(_blogsServices.GetBlog(blogPost));

            List<Post> listPostsResults = new List<Post>();

            foreach (var post in postsResults)
                listPostsResults.Add(_postServices.GetPost(post));

            var result = new SearchResultsVM(searchTerm, listBlogResults, listPostsResults, tagResults);
            //result.BlogIds =_searchservice.FindBlogsByTerm(searchTerm);
            //result.PostIds = _searchservice.FindPostsByTerm(searchTerm);

            return View(result);
        }
    }
}
