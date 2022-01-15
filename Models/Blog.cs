using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaBlogging.Models
{
    public class Blog
    {
        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, Display(Name = "Content")]
        public string Body { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string BlogImage { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }      

        public bool Visible { get; set; }
        private ICollection<Post> _posts;
       

        
      
        public virtual ICollection<Post> Posts { get; set; }
        
         public Blog()
        {

            Visible = true;

            Description = "";
            BlogImage = "earthrise.jpg";

        }

        public Blog(DateTime created)
        {
            Created = created;
        }



        public Blog(string title, string description, string body, string blogImage, IFormFile imageFile,  ApplicationUser author, bool visible = true)

        {
            Title = title;
            Description = description;
            Body = body;
            BlogImage = blogImage;
            ImageFile = imageFile;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            Author = author;
            //Posts = posts;
            Visible = true;
        }
       
    }
}
