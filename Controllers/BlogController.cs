using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AlphaBlogging.Controllers
{
    public class BlogController : Controller
    {
        //Dependency Inject of BlogService and SignIn
        private readonly IBlogsService _bloggy;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public BlogController(IBlogsService bloggy, SignInManager<ApplicationUser> signInManager)
        {
            _bloggy = bloggy;
            _signInManager = signInManager; 
        }
        public IActionResult Bloglist()
        {
            var blogs = _bloggy.GetAllBlogs();
            return View(blogs);
        }
        [Authorize]
        public IActionResult Blog(int id)
        {
            var blog = _bloggy.GetBlog(id);
            return View(blog);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var signedIn = _signInManager.IsSignedIn(User);
            var user = User.Identity.Name;
            
           
            return View(new Blog());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {

            blog.Author.Email = User.Identity.Name;
            _bloggy.AddBlog(blog);

            if (await _bloggy.SaveChangesAsync())
                return RedirectToAction("Edit");
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
                var blog = _bloggy.GetBlog((int)id);
                return View(blog);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (blog.Id > 0)
                _bloggy.UpdateBlog(blog);
            else
            {
                _bloggy.AddBlog(blog);
            }

            if (await _bloggy.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(blog);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _bloggy.DeleteBlog(id);
            await _bloggy.SaveChangesAsync();
            return RedirectToAction("Bloglist");
        }

    }
}
