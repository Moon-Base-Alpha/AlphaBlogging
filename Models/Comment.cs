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

        public ApplicationUser Author { get; set; }

        public int PostId { get; set; }

        private Comment(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private Post _posts;

        private ILazyLoader LazyLoader { get; set; }

        [BackingField(nameof(_posts))]
        public Post Post
        {
            get => LazyLoader.Load(this, ref _posts);
            set => _posts = value;
        }

        public Comment()
        {
            
        }

        public Comment( string body, ApplicationUser author, int postId)

        {            
            Body = body;
            Created = DateTime.Now;
            Author = author;
            PostId = postId;
        }
    }
}
