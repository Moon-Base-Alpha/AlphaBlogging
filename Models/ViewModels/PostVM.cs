using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models.ViewModels
{
    public class PostVM
    {


        public string Title { get; set; }
        public string Body { get; set; }      
       
        public bool Visible { get; set; }
        public int BlogId { get; set; }
        public string HashTag { get; set; }

        public PostVM()
        {
            HashTag = "";   
        }
    }
}
