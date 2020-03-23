using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public Post Post { get; set; }
        public int Author_Id { get; set; }
        public int Post_Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
