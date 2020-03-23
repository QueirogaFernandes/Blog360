using BlogApp.Data.Models;
using BlogApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: User
        public ActionResult Index()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var model = userService.GetAll();
                    return View(model);
                }
            }
            return RedirectToAction("LogIn");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            if (ModelState.IsValid)
            {
                userService.Add(user);
                Session["Role"] = user.Role.ToString();
                Session["Id"] = user.Id;
                Session["UserName"] = user.UserName;
                return RedirectToAction("Index","Post");
            }

            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User user)
        {
           var userActual =  userService.LogIn(user);

            if(userActual != null)
            {
                Session["Role"] = userActual.Role.ToString();
                Session["Id"] = userActual.Id;
                Session["UserName"] = userActual.UserName;
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = userService.GetById(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                userService.Add(user);
            }
            return View();
        }
    }
}