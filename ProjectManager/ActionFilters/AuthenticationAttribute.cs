using Common.Entities;
using Common.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManagerWeb.Extensions;

namespace ProjectManagerWeb.ActionFilters
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObject<User>(GlobalConstants.LoggedUserKey) == null)
                context.Result = new RedirectToActionResult("Login", "Home", new { });
        }
    }
}
