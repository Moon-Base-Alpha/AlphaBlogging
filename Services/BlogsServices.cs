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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AlphaBlogging.Services
{
    public class BlogsServices : IBlogsServices
    {
        private ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogsServices(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _db = context;
            _webHostEnvironment = hostEnvironment;
        }
        
        public void AddBlog(Blog blog)
        {
               
            _db.Blogs.Add(blog);
        }

        public async void DeleteBlog(int id)
        {
            //var imageModel = await _db.Blogs.FindAsync(id);

            //delete image from wwwroot/image
            //var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", imageModel.BlogImage);
            //if (System.IO.File.Exists(imagePath))
            //    System.IO.File.Delete(imagePath);
            _db.Blogs.Remove(GetBlog(id));
        }

        public List<Blog> GetAllBlogs()
        {           

            return _db.Blogs.OrderByDescending(b=>b.Updated).Take(5).ToList();
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
            // returning a blog with these includes ensures availability of everythng needed
            var q1 = _db.Blogs.Where(b=>b.Id == id)
                .Include(b=>b.Posts).ThenInclude(p=>p.Comments).ThenInclude(c=>c.Author)
                .Include(b=>b.Posts).ThenInclude(p=>p.Tags)
                .Include(b=>b.Author)
                .FirstOrDefault();
            return q1;
        }

        public void UpdateBlog(Blog blog)        {
            
            blog.Updated = DateTime.Now;
            blog.Created = (from x in _db.Blogs
                            where x.Id == blog.Id
                            select x.Created).First();

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
        public async void AddImage(Blog blog)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(blog.ImageFile.FileName);
            string extension = Path.GetExtension(blog.ImageFile.FileName);
            blog.BlogImage = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //string path = Path.Combine(wwwRootPath + "/image/", fileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\image", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await blog.ImageFile.CopyToAsync(fileStream);
            }
           
            return;
        }
        public async void DeleteImage(string oldImage)
        {
            var imageModel = await _db.Blogs.FindAsync(oldImage);
            //delete image from wwwroot/image
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", oldImage);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);            
        }
        public void IncreaseViewCountOfAllPostsInBlog(int Id)
        {
            //Post post = (from x in _db.Posts
            //            where x.Id == Id
            //            select x).SingleOrDefault();


            var blogs = _db.Blogs.Find(Id);
            foreach (var item in blogs.Posts)
            {
                item.Views++;
            }
            _db.Update(blogs);
            _db.SaveChangesAsync();

        }
        public IEnumerable<Post> GetPostsByDate()
        {
            //List<Post> posts = new List<Post>();

            return _db.Posts.ToList().OrderBy(p => p.Created);
        }

        public List<Post> GetLatestPostFromAllBlogs()
        {
            var result = (from x in _db.Blogs
                          select x).ToList();

            List<Post> postsList = new List<Post>();
            foreach (var item in result)
            {
                if (item.Posts.LastOrDefault() != null)
                {
                    postsList.Add(item.Posts.LastOrDefault()); // maybe solve this with LINQ instead?
                }
            }

            return postsList;
        }
    }
}
