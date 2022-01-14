using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Tag
    {
       
        public int Id { get; set; }

       
        public string HashTag { get; set; }


        //public virtual ICollection<Post> Posts { get; set; }
       
        public Tag()
        {
                
        }
        
        public virtual ICollection<Post> Posts { get; set; }
        public Tag(int id, string hashTag, ICollection<Post> posts)
        {
            
            HashTag = hashTag;
            Posts = posts;
        }
    }
}
