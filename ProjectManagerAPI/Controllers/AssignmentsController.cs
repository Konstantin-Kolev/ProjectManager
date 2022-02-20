using Common.AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerAPI.Tools;
using ProjectManagerAPI.ViewModels.Assignments;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        [HttpPut]
        public IActionResult Put([FromBody] CreateVM model)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            AssignmentsRepository repo = new AssignmentsRepository();
            Assignment item = new Assignment();
            item.ProjectId = model.ProjectId;
            item.Title = model.Title;
            item.Description = model.Description;
            item.UserId = model.UserId;
            item.CreatorId = user.Id;
            item.CreatedOn = DateTime.Now;
            repo.Save(item);

            return Ok(item);
        }

        [HttpGet("{assignmentId}")]
        public IActionResult Get(int assignmentId)
        {
            AssignmentsRepository repo = new AssignmentsRepository();
            DetailsVM model = new DetailsVM();

            AssignmentDetailsDTO assignment = Mapper.Instance().Map<AssignmentDetailsDTO>(repo.FirstOrDefault(a => a.Id == assignmentId));

            if (assignment == null)
                return NotFound(assignment);

            model.AssignmentId = assignment.Id;
            model.Assignment = assignment;
            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get([FromBody]PagerVM pager, int projectId)
        {
            pager ??= new PagerVM();
            pager.Page = pager.Page <= 0
                ? GlobalConstants.DefaultPage
                : pager.Page;
            pager.ItemsPerPage = pager.ItemsPerPage <= 0
                ? GlobalConstants.DefaultItemsPerPage
                : pager.ItemsPerPage;

            AssignmentsRepository assignmentsRepository = new AssignmentsRepository();
            GetVM model = new GetVM();
            model.Items = Mapper.Instance().Map<List<AssignmentDTO>>(assignmentsRepository.GetAll(a => a.ProjectId == projectId,
                                                                            pager.Page,
                                                                            pager.ItemsPerPage));
            model.Pager = pager;
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AssignmentsRepository assignmentsRepository = new AssignmentsRepository();
            Assignment item = assignmentsRepository.FirstOrDefault(a => a.Id == id);

            if (item != null)
            {
                assignmentsRepository.Delete(item);
                return Ok(item);
            }

            return NotFound(item);
        }
    }
}
