using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BlogApp.Data.Models
{
    public class PostView
    {
        [Required]
        public string Title { get; set; }
        public string Keywords { get; set; }
        public byte[] Image { get; set; }
        public HttpPostedFile Image2 { get; set; }
        public string ImageFolderPath { get; set; }
        [Required]
        public string Content { get; set; }
        public int Id { get; set; }
    }
}
