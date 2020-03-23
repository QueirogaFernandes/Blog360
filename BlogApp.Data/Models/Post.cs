using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public virtual User Author { get; set; }
        public string Keywords { get; set; }
        public byte[] Image { get; set; }
        public string ImageFolderPath { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Status Status { get; set; } = Status.WaitingApproval;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public virtual ICollection<Comment> Comments { get; set; }
        public int Author_Id { get; set; }
        public string AuthorUserName { get; set; }
    }
}
