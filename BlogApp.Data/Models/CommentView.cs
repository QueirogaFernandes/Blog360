using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
    public class CommentView
    {
        public string Content { get; set; }
        public int Post_Id { get; set; }
    }
}
