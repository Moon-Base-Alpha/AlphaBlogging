
﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32)]
        public string LastName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(string firstName, string lastName, ICollection<Comment> comments, ICollection<Blog> blogs)
        {
            FirstName = firstName;
            LastName = lastName;
            Comments = comments;
            Blogs = blogs;
        }
    }
}
