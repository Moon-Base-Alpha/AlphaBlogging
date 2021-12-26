using AlphaBlogging.Models;
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
        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
            
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
        public Post GetPost(int Id)
        {
            return _db.Posts.FirstOrDefault(p => p.Id == Id);

        }

        public void UpdatePost(Post post)
        {
            _db.Posts.Update(post);
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
