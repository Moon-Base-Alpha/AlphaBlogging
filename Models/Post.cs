﻿using Microsoft.AspNetCore.Authorization;
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
        public uint Likes { get; set; }
        public bool Visible { get; set; }
        public virtual Blog Blog { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            //Tags = new List<Tag>();
            Created = DateTime.Now;
            Updated = DateTime.Now;
            Visible = true;
            Views = 0;
            Tags = new List<Tag>();
            Likes = 0;
        }

        public Post(string title, string body, int blogId)

        {
               
            Title = title;
            Body = body;
            Created = DateTime.Now;
            Updated = DateTime.Now;

            Views = 0;
            Comments = new List<Comment>();
            Tags = new List<Tag>();             

            Visible = true;
            BlogId = blogId;
            Likes = 0;


        }

    }

   
}
