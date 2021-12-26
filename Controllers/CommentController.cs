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
        private ICommentServices _repo;
        private readonly ApplicationDbContext _db;

        public CommentController(ICommentServices repo, ApplicationDbContext context)
        {
            _repo = repo;
            _db = context;
        }

        public IActionResult Commentlist(int commentId)
        {
            var comments = _repo.GetAllComments();
            return View(comments);
        }

        public IActionResult Comment (int id)
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
        public async Task<IActionResult> Create(Comment comment/*, int commentId*/)
        {
            var user = User.Identity.Name;

            comment.Author = (from x in _db.Users
                              where x.UserName == user
                              select x).First();

            _repo.AddComment(comment);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
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
