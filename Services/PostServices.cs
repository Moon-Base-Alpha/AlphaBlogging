﻿using AlphaBlogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public class PostServices : IPostServices
    {

        private ApplicationDbContext _db;
        public PostServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Post> Posts { get;}

        public IEnumerable<Post> GetPostsByDate(int id)
        {
            //List<Post> posts = new List<Post>();
                      
            return _db.Posts.Where(p=>p.Id == id).ToList().OrderBy(p=>p.Created);
        }

        public IEnumerable<Post> GetPostsFromBlogID(int Id) // returns all blogs as a list
        {
            List<Post> resultList = new List<Post>();
            
            var temp = (from x in _db.Blogs 
                        where x.Id == Id
                        select x.Posts).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }

            return resultList;
        }
        public void AddPost(Post post/*, int blogId, Tag tag*/)
        {
            (_db.Blogs.Where(b => b.Id == post.BlogId).FirstOrDefault()).Posts.Add(post);
            //_db.Posts.Add(post);
            //_db.Tags.Add(tag);
            
        }

        public void DeletePost(int id)
        {
            _db.Posts.Remove(GetPost(id));
        }

        public List<Post> GetAllPosts()
        {
            return _db.Posts.ToList();

        }
        public IEnumerable<Post> GetBlogPosts(int? Id)
        {
            return _db.Posts.Where(p => p.BlogId == Id).ToList();
            
        }
        public Post GetPost(int id)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == id);
            return post;

        }

        public void UpdatePost(Post post)
        {
            post.Updated = DateTime.Now;
            post.Created = (from x in _db.Posts
                            where x.Id == post.Id
                            select x.Created).First();
            _db.Posts.Update(post);
        }
        public void IncreaseLikesInPost(int Id)
        {
            var qPost = GetPost(Id);
            qPost.Likes++;
            _db.Posts.Update(qPost);
        }
        public void DecreaseLikesInPost(int Id)
        {
            var qPost = GetPost(Id);
            qPost.Likes--;
            _db.Posts.Update(qPost);
        }


        public async Task<bool> SaveChangesAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;           
        }
        

        public int GetViewsOfPost(int Id)
        {
            var post = _db.Posts.Find(Id);
            return post.Views;
        }

        public void IncreaseViewCountOfPost(int id)
        {
            var post = _db.Posts.Find(id);
            post.Likes++;
            _db.Posts.Update(post);
        }
    }
}
