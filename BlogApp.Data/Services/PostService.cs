using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Services
{
    public class PostService : IPostsService
    {
        private readonly BlogAppDbContext db;
        private readonly IUserService userService;

        public PostService(BlogAppDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }
        public void Add(PostView postView, int userId)
        {
            var user = userService.GetById(userId);

            Post post = new Post();

            if(user != null)
            {
                post.Author = user;
                post.AuthorUserName = user.UserName;
                post.Title = postView.Title;
                post.Keywords = postView.Keywords;
                post.Image = postView.Image;
                post.Content = postView.Content;
                post.Author_Id = user.Id;
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public bool Approve(int postId, int userId)
        {
            var user = db.Users.Find(userId);
            var post = db.Posts.Find(postId);

            if (user != null && post != null)
            {
                if (user.Role == Role.Admin)
                {
                    post.Status = Status.Approved;
                    var entry = db.Entry(post);
                    entry.State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id, int userId)
        {
            var post = db.Posts.Find(id);
            var user = db.Users.Find(userId);

            if( user != null && post != null)
            {
                if( user.Role == Role.Admin || post.Status != Status.Approved && post.Author_Id == user.Id)
                {
                    db.Posts.Remove(post);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Post> GetAll()
        {
            return from post in db.Posts
                   orderby post.DateTime
                   select post;
        }

        public IEnumerable<Post> GetAllApproved()
        {
            return from post in db.Posts
                   where post.Status == Status.Approved
                   orderby post.DateTime
                   select post;
        }

        public IEnumerable<Post> GetAllFromUser(int id)
        {
            var user = userService.GetById(id);
            var posts = from post in db.Posts
                        where post.Author_Id == id
                        orderby post.DateTime
                        select post;
            return posts;
        }

        public IEnumerable<Post> GetAllNotAproved()
        {
            return from post in db.Posts
                   where post.Status == Status.NotApproved
                   orderby post.DateTime ascending
                   select post;
        }

        public IEnumerable<Post> GetAllNotAproved(int id)
        {
            return from post in db.Posts
                   where post.Status == Status.NotApproved && post.Author_Id == id
                   orderby post.DateTime ascending
                   select post;
        }

        public IEnumerable<Post> GetAllWaintingAprovel()
        {
            return from post in db.Posts
                   where post.Status == Status.WaitingApproval
                   orderby post.DateTime ascending
                   select post;
        }

        public IEnumerable<Post> GetAllWaintingAprovel(int id)
        {
            return from post in db.Posts
                   where post.Status == Status.WaitingApproval && post.Author_Id == id
                   orderby post.DateTime ascending
                   select post;
        }

        public Post GetById(int id)
        {
            return db.Posts.Include("Comments").FirstOrDefault(r => r.Id == id);

        }

        public bool NotApprove(int postId, int userId)
        {
            var user = db.Users.Find(userId);
            var post = db.Posts.Find(postId);

            if (user != null && post != null)
            {
                if (user.Role == Role.Admin)
                {
                    post.Status = Status.NotApproved;
                    var entry = db.Entry(post);
                    entry.State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void Update(Post post)
        {
            var entry = db.Entry(post);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Update(Post post, int userId)
        {
            var user = db.Users.Find(userId);

            if (user != null && post != null)
            {
                if (user.Role == Role.Admin || post.Status == Status.WaitingApproval && post.Author_Id == user.Id)
                {
                    var entry = db.Entry(post);
                    entry.State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            
        }
    }
}
