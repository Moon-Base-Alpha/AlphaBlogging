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

namespace AlphaBlogging.Services
{
    public class BlogsService : IBlogsService
    {
        private ApplicationDbContext _db;
        

        public BlogsService(ApplicationDbContext context)
        {
            _db = context;
            
        }
        
        public void AddBlog(Blog blog)
        {
            blog.Created = DateTime.Now;    
            _db.Blogs.Add(blog);
        }

        public void DeleteBlog(int id)
        {
            _db.Blogs.Remove(GetBlog(id));
        }

        public List<Blog> GetAllBlogs()
        {           

            return _db.Blogs.ToList();
        }

        public List<Blog> GetMyBlogs(ApplicationUser authorId)
        {
            var allBlogs = GetAllBlogs();

            var query = (from blogItem in allBlogs
                         where blogItem.Author == authorId
                         select blogItem).ToList();

            return query;
        }

        public Blog GetBlog(int id)
        {
            var q0 = _db.Blogs.Find(id);

            return q0;

        }

        public void UpdateBlog(Blog blog)
        {
            _db.Blogs.Update(blog);
        }
        public async Task<bool> SaveChangesAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        public List<Post> GetPostsFromBlogID(int BlogID) // returns all blogs as a list
        {
            List<Post> resultList = new List<Post>();

            var temp = (from x in _db.Blogs
                        where x.Id == BlogID
                        select x.Posts).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }

            return resultList;
        }
    }
}
