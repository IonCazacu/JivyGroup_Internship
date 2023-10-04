using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using server2.Services.UserServices.Model;

namespace server2.Services.UserServices.Authorization.Basic.Attributes
{
    // Decorator [Authorize] - restricts access to controllers or specified
    // action methods except for methods decorated with the [AllowAnonymous]

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute
        : Attribute
        , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authentication if action is decorated with [AllowAnonymous]
            var allowAnonymous =
                context.ActionDescriptor.EndpointMetadata.
                OfType < AllowAnonymousAttribute > ().
                Any();
            if (allowAnonymous) {
                return;
            }

            User? user = context.HttpContext.Items["User"] as User;
            if (user == null) {
                // if not logged in return 401 Unauthorized
                context.Result = new JsonResult (
                    new { message = "Unauthorized" })
                    { StatusCode = StatusCodes.Status401Unauthorized };

                // set 'WWW-Authenticate' header to trigger login popup in
                // browsers
                context.HttpContext.Response.Headers.Add(
                    "WWW-Authenticate", "Basic");
            }
        }
    }
}
