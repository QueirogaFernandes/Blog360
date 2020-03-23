using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Services
{
    public interface ICommentService
    {
        void Add(int postId, int userId, CommentView commentView);
        bool Delete(int CommentId, int userId);
    }
}
