﻿using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaBlogging.Services
{
    public interface IBlogsServices
    {
        
        Blog GetBlog(int Id);
        List<Blog> GetAllBlogs();
        List<Blog> GetMyBlogs(ApplicationUser authorId);
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(int Id);
        //List<Post> GetPostsFromBlogID(int BlogID);

        Task<bool> SaveChangesAsync();
        void AddImage(Blog blog);
        void DeleteImage(string oldImage);
        public void IncreaseViewCountOfAllPostsInBlog(int Id);
        IEnumerable<Post> GetPostsByDate();
        public List<Post> GetLatestPostFromAllBlogs();
        List<Blog> GetlatestBlogs();
    }
}
