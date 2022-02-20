using Common.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerWeb.ActionFilters;
using ProjectManagerWeb.ViewModels.Shared;
using ProjectManagerWeb.ViewModels.Users;
using System;
using System.Linq;

namespace ProjectManagerWeb.Controllers
{
    [Authentication]
    public class UsersController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            model.Filter ??= new FilterVM();
            model.Pager ??= new PagerVM();
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0
                                ? 1
                                : model.Pager.Page;

            var filter = model.Filter.GetFilter();

            UsersRepository repo = new UsersRepository();
            model.Items = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)model.Pager.ItemsPerPage);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UsersRepository repo = new UsersRepository();
            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            repo.Save(item);

            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            UsersRepository repo = new UsersRepository();
            User item = repo.FirstOrDefault(u => u.Id == id);

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UsersRepository repo = new UsersRepository();
            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            repo.Save(item);

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Delete(int id)
        {
            UsersRepository repo = new UsersRepository();
            User item = new User();
            item.Id = id;

            repo.Delete(item);

            return RedirectToAction("Index", "Users");
        }
    }
}
