using System.ComponentModel.DataAnnotations;
using System;

namespace AlphaBlogging.Models.ViewModels
{
    public class CreateBlogVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32)]
        public string LastName { get; set; }


    }
}
