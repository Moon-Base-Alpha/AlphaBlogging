using AlphaBlogging.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlphaBlogging.Data.Repos
{
    public class BlogRepos
    { 
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
        public BlogRepos(ApplicationDbContext aaa)
        {
            _context = aaa;
        }

        public static List<Blog> GetBlogs() // returns all blogs as a list
        {
            var blogs = from x in _context.Blogs select x;

            return blogs.ToList();
        }
        //public static void SeedVarious() uncomment this later!!!
        //{
        //    UserManager<ApplicationUser> UMAU = new UserManager<ApplicationUser>();
        //    UMAU.CreateAsync(,);


        //    ApplicationUser testuser = new UserManager<ApplicationUser>();
        //    _context.Users.Add(testuser);
        //    ICollection<Post> posts = new List<Post>();

        //    _context.Blogs.Add(new Blog("Blogtitle 1", "body string asdjifa", testuser, posts, true));
        //    _context.Blogs.Add(new Blog("Blogtitle 2", "body string asdjifa", testuser, posts, true));
        //    _context.Blogs.Add(new Blog("Blogtitle 3", "body string asdjifa", testuser, posts, true));


        //}

    }
}
