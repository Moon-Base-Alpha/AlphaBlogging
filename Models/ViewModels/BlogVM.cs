using System;
using System.Collections.Generic;

namespace AlphaBlogging.Models.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; } 
        public string Title { get; set; }     
        public string Body { get; set; }    
        public DateTime Created { get; set; }    
        public ApplicationUser Author { get; set; }
        public  List<PostVM> Posts { get; set; }
        public bool Visible { get; set; }
    }
}
