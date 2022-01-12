﻿using AlphaBlogging.Data;
using AlphaBlogging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlphaBlogging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _SignInManager;


        public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> SignInManager)
        {
            _logger = logger;
            _SignInManager = SignInManager; 
           
        }

        public IActionResult Index()
        {
            var signedIn = _SignInManager.IsSignedIn(User);

            return View();
       

        }
        public IActionResult BlogCreate()
        {
            return View();
        }

        [HttpPost]
        public void BlogCreate(Blog newBlog)
        {
             RedirectToAction("Index","Home");
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
