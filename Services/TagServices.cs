using AlphaBlogging.Models;
using AlphaBlogging.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public class TagServices : ITagServices
    {

        private ApplicationDbContext _db;
        public TagServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Tag> Tags { get;}

       
        public void AddTag(Tag tag)
        {
            //if (!_db.Tags.Any(t=>t.HashTag == tag.HashTag))
            //{
            //    _db.Tags.Add(tag);
            //}
            _db.Tags.Add(tag);
            
        }
        public Tag FindTag(string tagName)
        {
           return _db.Tags.Where(t=>t.HashTag == tagName).FirstOrDefault();   
        } 


        public void DeleteTag(int id)
        {
            _db.Tags.Remove(GetTag(id));
        }

        public List<Tag> GetAllTags()
        {
            return _db.Tags.ToList();

        }
        
        public Tag GetTag(int id)
        {
            return GetTag(id);
            //return _db.Tags.FirstOrDefault(t => t.Id == id);
            //return _db.Blogs.Where(b => b.Id == id).Include(b => b.Posts).ThenInclude(p => p.Tags).ThenInclude(t=>t.HashTag).FirstOrDefault();
        }
        public IEnumerable<Tag> GetTagsFromPostID(int Id) // returns all tags as a list
        {
            List<Tag> resultList = new List<Tag>();
            
            var temp = (from p in _db.Posts
                        where p.Id == Id
                        select p.Tags).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }

            return resultList;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;           
        }
    }
}
