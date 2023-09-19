using Application.AccountApplication.Queries;
using Common.Constant;
using Common.Models;
using Common.Resources.Messages;
using Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Api.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        public CustomAuthorizeAttribute(string permission)
        {
            _permission = permission;
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var apiResult = new FluentResult();
            var identity = context.HttpContext.User.Identity;
            var userId = identity?.GetUserId();
            if (userId == null)
            {
                apiResult.AddError(Errors.YouAreNotLoggedIn);
                context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var siteSettings = context.HttpContext.RequestServices.GetRequiredService<IOptionsSnapshot<SiteSettings>>().Value;
            if (siteSettings.UseTokenClaim)
            {
                var isPermitted = identity?.FindClaimByType(ConstantClaim.Permission)?.Any(d => d.Value == _permission);
                if (isPermitted.HasValue && isPermitted.Value) return;


                apiResult.AddError(Errors.AuthorizationFaild);
                context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status403Forbidden };

            }
            else
            {
                var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
                var permissionsResult = await mediator.Send(new GetClaimsByTypeQuery()
                {
                    Type = ConstantClaim.Permission,
                    UserId = Guid.Parse(userId)
                });
                if (permissionsResult.IsSuccess)
                {
                    var isPermitted = permissionsResult.Data.Any(d => d.ClaimValue == _permission);
                    if (isPermitted) return;

                    apiResult.AddError(Errors.AuthorizationFaild);
                    context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status403Forbidden };

                }
                else
                {
                    apiResult.AddErrors(permissionsResult.Errors);
                    context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
        }
    }
}
