using AlphaBlogging.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using System.Linq;

using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AlphaBlogging.Data;
using System.Linq;
using AlphaBlogging.Data.Repos;

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
            var temp = User.Identity.Name; // ger null???
            var query = (from x in _db.Users
                         where x.UserName == User.Identity.Name
                         select x.Id).ToString();

            return result;
        }
        public ApplicationUser GetCurrentApplicationUser(string un)
        {
            var query = (from x in _db.Users
                         where x.UserName == un
                         select x).FirstOrDefault();
            return (ApplicationUser)query;
        }
        public async Task<bool> SaveChangesAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
