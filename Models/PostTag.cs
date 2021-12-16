using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class PostTag
    {

        public int TagId { get; set; }

        [Required]
        public int PostId { get; set; }

        public PostTag()
        {

        }
    }
}
