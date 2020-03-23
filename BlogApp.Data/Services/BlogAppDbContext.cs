using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Services
{
    public class BlogAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasRequired<User>(s => s.Author)
                .WithMany(g => g.Posts);
            modelBuilder.Entity<Comment>()
                .HasRequired<Post>(s => s.Post)
                .WithMany(g => g.Comments);
        }
    }
}
