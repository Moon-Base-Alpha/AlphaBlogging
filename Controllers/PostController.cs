using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Controllers
{
    public class PostController : Controller
    {
        private IPostServices _repo;
        private ITagServices _tagservice;
        private readonly ApplicationDbContext _db;
        public PostController(IPostServices repo, ApplicationDbContext context, ITagServices tagservice)
        {
            _repo = repo;
            _tagservice = tagservice;   
            _db = context;
            
        }
       
        public IActionResult Postlist(int blogid)
        {
            var posts = _db.Blogs.Where(b => b.Id == blogid).FirstOrDefault().Posts;
            //var posts = _repo.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int Id) 
        {
            var post = new PostVM();
            post.Id = Id;
            post.Title = _repo.GetPost(Id).Title;
            post.Body = _repo.GetPost(Id).Body;
            post.Tags = _repo.GetPost(Id).Tags;
            return View(post); 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PostVM());
        }
        [HttpPost]
        public async Task<IActionResult> AddTag(Tag tag)
        {

           _tagservice.AddTag(tag); 
                
            if (await _repo.SaveChangesAsync())
                return RedirectToAction();
            else
                return View(tag);

        }
        [HttpPost]
        public async Task<IActionResult> Create(Post post, int blogId, Tag tag, string tags)
        {           
            if (post == null | tag == null)
           
            //var user = User.Identity.Name;

            //post.BlogId = (from x in _db.Blogs
            //               where x.Author == user
            //               select x).First();
            //post.BlogId = blogid;
            
            _repo.AddPost(post);
            (_db.Blogs.Where(b => b.Id == blogId).FirstOrDefault()).Posts.Add(post);
            
            _tagservice.AddTag(tag);
            _db.SaveChanges();
            PostTag posTag = new PostTag()
            {
                PostsId = post.Id,
                TagsId = _db.Tags.Where(t=>t.HashTag == tag.HashTag).FirstOrDefault().Id
            };
            //var PostTag = _db.PostTag.ToList(); 
            //_db.PostTag.Add(posTag);
            _db.SaveChanges();
            //string[] tagArr = tags.Split(',');

            //foreach (string item in tagArr)
            //{

            //    postTags .AddTag(item);
            //}
            //_tagservice.AddTag(tag);

            //(_db.Posts.Where(p => p.Id == 3).FirstOrDefault()).Tags.Add(tag);
            if (await _repo.SaveChangesAsync())
                //return RedirectToAction("Edit");
                return Redirect($"~/Blog/BlogView/{blogId}");
            else
                return View(post);
        }
        [HttpGet]
        public IActionResult Edit(int? id, Tag tag)
        {
            if (id == null)
                return View(new PostVM());

            else
            {

                //var post = _repo.GetPost((int)id);

                var post = new PostVM();
                post.Id = (int)id;
                post.Title = _repo.GetPost((int)id).Title;
                post.Body = _repo.GetPost((int)id).Body;
                post.Tags = _repo.GetPost((int)id).Tags;
                post.PostTagId = _tagservice.GetTag((int)id).Id;
                post.PostsId = _repo.GetPost((int)(id)).Id;
               
                return View(post);
                //string tagg = _db.Tags.Find().HashTag;
                //return View(tag);
            }           
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post, Tag tag)
        {
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
            {
                _repo.AddPost(post/*, tag*/);
            }
           
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id, int blogId)
        {
            _repo.DeletePost(id);
            await _repo.SaveChangesAsync();
                      
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
    }

   
}
