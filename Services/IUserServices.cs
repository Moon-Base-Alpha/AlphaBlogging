using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AlphaBlogging.Data;
using AlphaBlogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AlphaBlogging.Services;
using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AlphaBlogging.Data.Repos
{
    public interface IUserServices
    {
        public bool IsLoggedIn();
        public string GetCurrentUsername();
        public string GetCurrentUserID();
        public ApplicationUser GetCurrentApplicationUser(string username);
    }
}
