using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Controllers
{
    public class CommentController : Controller
    {

        private ICommentServices _commentservice;

        private IPostServices _postService;

        private readonly ApplicationDbContext _db;

        public CommentController(ICommentServices repo, IPostServices postServices, ApplicationDbContext context)
        {

            _commentservice = repo;
            _postService = postServices;
            _db = context;
        }

        public IActionResult CommentList(/*int postId*/)
        {
            //var comments = _db.Posts.Where(p => p.Id == postId).FirstOrDefault().Comments;
            var comments = _commentservice.GetAllComments();
            return View(comments);
        }

        public IActionResult CommentView (int id)
        {
            var comment = _commentservice.GetComment(id);
            return View(comment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Comment());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment comment/*, int postId*/)
        {
            //var user = User.Identity.Name;
            //comment.Author = (from x in _db.Users
            //                  where x.UserName == user
            //                  select x).First();

            //_commentservice.AddComment(comment);
            //if (await _commentservice.SaveChangesAsync())
            //    return RedirectToAction("Index", "Home");
            //else
            //    return View(comment);

            _commentservice.AddComment(comment);
            (_db.Posts.Where(p => p.Id == 1).FirstOrDefault()).Comments.Add(comment);
            if (await _commentservice.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(comment);

            //_repo.AddComment(comment);
            //(_db.Posts.Where(p => p.Id == 1).FirstOrDefault()).Comments.Add(comment);
            //if (await _repo.SaveChangesAsync())
            //    return RedirectToAction("Edit");
            //else
            //    return View(comment);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Comment());
            else
            {
                var comment = _commentservice.GetComment((int)id);
                return View(comment);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Comment comment)
        {
            if (comment.Id > 0)
                _commentservice.UpdateComment(comment);
            else
            {
                _commentservice.AddComment(comment);
            }

            if (await _commentservice.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(comment);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _commentservice.DeleteComment(id);
            await _commentservice.SaveChangesAsync();
            return RedirectToAction("CommentList");
        }
    }
}
