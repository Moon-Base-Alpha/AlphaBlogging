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
        private readonly ICommentServices _repo;
        private readonly ISignedInService _signedInService;
        private readonly ApplicationDbContext _db;

        public CommentController(ICommentServices repo, ApplicationDbContext context, ISignedInService signedInService)
        {
            _repo = repo;
            _signedInService = signedInService; 
            _db = context;
        }

        public IActionResult CommentList(/*int postId*/)
        {
            //var comments = _db.Posts.Where(p => p.Id == postId).FirstOrDefault().Comments;
            var comments = _repo.GetAllComments();
            return View(comments);
        }

        public IActionResult CommentView (int id)
        {
            var comment = _repo.GetComment(id);
            return View(comment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Comment());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment comment, int postId)
        {
            var user = User.Identity.Name;
            comment.Author = _signedInService.GetAuthorId(user);

            //_repo.AddComment(comment);
            //if (await _repo.SaveChangesAsync())
            //    return RedirectToAction("Index", "Home");
            //else
            //    return View(comment);
            comment.PostId = postId;    
            //_repo.AddComment(comment);
            (_db.Posts.Where(p => p.Id == postId).FirstOrDefault()).Comments.Add(comment);
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("CommentList");
            else
                return View(comment);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Comment());
            else
            {
                var comment = _repo.GetComment((int)id);
                return View(comment);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Comment comment)
        {
            if (comment.Id > 0)
                _repo.UpdateComment(comment);
            else
            {
                _repo.AddComment(comment);
            }

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(comment);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.DeleteComment(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("CommentList");
        }
    }
}
