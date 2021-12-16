using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Blog
    {
        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public string Body { get; set; }
        public DateTime Created { get; set; }
        public ApplicationUser Author { get; set; } 
    }
}
