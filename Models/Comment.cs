using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
