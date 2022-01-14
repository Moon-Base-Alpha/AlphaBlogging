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
using System.Net.Http.Headers;

namespace AlphaBlogging.Controllers
{
    //[Authorize(Roles = "Superadmin, Admin, Author")]
    public class BlogController : Controller
    {
        //Dependency Inject of BlogService and SignIn
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserServices _userServices; 
        private readonly IBlogsServices _bloggyService;
        private readonly IPostServices _postService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;


        public BlogController(IUserServices userServices, 
            IPostServices posty, 
            IBlogsServices bloggy,
            SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)

        {
            _bloggyService = bloggy;
            _postService = posty;
            _userServices = userServices;
            _signInManager = signInManager; 
            _webHostEnvironment = hostEnvironment;
            _db = context;
        }

        public ApplicationUser GetSignedInId()
        {
            var signedIn = _signInManager.IsSignedIn(User);
            var user = User.Identity.Name;

            ApplicationUser authorId = _userServices.GetCurrentApplicationUser(user);

            return authorId;
        }

        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Bloglist()
        {
            var blogs = _bloggyService.GetAllBlogs();
            return View(blogs);

        }

        [Authorize(Roles = "Superadmin, Admin, Author")]
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


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {            
            return View(new Blog());
        }
        //-----------------Test to add image to blog, not working at the moment------------------------
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Description,Body,BlogImage,Author,Visible")] Blog blog)
        //{
        //    blog.Author = GetSignedInId();


        //    //if (ModelState.IsValid)
        //    //{
        //        var files = HttpContext.Request.Form.Files;
        //        foreach (var Image in files)
        //        {
        //            if (Image != null && Image.Length > 0)
        //            {
        //                var file = Image;
        //                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "assets\\img");
        //                if (file.Length > 0)
        //                {
        //                    var fileName = ContentDispositionHeaderValue.Parse
        //                        (file.ContentDisposition).FileName.Trim('"');
        //                    System.Console.WriteLine(fileName);
        //                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
        //                    {
        //                        await file.CopyToAsync(fileStream);
        //                        blog.BlogImage = file.FileName;
        //                    }
        //                }
        //            }
        //        }



        //        Blog bloggy = new Blog(blog.Title, blog.Description, blog.Body, blog.BlogImage, blog.Author, blog.Visible);

        //        _bloggyService.AddBlog(bloggy);
        //        //await _bloggyService.SaveChangesAsync();
        //        //return RedirectToAction("Index", "Home");
        //    //}
        //    if (await _bloggyService.SaveChangesAsync())
        //       return RedirectToAction("Index", "Home" );
        //    else
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    }
        //    return View(bloggy);
        //}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {

            blog.Author = GetSignedInId();


            Blog bloggy = new Blog(blog.Title,blog.Description,blog.Body, blog.BlogImage,blog.Author,blog.Visible = true);


            _bloggyService.AddBlog(bloggy);

            if (await _bloggyService.SaveChangesAsync())

                if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
                {
                    return RedirectToAction("Bloglist");
                }
            return RedirectToAction("MyBloglist");
            //    return RedirectToAction("BlogView", new { id = blog.Id });
            //else
            //    return View(bloggy);

        }
        //private string UploadedFile(Blog blog)
        //{
        //    string uniqueFileName = null;
        //    if (blog.BlogImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment);
        //    }
        //    return uniqueFileName;
        //}

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
            if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
            {
                return RedirectToAction("Bloglist");
            }
                return RedirectToAction("MyBloglist");
        }

    }
}
