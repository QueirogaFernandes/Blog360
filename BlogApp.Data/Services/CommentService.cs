using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogAppDbContext db;
        private readonly IUserService userService;
        private readonly IPostsService postsService;

        public CommentService(BlogAppDbContext db, IUserService userService, IPostsService postsService)
        {
            this.db = db;
            this.userService = userService;
            this.postsService = postsService;
        }
        public void Add(int postId, int userId, CommentView commentView)
        {
            var user = userService.GetById(userId);
            var post = postsService.GetById(postId);
            Comment comment = new Comment();

            if (user != null && post != null)
            {

                comment.Author = user;
                comment.Author_Id = user.Id;
                comment.Post = post;
                comment.Post_Id = post.Id;
                comment.Content = commentView.Content;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public bool Delete(int CommentId, int userId)
        {
            var comment = db.Comments.Find(CommentId);
            var user = db.Users.Find(userId);

            if (user != null && comment != null)
            {
                if (user.Role == Role.Admin || comment.Author_Id == user.Id)
                {
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
