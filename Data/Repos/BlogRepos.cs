using AlphaBlogging.Models;

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

    }
}
