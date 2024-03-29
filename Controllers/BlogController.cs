﻿using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using System.Threading.Tasks;
using AlphaBlogging.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Hosting;


namespace AlphaBlogging.Controllers
{
    //[Authorize(Roles = "Superadmin, Admin, Author")]
    public class BlogController : Controller
    {
        //Dependency Inject of BlogService and SignIn
        private readonly IUserServices _userServices;
        private readonly IBlogsServices _bloggyService;
        private readonly IPostServices _postService;
        private static readonly HttpClient httpClient = new();
        private readonly HttpRequestSettings _requestSettings;
        public BlogController(
            IUserServices userServices,
            IPostServices posty,
            IBlogsServices bloggy,
            IOptions<HttpRequestSettings> requestSettings
            )

        {
            _bloggyService = bloggy;
            _postService = posty;
            _userServices = userServices;
            _requestSettings = requestSettings.Value;
        }

        public IActionResult PostArchive(int id)
        {
            var query = _postService.GetPostsByDate(id);
                return View(query);
        }
        public ApplicationUser GetSignedInId()
        {
            //var signedIn = _signInManager.IsSignedIn(User);
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
            
            //var user = User.Identity.Name;

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
            _bloggyService.IncreaseViewCountOfAllPostsInBlog(id);
            return View(blog);  
        }

        //public IActionResult BlogPostView(int Id)
        //{
        //    //var blog = _bloggyService.GetPostsFromBlogID(Id);
        //    return View();
        //}


        //[Authorize]
        //[HttpGet]
        //public IActionResult CreateBlog()
        //{

        //    return View(new Blog());
        //}


        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> CreateBlog(Blog blog)
        //{
        //    blog.Author = GetSignedInId();
        //    // _bloggyService.AddImage(blog);

        //    Blog bloggy = new Blog(blog.Title, blog.Description, blog.Body, blog.BlogImage, blog.ImageFile, blog.Author, blog.Visible = true);

        //    _bloggyService.AddBlog(bloggy);

        //    if (await _bloggyService.SaveChangesAsync())

        //        if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
        //        {
        //            return RedirectToAction("Bloglist");
        //        }
        //    return RedirectToAction("MyBloglist");

        //}

        [Authorize]
        [HttpGet]
        public IActionResult CreateBlog()
        {
            return View(new Blog());
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            blog.Author = GetSignedInId();
            if (blog.ImageFile != null)
                _bloggyService.AddImage(blog);

            Blog bloggy = new(blog.Title, blog.Description, blog.Body, blog.BlogImage, blog.ImageFile, blog.Author, blog.Visible = true);

            _bloggyService.AddBlog(bloggy);

            if (await _bloggyService.SaveChangesAsync())

            {
                string userEmail = _userServices.GetAuthorEmail(User.Identity.Name);

                ConfirmMessage sendMsg = new()
                {
                    Email = userEmail,
                    MobileNumber = "0",
                    BlogTitle = blog.Title,
                    ConfirmText = "<html><body><h2>Dear " + User.Identity.Name
                    + "!</h2><h3> Your blog " + blog.Title + " is now ready to use!</h3>"
                };

                TempData["EmailStatus"] = await SendConfirmation(sendMsg);

                //return RedirectToAction("BlogView", new { id = blog.Id });

                if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
                {
                    return RedirectToAction("Bloglist");
                }

                return RedirectToAction("MyBloglist");
            }
            else
                return View(bloggy);


            //if (await _bloggyService.SaveChangesAsync())
            //    if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
            //    {
            //        return RedirectToAction("Bloglist");
            //    }
            //return RedirectToAction("MyBloglist");

            //    return RedirectToAction("BlogView", new { id = blog.Id });
            //else
            //    return View(bloggy);
        }


        private async Task<string> SendConfirmation(ConfirmMessage sendMsg)
        {
            //string funcUrl = "https://alphablogqueuefunction.azurewebsites.net/api/HttpTrigger1?code=54KCQAvWXKb6qcuogze/uNfIlwnaQUpz120AiNjQI5VD/3ogmqla7Q==";
            string funcUrl = _requestSettings.MyAzureFunctionUrl;  //Azure url
            //string funcUrl = _requestSettings.MyLocalFunctionUrl;  // For testing function locally(localhost://)
            string statusMsg = "";

            using (var myrequest = new HttpRequestMessage(HttpMethod.Post, funcUrl))
            {
                var json = JsonConvert.SerializeObject(sendMsg);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                myrequest.Content = httpContent;

                using var newresponse = await httpClient
                    .SendAsync(myrequest)
                    .ConfigureAwait(false);
                if (newresponse.IsSuccessStatusCode)
                {
                    statusMsg = "Your blog has been created. A confirmation has been sent by email.";
                }
                else
                    statusMsg = "Ooops!";
            }
            return statusMsg;
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new Blog());
            }
            else
            {
                var blog = _bloggyService.GetBlog((int)id);

                if (User.IsInRole("Superadmin") || User.IsInRole("Admin") || blog.Author.UserName == User.Identity.Name)
                {
                    return View(blog); 
                }
            }
            return View("NotFound");

            //if (id == null)
            //    return View(new Blog());
            //else
            //{
            //    var blog = _bloggyService.GetBlog((int)id);
            //    return View(blog);
            //}
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (blog.Id > 0) 
            {
                //_bloggyService.AddImage(blog);
                _bloggyService.UpdateBlog(blog);
            }
            else
            {
                //_bloggyService.AddImage(blog);
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
            var blog = _bloggyService.GetBlog((int)id);

            if (User.IsInRole("Admin") || User.IsInRole("Superadmin") || blog.Author.UserName == User.Identity.Name)
            {
                _bloggyService.DeleteBlog(id);
                await _bloggyService.SaveChangesAsync();
            }

            if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
            {
                return RedirectToAction("Bloglist");
            }
            return RedirectToAction("MyBloglist");

            //_bloggyService.DeleteBlog(id);
            //await _bloggyService.SaveChangesAsync();
            //if (User.IsInRole("Admin") || User.IsInRole("Superadmin"))
            //{
            //    return RedirectToAction("Bloglist");
            //}
            //    return RedirectToAction("MyBloglist");
        }

    }
}
