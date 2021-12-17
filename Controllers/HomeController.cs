using AlphaBlogging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static AlphaBlogging.Data.Repos.BlogRepos;

namespace AlphaBlogging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //return a linq list with all user's blogs by using a _db context call, from repos
            return View();
        }
        public IActionResult BlogCreate()
        {
            return View();
        }

        [HttpPost]  
        public IActionResult BlogCreate(ApplicationUser newBlogger)
        {
            return View(CreateBlog(newBlogger));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
