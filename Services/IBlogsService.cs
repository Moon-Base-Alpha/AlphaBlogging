using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaBlogging.Services
{
    public interface IBlogsService
    {

        Blog GetBlog(int Id);
        List<Blog> GetAllBlogs();
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(int Id);

        Task<bool> SaveChangesAsync();
        
    }
}
