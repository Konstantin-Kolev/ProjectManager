using Common.Entities;
using Common.Repositories;
using Common.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokensController : ControllerBase
    {
        [Route("Refresh/{refreshToken}")]
        [HttpPut]
        public IActionResult Put(string refreshToken)
        {
            RefreshTokensRepository repo = new RefreshTokensRepository();
            RefreshToken item = repo.FirstOrDefault(t => !t.IsUsed && t.Token==refreshToken);

            if (item == null)
                return NotFound(item);

            var Claims = new[]
            {
                new Claim(GlobalConstants.LoggedUserKey, item.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!Password123!Password123"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "projectmanager.server",
                "projectmanagement.andular.app",
                Claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            item.IsUsed = true;
            repo.Save(item);

            return Ok(new { success = true, token = jwt });
        }
    }
}
