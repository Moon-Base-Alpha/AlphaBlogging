using System.ComponentModel.DataAnnotations;
using System;

namespace AlphaBlogging.Models.ViewModels
{
    public class CreateCommentVM
    {
        public string Body { get; set; }

        public DateTime Created { get; set; }

        public int AuthorId { get; set; }

        public int PostId { get; set; }
    }
}
