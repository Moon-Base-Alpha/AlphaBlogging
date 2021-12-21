using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaBlogging.Services
{
    public interface ICreateBlogsService
    {
        Task<CreateBlogVM> CreateBlogsAsync(CreateBlogVM cBlog);
    }
}
