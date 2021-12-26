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

        [Required]
        public ApplicationUser Author { get; set; }

        //public int PostId { get; set; }

       // public virtual Post Post { get; set; }

        public Comment()
        {

        }
        public Comment(int id, string body, DateTime created, ApplicationUser author)
        {
            Id = id;
            Body = body;
            Created = created;
            Author = author;
        }
    }
}
