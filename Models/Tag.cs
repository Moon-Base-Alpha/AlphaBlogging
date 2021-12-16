using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public int PostTagId { get; set; }

        [Required]
        public string HashTag { get; set; }

        public Tag()
        {
                
        }
    }
}
