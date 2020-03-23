using BlogApp.Data.Models;
using BlogApp.Data.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostsService postService;
        private readonly ICommentService commentService;

        public PostController(IPostsService postService, ICommentService commentService)
        {
            this.postService = postService;
            this.commentService = commentService;
        }
        // GET: Post
        public ActionResult Index()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var model2 = postService.GetAll();
                    return View(model2);
                }
            }
            if (Session["Id"] != null)
            {
                var model = postService.GetAllApproved();
                return View(model);
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult NotApproved()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var model = postService.GetAllNotAproved();
                    return View(model);
                } else
                {
                    if (Session["Id"] != null)
                    {
                        var model = postService.GetAllNotAproved((int)Session["Id"]);
                        return View(model);
                    }
                }
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult WaitingApproval()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var model = postService.GetAllWaintingAprovel();
                    return View(model);
                } else
                {
                    if (Session["Id"] != null)
                    {
                        var model = postService.GetAllWaintingAprovel((int)Session["Id"]);
                        return View(model);
                    }
                }
            } 
            return RedirectToAction("Login", "User");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetMyPosts()
        {
            if (Session["Id"] != null)
            {
                    var model = postService.GetAllFromUser((int) Session["Id"]);
                    return View(model);
            }
            return RedirectToAction("Login","User");
        }

        public ActionResult Details(int id)
        {
            var model = postService.GetById(id);
            if (model == null)
            {
                return View("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostView post, List<IFormFile> Image)
        {

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                if( file != null)
                {
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    post.Image = reader.ReadBytes((int) file.ContentLength);
                }
                var userId = (int) Session["Id"];
                postService.Add(post, userId);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Post model = postService.GetById(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if(Session["Role"].Equals("Admin") || model.Author_Id == (int) Session["Id"] && model.Status == Status.WaitingApproval)
            {
                PostView model2 = new PostView();
                model2.Content = model.Content;
                model2.Keywords = model.Keywords;
                model2.Title = model.Title;
                model2.Image = model.Image;
                model2.Id = model.Id;
                return View(model2);
                
            }
            TempData["MessageAlert"] = "You Can't Edit this Post!!";
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostView postView)
        {
            if (ModelState.IsValid)
            {
                Post post = postService.GetById(postView.Id);
                post.Content = postView.Content;
                post.Keywords = postView.Keywords;
                post.Title = postView.Title;
                postService.Update(post, (int) Session["Id"]);
                TempData["Message"] = "You have saved the Post!";
                return RedirectToAction("Details", new { id = post.Id });
            }
            return View(postView);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = postService.Delete(id, (int) Session["Id"]);

            if (result)
            {
                TempData["Message"] = "Post Deleted Successfully";
            }
            else
            {
                TempData["MessageAlert"] = "You are not Allowed to Delete that Post!!!!";
            }
            
            return Redirect(Request.UrlReferrer.ToString());

        }

        [HttpGet]
        public ActionResult Approve(int id)
        {
            var result = postService.Approve(id, (int)Session["Id"]);

            if (result)
            {
                TempData["Message"] = "Post Approved Successfully";
            }
            else
            {
                TempData["MessageAlert"] = "You are not Allowed to Approve that Post!!!!";
            }

            return Redirect(Request.UrlReferrer.ToString());

        }

        [HttpGet]
        public ActionResult NotApprove(int id)
        {
            var result = postService.NotApprove(id, (int)Session["Id"]);

            if (result)
            {
                TempData["Message"] = "Post Not Approved Successfully";
            }
            else
            {
                TempData["MessageAlert"] = "You are not Allowed to Not Approve that Post!!!!";
            }

            return Redirect(Request.UrlReferrer.ToString());

        }

        public ActionResult DeleteComment(int commentId)
        {
            var result = commentService.Delete(commentId, (int)Session["Id"]);

            if (result)
            {
                TempData["Message"] = "Comment Deleted Successfully";
            }
            else
            {
                TempData["MessageAlert"] = "You are not Allowed to Delete that Comment!!!!";
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult CreateComment(int postId)
        {
            var model = new CommentView { Post_Id = postId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CommentView commentView)
        {

            if (ModelState.IsValid)
            {
                var userId = (int) Session["Id"];
                commentService.Add(commentView.Post_Id, userId, commentView);
                return RedirectToAction("Index", "Post");
            }

            return View();
        }
    }
}