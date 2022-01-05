using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Controllers
{
    public class PostController : Controller
    {
        private IPostServices _postservice;
        private ITagServices _tagservice;
        private readonly ApplicationDbContext _db;
        public PostController(IPostServices repo, ApplicationDbContext context, ITagServices tagservice)
        {
            _postservice = repo;
            _tagservice = tagservice;   
            _db = context;
            
  
      }

        // PostList only used for testing. TO BE REMOVED

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

        [HttpGet]
        public IActionResult Create(int blogId)
        {                        
            return View(new PostVM() {BlogId = blogId });
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(PostVM post, int blogId /*, Tag tag, string tags*/)
        {

            if (ModelState.IsValid )
            {
                //var user = User.Identity.Name;


                //post.BlogId = (from x in _db.Blogs
                //               where x.Author == user
                //               select x).First();
                //post.BlogId = blogid;
                Post newPost = new Post(post.Title,post.Body,blogId);


                if (post.HashTag != null)
                {
                    string[] tagArr = post.HashTag.Split(',');
                    foreach (string item in tagArr)
                    {
                        Tag foundTag = _tagservice.FindTag(item);
                        if (foundTag != null)
                            newPost.Tags.Add(foundTag);
                        else
                            newPost.Tags.Add(new Tag() { HashTag = item });
                    }
                }
                _postservice.AddPost(newPost);
            }   
            if (await _postservice.SaveChangesAsync())
                //return RedirectToAction("Edit");
                return Redirect($"~/Blog/BlogView/{post.BlogId}");

            else
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Edit(int? id, Tag tag)
        {
            if (id == null)
                return View(new PostVM());

            else
            {
               
                //var post = _postservice.GetPost((int)id);

                var post = new PostVM();
                post.BlogId = (int)id;
                var dbPpost = _postservice.GetPost((int)id);
                post.Title = dbPpost.Title;
                post.Body = dbPpost.Body;               
                post.Visible = true;

                
                return View(post);
                //string tagg = _db.Tags.Find().HashTag;
                //return View(tag);
            }           
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post, int blogId, Tag tag)
        {
            if (post.Id > 0)
                _postservice.UpdatePost(post);
            else
            {
                _postservice.AddPost(post);
            }
           
            if (await _postservice.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id, int blogId)
        {
            _postservice.DeletePost(id);
            await _postservice.SaveChangesAsync();
                      
            return Redirect($"~/Blog/BlogView/{blogId}");
        }

        //// GET: PostController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: PostController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PostController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PostController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PostController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PostController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PostController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PostController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    Post post = _db.Posts.Where(post=>post.Id == id).FirstOrDefault();
        //    try
        //    {
        //        _db.Posts.Remove(post);

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult UserClicksOnLike(int Id)////////////////////////////
        {
            //_postservice.
            //await _postservice.SaveChangesAsync();


            return View();
        }


        [HttpPost]
        public ActionResult UserClicksOnLike()////////////////////////////
        {
            //_postservice.
            //await _postservice.SaveChangesAsync();


            return View();
        }
    }

   
}
