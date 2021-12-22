using AlphaBlogging.Data;
using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Services
{
    public class BlogsService : IBlogsService
    {
        private ApplicationDbContext _db;
        public BlogsService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddBlog(Blog blog)
        {
            _db.Add(blog);

        }

        public void DeleteBlog(int id)
        {
            _db.Blogs.Remove(GetBlog(id));
        }

        public List<Blog> GetAllBlogs()
        {
            return _db.Blogs.ToList();

        }

        public Blog GetBlog(int id)
        {
            return _db.Blogs.FirstOrDefault(b => b.Id == id);

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
    }
}
