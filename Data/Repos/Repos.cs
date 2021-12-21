using AlphaBlogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public class Repos : IRepos
    {
        private ApplicationDbContext _db;
        public Repos(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddPost(Post post)
        {
            _db.Add(post);
            
        }

        public void DeletePost(int id)
        {
            _db.Posts.Remove(GetPost(id));
        }

        public List<Post> GetAllPosts()
        {
            return _db.Posts.ToList();

        }

        public Post GetPost(int id)
        {
            return _db.Posts.FirstOrDefault(p => p.Id == id);

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
