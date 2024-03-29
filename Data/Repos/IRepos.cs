﻿using AlphaBlogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public interface IRepos
    {
        Post GetPost(int Id);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int Id);

        Task<bool> SaveChangesAsync();
    }
}
