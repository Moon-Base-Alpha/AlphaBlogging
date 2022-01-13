using AlphaBlogging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaBlogging.Controllers
{
    public class UserController:Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(SignInManager<ApplicationUser> signInManager)
        {            
            _signInManager = signInManager;
        }


    }
}
