using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerWeb.ActionFilters;
using ProjectManagerWeb.Extensions;
using ProjectManagerWeb.ViewModels.Home;
using System;

namespace ProjectManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string url)
        {
            if (this.HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey) != null)
                return RedirectToAction("Index", "Home");

            LoginVM model = new LoginVM();
            model.Url = url;

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (this.HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey) != null)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            UsersRepository repo = new UsersRepository();
            User loggedUser = repo.FirstOrDefault(u =>
                                                    u.Username == model.Username &&
                                                    u.Password == model.Password);

            if (loggedUser == null)
            {
                ModelState.AddModelError("authError", "Incorrect username or password");
                return View(model);
            }

            this.HttpContext.Session.SetObject(GlobalConstants.LoggedUserKey, loggedUser);

            return Redirect(String.IsNullOrEmpty(model.Url) ? "/Home/Index" : model.Url);
        }

        [Authentication]
        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove(GlobalConstants.LoggedUserKey);

            return RedirectToAction("Login", "Home");
        }
    }
}
