using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models.ViewModels
{
    public class PostVM
    {

       
        public int Id { get; set; }      
        public string Title { get; set; }        
        public string Body { get; set; }      
        public DateTime Created { get; set; }
        public int Views { get; set; }
        public bool Visible { get; set; }
        public int BlogId { get; set; }
        //public int PostTagId { get; set; }             
        //public string HashTag { get; set; }


    }
}
