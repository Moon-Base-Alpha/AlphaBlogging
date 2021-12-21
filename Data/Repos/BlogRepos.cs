using AlphaBlogging.Models;
using System.Collections.Generic;
using System.Linq;


namespace AlphaBlogging.Data.Repos
{
    public class BlogRepos
    {
        //private readonly ApplicationDbContext _db;

        //public CreateBlogsService(ApplicationDbContext context)
        //{
        //    _db = context;
        //}

        //public static void CreateBlog(Blog newBlog)
        //{
          

        //    Blog blog = new Blog(newBlog.Title,newBlog.Body,User);
        //    //owner.Blogs.Add(blog);

        //    _db.Blogs.Add(blog);
        //    _db.SaveChanges(); 
            
        //    //ApplicationUser result = new ApplicationUser(newBlog.FirstName, newBlog.LastName, blog);

               
        //    //return result;
        //}



        //public static List<Blog> GetBlogs() // returns all blogs as a list
        //{
        //    var blogs = from x in _db.Blogs select x;

        //    return blogs.ToList();
        //}
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
