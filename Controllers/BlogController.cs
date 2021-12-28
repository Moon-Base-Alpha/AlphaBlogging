﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IBlogsService _bloggyService;
        private readonly IPostServices _postService;
        private readonly ApplicationDbContext _db;

        public BlogController(IBlogsService bloggy, IPostServices posty, ApplicationDbContext context)
        {
            _bloggyService = bloggy;
            _postService = posty;
            _db = context;
        }

        public IActionResult Bloglist()
        {
            var blogs = _bloggyService.GetAllBlogs();
            return View(blogs);

        }

        public IActionResult MyBloglist()
        {
            
            var user = User.Identity.Name;

            var authorId = (from x in _db.Users
                           where x.UserName == user
                           select x).First();

            var blogs = _bloggyService.GetAllBlogs();

            var query = (from blogItem in blogs
                        where blogItem.Author == authorId
                        select blogItem).ToList();            

            return View(query);

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
