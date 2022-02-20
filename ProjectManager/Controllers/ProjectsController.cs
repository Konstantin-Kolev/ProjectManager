using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerWeb.ActionFilters;
using ProjectManagerWeb.Extensions;
using ProjectManagerWeb.ViewModels.Projects;
using ProjectManagerWeb.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerWeb.Controllers
{
    [Authentication]
    public class ProjectsController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            int loggedUser = HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey).Id;

            model.Filter ??= new FilterVM();
            model.Pager ??= new PagerVM();
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0
                                ? 1
                                : model.Pager.Page;

            model.Filter.OwnerId = loggedUser;
            var filter = model.Filter.GetFilter();

            ProjectsRepository projectsRepo = new ProjectsRepository();
            model.Items = projectsRepo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            
            ProjectToMembersRepository projectToMembersRepository = new ProjectToMembersRepository();
            List<int> memberProjectsId = projectToMembersRepository.GetAll().Where(i => i.MemberId == loggedUser).Select(i => i.ProjectId).ToList();

            List<Project>memberProjects = projectsRepo.GetAll(p => memberProjectsId.Contains(p.Id));
            memberProjects.ForEach(p => model.Items.Add(p));
            model.Pager.PagesCount = (int)Math.Ceiling(model.Items.Count / (double)model.Pager.ItemsPerPage);

            return View(model);
        }

        public IActionResult Details(DetailsVM model)
        {
            int loggedUser = HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey).Id;

            model.Filter ??= new ViewModels.Assignments.FilterVM();

            model.Pager ??= new PagerVM();
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? GlobalConstants.DefaultItemsPerPage
                                        : model.Pager.ItemsPerPage;
            model.Pager.Page = model.Pager.Page <= 0
                                ? GlobalConstants.DefaultPage
                                : model.Pager.Page;

            ProjectsRepository projectsRepo = new ProjectsRepository();
            model.Project = projectsRepo.FirstOrDefault(i => i.Id == model.ProjectId);

            if (model.Project == null)
                return RedirectToAction("Index", "Projects");

            model.Filter.ProejctId = model.ProjectId;
            var filter = model.Filter.GetFitler();

            AssignmentsRepository assignmentsRepo = new AssignmentsRepository();
            model.Items = assignmentsRepo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);

            ProjectToMembersRepository projectToMembersRepo = new ProjectToMembersRepository();
            model.Members = projectToMembersRepo.GetAll(i => i.ProjectId == model.Project.Id);

            List<int?> memberUsers = model.Members.Select(i => i.MemberId).ToList();
            memberUsers.Add(loggedUser);

            UsersRepository usersRepo = new UsersRepository();
            model.Users = usersRepo.GetAll(u => !memberUsers.Contains(u.Id));

            return View(model);
        }

        [HttpPost]
        public IActionResult AddMembers(int[] usersId, int projectId)
        {
            ProjectToMembersRepository projectToMembersRepo = new ProjectToMembersRepository();
            foreach (int id in usersId)
            {
                ProjectToMember item = new ProjectToMember();
                item.ProjectId = projectId;
                item.MemberId = id;

                projectToMembersRepo.Save(item);
            }

            return RedirectToAction("Details", "Projects", new { ProjectId = projectId });
        }

        public IActionResult RemoveMember(int id)
        {
            ProjectToMembersRepository projectToMembersRepository = new ProjectToMembersRepository();
            ProjectToMember item = projectToMembersRepository.FirstOrDefault(i => i.Id == id);

            projectToMembersRepository.Delete(item);

            return RedirectToAction("Details", "Projects", new { ProjectId = item.ProjectId });
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

            ProjectsRepository repo = new ProjectsRepository();

            int loggedUserId = this.HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey).Id;

            Project item = new Project();
            item.OwnerId = loggedUserId;
            item.Title = model.Title;
            item.Description = model.Description;

            repo.Save(item);

            return RedirectToAction("Index", "Projects");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProjectsRepository repo = new ProjectsRepository();
            Project item = repo.FirstOrDefault(i => i.Id == id);
            EditVM model = new EditVM();

            model.Id = item.Id;
            model.OwnerId = item.OwnerId;
            model.Title = item.Title;
            model.Description = item.Description;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ProjectsRepository repo = new ProjectsRepository();

            int loggedUserId = this.HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey).Id;
            Project item = repo.FirstOrDefault(i => i.Id == model.Id);

            if (model == null || item.OwnerId != loggedUserId)
                return RedirectToAction("Index", "Projects");

            item.OwnerId = loggedUserId;
            item.Title = model.Title;
            item.Description = model.Description;

            repo.Save(item);

            return RedirectToAction("Index", "Projects");
        }

        public IActionResult Delete(int id)
        {
            ProjectsRepository repo = new ProjectsRepository();
            Project item = new Project();
            item.Id = id;

            repo.Delete(item);

            return RedirectToAction("Index", "Projects");
        }
    }
}
