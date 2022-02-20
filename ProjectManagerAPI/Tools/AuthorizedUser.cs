using Common.Tools;
using System.Security.Claims;

namespace ProjectManagerAPI.Tools
{
    public class AuthorizedUser
    {
        public int Id { get; set; }

        public AuthorizedUser(ClaimsPrincipal user)
        {
            Id = int.Parse(user.Claims.Where(c => c.Type == GlobalConstants.LoggedUserKey).FirstOrDefault().Value);
        }
    }
}
