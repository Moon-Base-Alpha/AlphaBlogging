using AlphaBlogging.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using System.Linq;



namespace AlphaBlogging.Data.Repos
{
    public class UserServices : Controller, IUserServices
    {
        
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly ApplicationDbContext _db;
        private IUserServices _UserService;

        public UserServices(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _db = context;
            _SignInManager = signInManager;
        }

        public bool IsLoggedIn()
        {
            var signedIn = _SignInManager.IsSignedIn(User);
            return signedIn;
        }
        public string GetCurrentUsername()
        {
            string result = "";
            var user = User.Identity.Name;

            return result;
        }
        public string GetCurrentUserID()
        {
            string result = "";
            string temp = User.Identity.Name; // ger null???
            var query = (from x in _db.Users
                         where x.UserName == User.Identity.Name
                         select x.Id).ToString();

            return result;
        }
        public ApplicationUser GetCurrentApplicationUser()
        {
            var query = (from x in _db.Users
                         where x.UserName == User.Identity.Name
                         select x).FirstOrDefault();
            return (ApplicationUser)query;
        }

       
    }
}
