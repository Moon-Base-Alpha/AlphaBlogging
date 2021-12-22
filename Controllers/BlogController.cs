using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AlphaBlogging.Data;
using System.Linq;

namespace AlphaBlogging.Controllers
{
    public class BlogController : Controller
    {
        //Dependency Inject of BlogService and SignIn
        private readonly IBlogsService _bloggyService;
        private readonly ApplicationDbContext _db;

        public BlogController(IBlogsService bloggy, ApplicationDbContext context)
        {
            _bloggyService = bloggy;
            _db = context;
        }

        public IActionResult Bloglist()
        {
            var blogs = _bloggyService.GetAllBlogs();
            return View(blogs);
        }

        [Authorize]
        public IActionResult Blog(int id)
        {
            var blog = _bloggyService.GetBlog(id);
            return View(blog);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {            
            return View(new Blog());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            var user = User.Identity.Name;
            
            blog.Author = (from x in _db.Users
                           where x.UserName == user
                           select x).First();

            _bloggyService.AddBlog(blog);

            if (await _bloggyService.SaveChangesAsync())
                return RedirectToAction("Index", "Home" );
            else
                return View(blog);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Blog());
            else
            {
                var blog = _bloggyService.GetBlog((int)id);
                return View(blog);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (blog.Id > 0)
                _bloggyService.UpdateBlog(blog);
            else
            {
                _bloggyService.AddBlog(blog);
            }

            if (await _bloggyService.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(blog);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _bloggyService.DeleteBlog(id);
            await _bloggyService.SaveChangesAsync();
            return RedirectToAction("Bloglist");
        }
    }
}
