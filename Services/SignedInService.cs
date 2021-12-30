using AlphaBlogging.Data;
using AlphaBlogging.Models;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlphaBlogging.Services
{
    public class SignedInService : ISignedInService
    {

        private ApplicationDbContext _db;
        
        public SignedInService(ApplicationDbContext context)
        {
            _db = context;            
        }

        public ApplicationUser GetAuthorId(string user)
        {
            
            var authorId = (from x in _db.Users
                            where x.UserName == user
                            select x).First();
            return authorId;
        }
    }
}
