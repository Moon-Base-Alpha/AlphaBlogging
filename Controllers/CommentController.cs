﻿using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlphaBlogging.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {

        private readonly ICommentServices _commentService;
        private readonly IUserServices _userServices;
        private readonly ISMSServices _smssService; 
        private readonly ApplicationDbContext _db;
        private static readonly HttpClient httpClient = new();
        private readonly HttpRequestSettings _requestSettings;

        public CommentController(
            IOptions<HttpRequestSettings> requestSettings,
            ICommentServices commentService, 
            ISMSServices smssService, 
            IUserServices userServices, 
            ApplicationDbContext context
            )
        {

            _commentService = commentService;
            _userServices = userServices;
            _smssService = smssService;
            _db = context;
            _requestSettings = requestSettings.Value;
        }

        [Authorize(Roles = "Superadmin, Admin, Author")]
        public IActionResult CommentList()        {
            
            var comments = _commentService.GetAllComments();
            return View(comments);
        }

        public IActionResult CommentView (int id)
        {
            var comment = _commentService.GetComment(id);
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

                Comment newComment = new(comment.Body, _userServices.GetCurrentApplicationUser(user), comment.PostId);

                _commentService.AddComment(newComment);


                if (await _commentService.SaveChangesAsync())
                    return RedirectToAction("PostView", "Post",new {id = comment.PostId});
                    //return RedirectToAction("BlogView", "Blog", new { Id = post.BlogId });

                else
                    return View(newComment);
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return View(new Comment());
            else
            {
                var comment = _commentService.GetComment((int)id);
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
                _commentService.UpdateComment(comment);
            }
            
            else
            {
                _commentService.AddComment(comment);
            }


            if (await _commentService.SaveChangesAsync())
                return RedirectToAction("PostView","Post",new { Id = comment.PostId});

            else
                return View(comment);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int commentId)
        {

            // Commenting the SMS functionality below. It works but sends me a SMS for each deleted comment. Anoying!!!!

            //int introLength = 10;
            //string userName = _commentService.GetCommentOwner(commentId);
            //string commentStart = _commentService.GetFirstPartOfComment(commentId, introLength);
            ////string mobileNum = _userServices.GetAuthorMobile(userName);

            //var sendMsg = "Dear " + userName + "\nYour comment \"" + commentStart + "\" has been erased due to infringement of the site rules!";

            //_smssService.SendSMS(sendMsg);

            //TempData["EmailStatus"] = await SendSMSToCommenter(sendMsg);

            var entry = _db.Comments.Single(r => r.Id == commentId);
            _commentService.DeleteComment(commentId);
            await _commentService.SaveChangesAsync();
            return RedirectToAction("PostView", "Post", new { id = entry.PostId });

        }


        private async Task<string> SendSMSToCommenter(string sendMsg)
        {

            //string funcUrl = _requestSettings.MyAzureSMSUrl;  //Azure url
            string funcUrl = _requestSettings.MyLocalSMSUrl;  // For testing function locally(localhost://)
            string statusMsg = "";

            using (var myrequest = new HttpRequestMessage(HttpMethod.Post, funcUrl))
            {
                var json = JsonConvert.SerializeObject(sendMsg);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                myrequest.Content = httpContent;

                using (var newresponse = await httpClient
                    .SendAsync(myrequest)
                    .ConfigureAwait(false))
                {
                    if (newresponse.IsSuccessStatusCode)
                    {
                        statusMsg = "The comment has been created. A message has been sent by SMS to the commenter.";
                    }
                    else
                        statusMsg = "Ooops!";
                }
            }
            return statusMsg;
        }
    }
}
