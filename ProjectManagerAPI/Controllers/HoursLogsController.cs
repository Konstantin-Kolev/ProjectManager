using Common.AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerAPI.Tools;
using ProjectManagerAPI.ViewModels.HoursLogs;
using ProjectManagerAPI.ViewModels.Shared;

namespace ProjectManagerAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class HoursLogsController : ControllerBase
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

            HoursLogsRepository hoursLogsRepository = new HoursLogsRepository();
            GetVM model = new GetVM();
            model.Items = Mapper.Instance().Map<List<HoursLogDTO>>(hoursLogsRepository.GetAll(c => c.AssignmentId == assignmentId,
                                                                                                       pager.Page,
                                                                                                       pager.ItemsPerPage));
            model.Pager = pager;
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CreateVM model)
        {
            AuthorizedUser user = new AuthorizedUser(User);

            HoursLogsRepository hoursLogsRepository = new HoursLogsRepository();
            HoursLog item = new HoursLog();
            item.Hours = model.Hours;
            item.AssignmentId = model.AssignmentId;
            item.CreatedOn = DateTime.Now;
            item.UserId = user.Id;

            hoursLogsRepository.Save(item);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            HoursLogsRepository hoursLogsRepository = new HoursLogsRepository();
            HoursLog item = hoursLogsRepository.FirstOrDefault(h => h.Id == id);

            if (item == null)
                return NotFound(item);

            hoursLogsRepository.Delete(item);
            return Ok(new { success = true, id = item.Id });
        }

        [Route("Report")]
        [HttpGet]
        public IActionResult GetReport([FromBody] ReportVM model)
        {
            HoursLogsReportDTO report = new HoursLogsReportDTO();
            report.Items = new Dictionary<string, Dictionary<string, int>>();

            HoursLogsRepository hoursLogsRepository = new HoursLogsRepository();
            List<HoursLog> logsForReport = hoursLogsRepository.GetAll(l => model.Users.Contains(l.UserId) && l.CreatedOn <= model.End && l.CreatedOn >= model.Start);

            foreach (var log in logsForReport)
            {
                if (!report.Items.ContainsKey(log.User.Username))
                {
                    report.Items.Add(log.User.Username, new Dictionary<string, int>());
                }

                if (!report.Items[log.User.Username].ContainsKey(log.CreatedOn.Date.ToString()))
                {
                    report.Items[log.User.Username].Add(log.CreatedOn.Date.ToString(), 0);
                }

                report.Items[log.User.Username][log.CreatedOn.Date.ToString()] += log.Hours;
            }

            return Ok(report);
        }
    }
}
