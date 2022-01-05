using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
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

        [Required, Display(Name = "Content")]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }      

        public bool Visible { get; set; }
        private ICollection<Post> _posts;
       
        private Blog(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        public ICollection<Post> Posts
        {
            get => LazyLoader.Load(this, ref _posts);
            set => _posts = value;
        }
        public Blog()
        {

        }

        public Blog(DateTime created)
        {
            Created = created;
        }
        public Blog(string title, string body,ApplicationUser author, bool visible = true)
        {
            Title = title;
            Body = body;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            Author = author;
            //Posts = posts;
            Visible = visible;
        }
       
    }
}
