using AlphaBlogging.Models;
using AlphaBlogging.Models.ViewModels;
using AutoMapper;

namespace AlphaBlogging.Mappings
{
    public class CreateBlogMappingProfile : Profile
    {

        public CreateBlogMappingProfile()
        {
            CreateMap<CreateBlogVM, Blog>();
           
        }
    }
}
