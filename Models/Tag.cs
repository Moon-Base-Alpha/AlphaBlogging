using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Tag
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string HashTag { get; set; }

        [Required]
        public virtual ICollection<Post> Posts { get; set; }

        public Tag()
        {
                
        }

        public Tag(int id, string hashTag, ICollection<Post> posts)
        {
            Id = id;
            HashTag = hashTag;
            Posts = posts;
        }
    }
}
