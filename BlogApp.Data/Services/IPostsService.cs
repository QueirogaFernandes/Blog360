using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Services
{
    public interface IPostsService
    {
        // Admin
        IEnumerable<Post> GetAll();
        // Todos
        IEnumerable<Post> GetAllApproved();
        // Admin
        IEnumerable<Post> GetAllNotAproved();
        // Admin
        IEnumerable<Post> GetAllWaintingAprovel();
        IEnumerable<Post> GetAllNotAproved(int id);
        IEnumerable<Post> GetAllWaintingAprovel(int id);
        // Próprios Posts
        IEnumerable<Post> GetAllFromUser(int id);
        // Todos ?
        Post GetById(int id);
        // Todos
        Post Add(PostView post, int userId);
        // Admin ou...
        void Update(Post post, int userId);
        bool Delete(int id, int userId);
        bool Approve(int postId, int userId);
        bool NotApprove(int postId, int userId);
    }
}

