using Common.AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerAPI.Tools;
using ProjectManagerAPI.ViewModels.AssignmentComments;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentCommentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromBody] PagerVM pager, int assignmentId)
        {
            pager ??= new PagerVM();
            pager.Page = pager.Page <= 0
                                        ? GlobalConstants.DefaultPage
                                        : pager.Page;

            pager.ItemsPerPage = pager.ItemsPerPage <= 0
                                                ? GlobalConstants.DefaultItemsPerPage
                                                : pager.ItemsPerPage;

            AssignmentCommentsRepository assignmentCommentsRepository = new AssignmentCommentsRepository();
            GetVM model = new GetVM();
            model.Items = Mapper.Instance().Map<List<CommentDTO>>(assignmentCommentsRepository.GetAll(c => c.AssignmentId == assignmentId,
                                                                                                       pager.Page,
                                                                                                       pager.ItemsPerPage));
            model.Pager = pager;
            return Ok(model);

        }

        [HttpPut]
        public IActionResult Put([FromBody] CreateVM model)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            AssignmentCommentsRepository repo = new AssignmentCommentsRepository();
            AssignmentComment item = new AssignmentComment();

            item.Content = model.Content;
            item.AssignmentId = model.AssignmentId;
            item.CreatorId = user.Id;
            item.CreatedOn = DateTime.Now;
            item.UpdatedOn = DateTime.Now;

            repo.Save(item);
            return Ok(item);
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] EditVM model)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            AssignmentCommentsRepository assignmentCommentsRepository = new AssignmentCommentsRepository();
            AssignmentComment item = assignmentCommentsRepository.FirstOrDefault(c => c.Id == model.Id && c.CreatorId == user.Id);
            if (item == null)
                return NotFound();

            item.Content = model.Content;
            item.UpdatedOn = DateTime.Now;

            assignmentCommentsRepository.Save(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AssignmentCommentsRepository assignmentCommentsRepository = new AssignmentCommentsRepository();
            AssignmentComment item = assignmentCommentsRepository.FirstOrDefault(c => c.Id == id);

            if (item == null)
                return NotFound(item);

            assignmentCommentsRepository.Delete(item);
            return Ok(item);
        }
    }
}
