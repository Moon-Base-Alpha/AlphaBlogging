using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required, Display(Name = "Content")]
        [StringLength(512)]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int PostId { get; set; }

        public string PostTitle { get; set; } 

        public virtual Post Post { get; set; }

        public Comment()
        {
            
        }

        public Comment(string body, ApplicationUser author, int postId, string postTitle)
        {            
            Body = body;
            Created = DateTime.Now;
            Author = author;
            PostId = postId;
            PostTitle = postTitle;
        }
    }
}
