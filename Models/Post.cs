using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public int Views { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public bool Visible { get; set; }

        public Post()
        {

        }

        public Post(int id, string title, string body, DateTime created, int views, ICollection<Comment> comments, ICollection<Tag> tags, bool visible)
        {
            Id = id;
            Title = title;
            Body = body;
            Created = created;
            Views = views;
            Comments = comments;
            Tags = tags;
            Visible = visible;
        }
    }
}
