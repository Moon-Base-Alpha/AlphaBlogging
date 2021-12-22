using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Blog
    {
        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public bool Visible { get; set; }
        public Blog()
        {

        }
        public Blog(string title, string body, ApplicationUser author, ICollection<Post> posts, bool visible = true)
        {
            Title = title;
            Body = body;
            Created = DateTime.Today;
            Author = author;
            Posts = posts;
            Visible = visible;
        }
        public Blog(string title, string body, ApplicationUser author, bool visible = true)
        {
            Title = title;
            Body = body;
            Created = DateTime.Today;
            Author = author;
            Visible = visible;
        }
    }
}
