using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Blog
    {
        [Required]
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
        public Blog(int id, string title, string body, DateTime created, ApplicationUser author, ICollection<Post> posts, bool visible = true)
        {
            Id = id;
            Title = title;
            Body = body;
            Created = created;
            Author = author;
            Posts = posts;
            Visible = visible;
        }
    }
}
