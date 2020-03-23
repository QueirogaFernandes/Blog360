using BlogApp.Data.Models;
using BlogApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int postId)
        {
            var model = new CommentView { Post_Id = postId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentView commentView)
        {

            if (ModelState.IsValid)
            {
                var userId = (int)Session["Id"];
                commentService.Add(commentView.Post_Id, userId, commentView);
                return RedirectToAction("Index","Post");
            }

            return View();
        }
    }
}