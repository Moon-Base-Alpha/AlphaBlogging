using AlphaBlogging.Data;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AlphaBlogging.Services;
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
    public class LikeController : Controller
    {
        private IPostServices _postservice;

        public LikeController(IPostServices IPS)
        {
            _postservice = IPS;
        }
        public IActionResult LikesView(int Id)
        {
            var thePost = _postservice.GetPost(Id);
            uint theLikes = thePost.Likes;

            return View(theLikes);
        }

    }
}
