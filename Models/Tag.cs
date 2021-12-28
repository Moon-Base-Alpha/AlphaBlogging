using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaBlogging.Models
{
    public class Tag
    {
       
        public int Id { get; set; }

       
        public string HashTag { get; set; }


        //public virtual ICollection<Post> Posts { get; set; }
       
        public Tag()
        {
                
        }
        private ICollection<Post> _posts;

        private Tag(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        public ICollection<Post> Posts
        {
            get => LazyLoader.Load(this, ref _posts);
            set => _posts = value;
        }
        public Tag(int id, string hashTag, ICollection<Post> posts)
        {
            Id = id;
            HashTag = hashTag;
            Posts = posts;
        }
    }
}
