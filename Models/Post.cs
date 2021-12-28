using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

        public Post(string title, string body, int blogId, int views, ICollection<Comment> comments, ICollection<Tag> tags, bool visible = true)
        {
            
            Title = title;
            Body = body;
            Created = DateTime.Today;
            Views = views;
            Comments = comments;
            Tags = tags;
            Visible = visible;
            BlogId = blogId;    
        }
    }

    [Keyless]
    public class PostTag
    {
        public int PostsId { get; set; }
        public Post Post { get; set; }
        public int TagsId { get; set; }
        public Tag Tag { get; set; }

    }
}
