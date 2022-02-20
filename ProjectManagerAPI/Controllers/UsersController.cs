using Common.AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerAPI.ViewModels.Shared;
using ProjectManagerAPI.ViewModels.Users;
using System.Collections.Generic;

namespace ProjectManagerAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromBody] GetVM model)
        {
            model.Filter ??= new FilterVM();

            model.Pager ??= new PagerVM();
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? GlobalConstants.DefaultItemsPerPage
                                        : model.Pager.ItemsPerPage;
            model.Pager.Page = model.Pager.Page <= 0
                                ? GlobalConstants.DefaultPage
                                : model.Pager.Page;

            var filter = model.Filter.GetFilter();

            UsersRepository usersRepository = new UsersRepository();
            model.Items = Mapper.Instance().Map<List<UserDTO>>(usersRepository.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage));
            model.Pager.PagesCount = (int)Math.Ceiling(usersRepository.Count(filter) / (double)model.Pager.ItemsPerPage);

            return Ok(model.Items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UsersRepository usersRepository = new UsersRepository();
            UserDTO item = Mapper.Instance().Map<UserDTO>(usersRepository.FirstOrDefault(u => u.Id == id));

            if (item == null)
                return NotFound(item);

            return Ok(item);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CreateVM model)
        {
            UsersRepository userRepository = new UsersRepository();

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            userRepository.Save(item);

            return Created(item.Id.ToString(),new { username = item.Username});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UsersRepository usersRepository = new UsersRepository();
            User item = usersRepository.FirstOrDefault(u => u.Id == id);

            if (item != null)
            {
                usersRepository.Delete(item);
                return Ok(new { succes = true, userId = item.Id, username = item.Username });
            }

            return NotFound(item);
        }
    }
}
