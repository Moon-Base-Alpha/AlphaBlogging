using System.ComponentModel.DataAnnotations;
using System;

namespace AlphaBlogging.Models.ViewModels
{
    public class CreateCommentVM
    {
        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
