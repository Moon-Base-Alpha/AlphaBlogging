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

        private Post(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        [Required]

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required, Display(Name = "Content")]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

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
        public Post()
        {
            Comments = new List<Comment>();
            //Tags = new List<Tag>();
            Created = DateTime.Now;
            Updated = DateTime.Now;
            Visible = true;
            Views = 0;
            Tags = new List<Tag>();
        }
        public Post(string title, string body, int blogId,  bool visible = true)
        {
               
            Title = title;
            Body = body;
            Created = DateTime.Now;
            Updated = DateTime.Now;

            Views = 0;
            Comments = new List<Comment>();
            Tags = new List<Tag>();             

            Visible = visible;
            BlogId = blogId;
            

        }
     
    }

   
}
