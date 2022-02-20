using Common.AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerAPI.Tools;
using ProjectManagerAPI.ViewModels.Projects;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromBody] GetVM model)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            model.Filter ??= new FilterVM();
            model.Pager ??= new PagerVM();
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? GlobalConstants.DefaultItemsPerPage
                                        : model.Pager.ItemsPerPage;
            model.Pager.Page = model.Pager.Page <= 0
                              ? GlobalConstants.DefaultPage
                              : model.Pager.Page;

            model.Filter.OwnerId = user.Id;
            var filter = model.Filter.GetFilter();

            ProjectsRepository projectsRepository = new ProjectsRepository();
            model.Items = Mapper.Instance().Map<List<ProjectDTO>>(projectsRepository.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage));

            ProjectToMembersRepository projectToMembersRepository = new ProjectToMembersRepository();
            List<int> memberProjectsId = projectToMembersRepository.GetAll().Where(i => i.MemberId == user.Id).Select(i => i.ProjectId).ToList();

            List<Project> memberProjects = projectsRepository.GetAll().Where(p => memberProjectsId.Contains(p.Id)).ToList();
            memberProjects.ForEach(p => model.Items.Add(Mapper.Instance().Map<ProjectDTO>(p)));
            model.Pager.PagesCount = (int)Math.Ceiling(model.Items.Count / (double)model.Pager.ItemsPerPage);

            return Ok(model.Items);
        }

        [HttpGet("{projectId}")]
        public IActionResult Get(int projectId)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            DetailsVM model = new DetailsVM();

            ProjectsRepository projectsRepo = new ProjectsRepository();
            model.ProjectId = projectId;
            model.Project = Mapper.Instance().Map<ProjectDTO>(projectsRepo.FirstOrDefault(i => i.Id == projectId));

            if (model.Project == null)
                return NotFound(model);

            return Ok(model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CreateVM model)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            ProjectsRepository projectsRepository = new ProjectsRepository();
            Project item = new Project();
            item.OwnerId = user.Id;
            item.Title = model.Title;
            item.Description = model.Description;

            projectsRepository.Save(item);

            return Ok(new { title = item.Title });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ProjectsRepository projectsRepository = new ProjectsRepository();
            Project item = projectsRepository.FirstOrDefault(p => p.Id == id);

            if (item != null)
            {
                projectsRepository.Delete(item);
                return Ok(item);
            }

            return NotFound(item);
        }
    }
}
