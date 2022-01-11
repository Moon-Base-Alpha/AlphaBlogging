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
using AlphaBlogging.Data.Repos;

namespace AlphaBlogging.Controllers
{
    public class BlogController : Controller
    {
        //Dependency Inject of BlogService and SignIn
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISignedInService _signedInService; 
        private readonly IBlogsService _bloggyService;
        private readonly IPostServices _postService;
        

        public BlogController(ISignedInService signedInService, IBlogsService bloggy, IPostServices posty, SignInManager<ApplicationUser> signInManager)
        {
            _bloggyService = bloggy;
            _postService = posty;
            _signedInService = signedInService;
            _signInManager = signInManager; 
            
        }

        public ApplicationUser GetSignedInId()
        {
            var signedIn = _signInManager.IsSignedIn(User);
            var user = User.Identity.Name;

            ApplicationUser authorId = _signedInService.GetAuthorId(user);

            return authorId;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Bloglist()
        {
            var blogs = _bloggyService.GetAllBlogs();
            return View(blogs);

        }

        public IActionResult MyBloglist()
        {
            
            var user = User.Identity.Name;

            ApplicationUser authorId = GetSignedInId();

            var blogs = _bloggyService.GetMyBlogs(authorId);

            //var query = (from blogItem in blogs
            //            where blogItem.Author == authorId
            //            select blogItem).ToList();            

            return View(blogs);

        }


        [Authorize]
        public IActionResult BlogView(int id)
        {           
            var blog = _bloggyService.GetBlog(id);           
            return View(blog);  
        }
        public IActionResult BlogPostView(int Id)
        {
            //var blog = _bloggyService.GetPostsFromBlogID(Id);
            return View();
        }


        [Authorize(Roles = "Admin, Author")]
        [HttpGet]
        public IActionResult Create()
        {            
            return View(new Blog());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {            
            
            blog.Author = GetSignedInId();

            Blog bloggy = new Blog(blog.Title,blog.Body,blog.Author,blog.Visible = true);

            _bloggyService.AddBlog(bloggy);

            if (await _bloggyService.SaveChangesAsync())
                return RedirectToAction("Index", "Home" );
            else
                return View(bloggy);
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
