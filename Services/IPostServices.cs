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
        Post GetPost(int id);
        IEnumerable<Post> GetBlogPosts(int? Id);
        List<Post> GetAllPosts();
        void AddPost(Post post/*, int blogId, Tag tag*/);
        void UpdatePost(Post post);
        void DeletePost(int id);
        void IncreaseLikesInPost(int Id);
        void DecreaseLikesInPost(int Id);
        IEnumerable<Post> GetPostsFromBlogID(int Id);
        public int GetViewsOfPost(int Id);
        Task<bool> SaveChangesAsync();
        IEnumerable<Post> GetPostsByDate(int id);
    }
}
