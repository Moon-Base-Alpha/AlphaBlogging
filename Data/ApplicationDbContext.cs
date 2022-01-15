using AlphaBlogging.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaBlogging.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
       

        //public DbSet<Like> Likes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PostTag>(
        //        eb =>
        //        {
        //            eb.HasNoKey();
        //            eb.ToView("Post");
        //            eb.Property(v => v.TagsId).HasColumnName("Title");
        //        });

        //}
        //public DbSet<PostTag> PostTag { get; set; }
    }
}
