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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AlphaBlogging.Controllers
{
    //[Authorize(Roles = "Superadmin, Admin, Author")]
    public class BlogController : Controller
    {
        //Dependency Inject of BlogService and SignIn
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISignedInService _signedInService; 
        private readonly IBlogsService _bloggyService;
        private readonly IPostServices _postService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public BlogController(ISignedInService signedInService, 
            IBlogsService bloggy, 
            IPostServices posty, 
            SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostEnvironment)
        {
            _bloggyService = bloggy;
            _postService = posty;
            _signedInService = signedInService;
            _signInManager = signInManager; 
            _webHostEnvironment = hostEnvironment;
            
        }

        public ApplicationUser GetSignedInId()
        {
            var signedIn = _signInManager.IsSignedIn(User);
            var user = User.Identity.Name;

            ApplicationUser authorId = _signedInService.GetAuthorId(user);

            return authorId;
        }

        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Bloglist()
        {
            var blogs = _bloggyService.GetAllBlogs();
            return View(blogs);

        }

        [Authorize(Roles = "Author")]
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


        //[Authorize]
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

            Blog bloggy = new Blog(blog.Title,blog.Description,blog.Body,blog.BlogImage,blog.Author,blog.Visible);

            _bloggyService.AddBlog(bloggy);

            if (await _bloggyService.SaveChangesAsync())
                return RedirectToAction("Index", "Home" );
            else
                return View(bloggy);
        }
        private string UploadedFile(Blog blog)
        {
            string uniqueFileName = null;
            if (blog.BlogImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment)
            }
            return uniqueFileName;
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
                return RedirectToAction("BlogView", new { id = blog.Id });
            else
                return RedirectToAction("Edit");
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
