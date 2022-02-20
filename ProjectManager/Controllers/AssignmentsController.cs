using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerWeb.ActionFilters;
using ProjectManagerWeb.Extensions;
using ProjectManagerWeb.ViewModels.Assignments;

namespace ProjectManagerWeb.Controllers
{
    [Authentication]
    public class AssignmentsController : Controller
    {
        [HttpGet]
        public IActionResult Create(int projectId)
        {
            int loggedUserId = HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey).Id;
            CreateVM model = new CreateVM();
            model.ProjectId = projectId;
            UsersRepository usersRepository = new UsersRepository();
            model.Users = usersRepository.GetAll(u => u.Id!=loggedUserId);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AssignmentsRepository repo = new AssignmentsRepository();
            AssignmentDTO item = new AssignmentDTO();
            item.ProjectId = model.ProjectId;
            item.Title=model.Title;
            item.UserId = model.UserId;
            repo.Save(item);

            return RedirectToAction("Details", "Projects", new { ProjectId = model.ProjectId});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int loggedUserId = HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey).Id;
            AssignmentsRepository repo = new AssignmentsRepository();
            AssignmentDTO item = repo.FirstOrDefault(i => i.Id == id);

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Title = item.Title;
            model.ProjectId=item.ProjectId;
            model.UserId = item.UserId;
            UsersRepository usersRepository = new UsersRepository();
            model.Users = usersRepository.GetAll(u => u.Id != loggedUserId);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if(!ModelState.IsValid)
                return View(model);

            AssignmentsRepository repo =new AssignmentsRepository();
            AssignmentDTO item = new AssignmentDTO();
            item.Id = model.Id;
            item.ProjectId=model.ProjectId;
            item.Title = model.Title;
            item.UserId=model.UserId;

            repo.Save(item);

            return RedirectToAction("Details", "Projects", new { ProjectId = model.ProjectId });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            AssignmentsRepository repo = new AssignmentsRepository();
            AssignmentDTO item = repo.FirstOrDefault(i => i.Id == id);

            repo.Delete(item);

            return RedirectToAction("Details", "Projects", new { ProjectId = item.ProjectId });
        }
    }
}