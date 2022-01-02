using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaBlogging.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageUser()
        {
            return View();
        }

        public IActionResult ManageBlog()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            return View();
        }
    }
}
