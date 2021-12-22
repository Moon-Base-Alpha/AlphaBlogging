using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models.ViewModels
{
    public class CreatePostVM
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
        public bool Visible { get; set; }
        public int PostTagId { get; set; }

        [Required]
        public string HashTag { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
