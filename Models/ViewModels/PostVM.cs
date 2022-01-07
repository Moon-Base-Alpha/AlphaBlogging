﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models.ViewModels
{
    public class PostVM
    {

        [Required]
        public string Title { get; set; }

        [Required, Display(Name = "Content")]
        public string Body { get; set; }      
       
        public bool Visible { get; set; }
        public int BlogId { get; set; }
        public string HashTag { get; set; }

        public DateTime Updated { get; set; }

        public PostVM()
        {
            HashTag = "";   
        }
    }
}
