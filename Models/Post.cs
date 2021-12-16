using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public int Views { get; set; }

        public Post()
        {

        }

    }
}
