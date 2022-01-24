using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {

        private ICommentServices _commentservice;
        private IPostServices _postService;
        private readonly IUserServices _userServices;
        

        public CommentController(ICommentServices repo, IPostServices postServices, IUserServices userServices)
        {

            _commentservice = repo;
            _postService = postServices;
            _userServices = userServices;

        }

        [Authorize(Roles = "Superadmin, Admin, Author")]
        public IActionResult CommentList()
        {
            
            var comments = _commentservice.GetAllComments();
            return View(comments);
        }

        public IActionResult CommentView (int id)
        {
            var comment = _commentservice.GetComment(id);
            return View(comment);
        }

        
        [HttpGet]
        public IActionResult Create(int postId)
        {
            return View(new Comment() { PostId = postId});
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
                var user = User.Identity.Name;

                Comment newComment = new Comment(comment.Body, _userServices.GetCurrentApplicationUser(user), comment.PostId, comment.PostTitle);

                _commentservice.AddComment(newComment);

                if (await _commentservice.SaveChangesAsync())
                    return RedirectToAction("Edit");
                else
                    return View(newComment);
            
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
            var user = User.Identity.Name;

            if (comment.Id > 0)
            {
                comment.Author = _userServices.GetCurrentApplicationUser(user);
                _commentservice.UpdateComment(comment);
            }
            
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
