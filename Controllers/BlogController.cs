using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;

namespace AlphaBlogging.Controllers
{
    public class BlogController : Controller
    {
        private readonly ICreateBlogsService _createBlogsService;

        public BlogController(ICreateBlogsService createBlogsService)
        {
            _createBlogsService = createBlogsService;
        }
        public async Task<IActionResult> CreateNewBlog(CreateBlogVM blogs)
        {
            // TO DO
            // blogs = await _createBlogsService.CreateBlogsAsync(blogs);

            return View();
        }

    }
}
