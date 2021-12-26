using AlphaBlogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public interface IPostServices
    {
        Post GetPost(int Id);
        IEnumerable<Post> GetBlogPosts(int? Id);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int Id);
        IEnumerable<Post> GetPostsFromBlogID(int Id);

        Task<bool> SaveChangesAsync();
    }
}
