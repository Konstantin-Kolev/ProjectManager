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
    public class TokensController : ControllerBase
    {
        [HttpPut]
        public IActionResult Put(string username, string password)
        {
            UsersRepository usersRepository = new UsersRepository();
            User item = usersRepository.FirstOrDefault(u => u.Username == username
                                                            && u.Password == password);

            if (item == null)
                return NotFound(item);

            var Claims = new[]
            {
                new Claim(GlobalConstants.LoggedUserKey, item.Id.ToString())
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

            //Creating new Refresh Token for this user
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.Token =  Guid.NewGuid().ToString();
            refreshToken.IsUsed = false;
            refreshToken.UserId = item.Id;

            //Invalidating all other refresh tokens for this user
            RefreshTokensRepository refreshTokensRepository = new RefreshTokensRepository();
            refreshTokensRepository.InvalidateTokens(item.Id);
            //Adding the new refresh token
            refreshTokensRepository.Save(refreshToken);

            return Ok(new { success = true, token = jwt, refreshToken = refreshToken.Token });
        }
    }
}
