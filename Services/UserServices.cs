using AlphaBlogging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AlphaBlogging.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using System.Collections.Generic;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AlphaBlogging.Data;
using System.Linq;


namespace AlphaBlogging.Data.Services
{
    public class UserServices : Controller, IUserServices
    {
        
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;
        

        public UserServices(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _db = context;
            _signInManager = signInManager;
        }

        public bool IsLoggedIn()
        {
            var signedIn = _signInManager.IsSignedIn(User);
            return signedIn;
        }

        public string GetCurrentUserID(string userName)
        {
            
            var userId = (from x in _db.Users
                         where x.UserName == userName
                         select x.Id).ToString();

            return userId;
        }

        public string GetAuthorEmail(string userName)
        {

            var user = (from x in _db.Users
                          where x.UserName == userName
                          select x).FirstOrDefault();

            return user.Email;
        }


        public ApplicationUser GetCurrentApplicationUser(string userName)
        {
            var userObj = (from x in _db.Users
                         where x.UserName == userName
                         select x).FirstOrDefault();

            return userObj;
        }
        public async Task<bool> SaveChangesAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public string GetCurrentUsername()
        {
            throw new System.NotImplementedException();
        }

        public string GetCurrentUserID()
        {
            throw new System.NotImplementedException();
        }
    }
}
