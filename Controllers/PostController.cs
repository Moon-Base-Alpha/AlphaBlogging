﻿using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlphaBlogging.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private IPostServices _postservice;
        private ITagServices _tagservice;
        private readonly ApplicationDbContext _db;
        private readonly IUserServices _userServices;
        public PostController(IUserServices us, IPostServices repo, ApplicationDbContext context, ITagServices tagservice, SignInManager<ApplicationUser> _SignInManager, ILogger<HomeController> _logger)
        {
            _postservice = repo;
            _tagservice = tagservice;   
            _db = context;
            _logger = this._logger;
            _SignInManager = this._SignInManager;
            _userServices = us;
        }


        // PostList only used for testing. TO BE REMOVED

        [Authorize(Roles = "Superadmin, Admin, Author")]
        public IActionResult Postlist(int blogid)
        {
            //var posts = _db.Blogs.Where(b => b.Id == blogid).FirstOrDefault().Posts;
            var posts = _postservice.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int Id) 
        {
            var post = new PostVM();
            //post.Id = Id;
            var dbPpost = _postservice.GetPost(Id);
            post.Title = dbPpost.Title;
            post.Body = dbPpost.Body;

            //post.Tags = 
            return View(post); 
        }

        public IActionResult PostView(int Id) 
        {            
            var dbPost = _postservice.GetPost(Id);
            //var post = new Post(dbPost.Title, dbPost.Body, 1);           

            //post.Tags = 

            _postservice.IncreaseViewCountOfPost(Id);
            return View(dbPost);
        }
      

        [HttpGet]
        public IActionResult Create(int blogId)
        {                        
            return View(new PostVM() {BlogId = blogId });
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(PostVM post /*, Tag tag, string tags*/)
        {

            if (ModelState.IsValid )
            {
                //var user = User.Identity.Name;


                //post.BlogId = (from x in _db.Blogs
                //               where x.Author == user
                //               select x).First();
                //post.BlogId = blogid;
                Post newPost = new Post(post.Title,post.Body,post.BlogId);


                if (post.HashTag != null)
                {
                    string[] tagArr = post.HashTag.Split(',');
                    foreach (string item in tagArr)
                    {
                        Tag foundTag = _tagservice.FindTag(item);
                        if (foundTag != null && item.Length != 0)
                            newPost.Tags.Add(foundTag);
                        else if(item.Length !=0)
                            newPost.Tags.Add(new Tag() { HashTag = item });
                    }
                }
                _postservice.AddPost(newPost);
            }
            if (await _postservice.SaveChangesAsync())
                //return RedirectToAction("Edit");
                return RedirectToAction("BlogView", "Blog", new { Id = post.BlogId });

            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PostVM());
            else
            {
                //var post = _postservice.GetPost((int)id);
                var postVm = new PostVM();
                postVm.PostId = (int)id;
                var dbPpost = _postservice.GetPost((int)id);
                postVm.Title = dbPpost.Title;
                postVm.Body = dbPpost.Body;
                postVm.Visible = true;

                foreach (var item in dbPpost.Tags)
                {
                    postVm.HashTag += item.HashTag;
                    postVm.HashTag += " ";
                }
                return View(postVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostVM postVm)
        {
            Post post = _postservice.GetPost(postVm.PostId);
            if (post != null)
            {
                post.Title = postVm.Title;
                post.Body = postVm.Body;
                post.Tags.Clear();
                if (postVm.HashTag != null)
                {
                    string[] tagArr = postVm.HashTag.Split(' ');
                    foreach (string item in tagArr)
                    {
                        Tag foundTag = _tagservice.FindTag(item);
                        if (foundTag != null && item.Length != 0)
                            post.Tags.Add(foundTag);
                        else if (item.Length != 0)
                            post.Tags.Add(new Tag() { HashTag = item });
                    }
                }
                _postservice.UpdatePost(post);
            }
            if (await _postservice.SaveChangesAsync())
                return Redirect($"~/Blog/BlogView/{post.BlogId}");
            else
                return View(postVm);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id, int blogId)
        {
            _postservice.DeletePost(id);
            await _postservice.SaveChangesAsync();
                      
            return Redirect($"~/Blog/BlogView/{blogId}");
        }


        public async Task<JsonResult> UserClicksOnLike(int Id) // Id is from the post in which the likebutton was clicked
        {
            var CurrUserID = User.Identity.Name;
            var CurrentUser = _userServices.GetCurrentApplicationUser(CurrUserID);

            var currentPost = _postservice.GetPost(Id);

            bool hasLiked = CurrentUser.LikedPosts.Contains(currentPost); // checks if the user has liked the selected post

            if (hasLiked == false)
            {
                CurrentUser.LikedPosts.Add(currentPost);
                _postservice.IncreaseLikesInPost(Id);
            }
            else if (hasLiked == true)
            {
                CurrentUser.LikedPosts.Remove(currentPost);
                _postservice.DecreaseLikesInPost(Id);
            }
            await _postservice.SaveChangesAsync();


            return Json(new {nofLikes= currentPost.Likes, postid= currentPost.Id});
        }
        public void IncreaseViewCountOfPost(int Id)
        {
            _postservice.GetViewsOfPost(Id);
            _db.SaveChangesAsync();

        }

    }
    

}
