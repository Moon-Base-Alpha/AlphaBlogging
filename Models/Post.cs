using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Post
    {
        private Blog _blog;
        public Post()
        {

        }
        private Post(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public int BlogId { get; set; }

        public int Views { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public bool Visible { get; set; }
        public Blog Blog
        {
            get => LazyLoader.Load(this, ref _blog);
            set => _blog = value;
        }

        public Post(int id, string title, string body, DateTime created, int blogId, int views, ICollection<Comment> comments, ICollection<Tag> tags, bool visible = true)
        {
            Id = id;
            Title = title;
            Body = body;
            Created = created;
            Views = views;
            Comments = comments;
            Tags = tags;
            Visible = visible;
            BlogId = blogId;    
        }
    }
}
