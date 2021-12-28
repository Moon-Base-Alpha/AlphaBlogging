using AlphaBlogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBlogging.Services
{
    public interface ITagServices
    {
        Tag GetTag(int id);
        
        List<Tag> GetAllTags();
        void AddTag(Tag tag);
       
        void DeleteTag(int id);
        IEnumerable<Tag> GetTagsFromPostID(int Id);
        Task<bool> SaveChangesAsync();
    }

}
