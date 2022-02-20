using Common.AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerAPI.ViewModels.Members;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromBody] PagerVM pager, int projectId)
        {
            pager ??= new PagerVM();
            pager.Page = pager.Page <= 0
                ? GlobalConstants.DefaultPage
                : pager.Page;
            pager.ItemsPerPage = pager.ItemsPerPage <= 0
                ? GlobalConstants.DefaultItemsPerPage
                : pager.ItemsPerPage;

            ProjectToMembersRepository projectToMembersRepository = new ProjectToMembersRepository();
            GetVM model = new GetVM();
            model.Items = Mapper.Instance().Map<List<MemberDTO>>(projectToMembersRepository.GetAll(m => m.ProjectId == projectId,
                                                                                                    pager.Page,
                                                                                                    pager.ItemsPerPage));
            model.Pager = pager;
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Put(int projectId, int userId)
        {
            ProjectToMembersRepository projectToMembersRepo = new ProjectToMembersRepository();

            ProjectToMember item = new ProjectToMember();
            item.ProjectId = projectId;
            item.MemberId = userId;

            projectToMembersRepo.Save(item);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int projectId, int userId)
        {
            ProjectToMembersRepository projectToMembersRepository = new ProjectToMembersRepository();
            ProjectToMember item = projectToMembersRepository.FirstOrDefault(i => i.ProjectId == projectId &&
                                                                                  i.MemberId == userId);

            if (item != null)
            {
                projectToMembersRepository.Delete(item);
                return Ok(new { projectId = item.ProjectId, memberId = item.MemberId });
            }

            return NotFound(item);
        }
    }
}
