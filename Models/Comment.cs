using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        
        public ApplicationUser Author { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public Comment()
        {

        }
        public Comment(string body, ApplicationUser author)
        {
            
            Body = body;
            Created = DateTime.Today;
            Author = author;
        }
    }
}
