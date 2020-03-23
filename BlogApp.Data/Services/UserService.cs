using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Services
{
    public class UserService : IUserService
    {
        private readonly BlogAppDbContext db;

        public UserService(BlogAppDbContext db)
        {
            this.db = db;
        }       
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include("Posts");
        }

        public User GetById(int id)
        {
            return db.Users.FirstOrDefault(user => user.Id == id);
        }

        public bool LogIn(string userName, string passWord)
        {
            var users = this.GetAll();

            foreach (var user in users)
            {
                if (user.PassWord == passWord && user.UserName == userName)
                {
                    var x = user.Posts;
                    return true;
                }
            }

            return false;
        }

        public User LogIn(User user)
        {
            var users = this.GetAll();

            foreach (var userActual in users)
            {
                if (user.PassWord == userActual.PassWord && user.UserName == userActual.UserName)
                {
                    return userActual;
                }
            }

            return null;
        }

        public void Update(User user)
        {
            var entry = db.Entry(user);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
