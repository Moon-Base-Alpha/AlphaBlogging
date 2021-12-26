using AlphaBlogging.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlphaBlogging.Data.Repos
{
    public class BlogServices
    {
        public BlogServices(ApplicationDbContext aaa)
        {
            _context = aaa;
        }

        public static ApplicationUser CreateBlog(ApplicationUser newBlogger)
        {
            ApplicationUser result = new ApplicationUser();

            result.FirstName = newBlogger.FirstName;
            result.LastName = newBlogger.LastName;
            result.Email = newBlogger.Email;
            result.Blogs = newBlogger.Blogs;

            return result;
        }
        private static ApplicationDbContext _context;


        public static List<Blog> GetBlogs() // returns all blogs as a list
        {
            var blogs = from x in _context.Blogs select x;

            return blogs.ToList();
        }
        public static List<Blog> GetBlogsFromUserID(string UserID) // returns all blogs as a list
        {
            List<Blog> resultList = new List<Blog>();

            var temp = (from x in _context.Users
                        where x.Id == UserID
                        select x.Blogs).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }

            return resultList;
        }
        public List<Post> GetPostsFromBlogID(int BlogID) // returns all blogs as a list
        {
            List<Post> resultList = new List<Post>();

            var temp = (from x in _context.Blogs
                        where x.Id == BlogID
                        select x.Posts).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }

            return resultList;
        }
        public static List<Comment> GetCommentsFromPostID(int PostID) // returns all blogs as a list
        {
            List<Comment> resultList = new List<Comment>();

            var temp = (from x in _context.Posts
                        where x.Id == PostID
                        select x.Comments).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }

            return resultList;
        }
    }



}

