//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Application.UserApplication.Services;
//using Common.Constant;
//using Common.Models;
//using Common.Resources.Messages;
//using Common.Utilities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;

//namespace SampleTemplate.Filters
//{
//    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
//    {
//        private readonly string _permission;
//        public CustomAuthorizeAttribute(string permission)
//        {
//            _permission = permission;
//        }


//        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//        {
//            var identity = context.HttpContext.User.Identity;
//            var userId = identity.GetUserId();
//            if (userId == null)
//            {
//                var apiResult = new ApiResult();
//                apiResult.AddError(Errors.YouAreNotLoggedIn);
//                context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status401Unauthorized };
//                return;
//            }

//            var siteSettings = context.HttpContext.RequestServices.GetRequiredService<IOptionsSnapshot<SiteSettings>>().Value;
//            if (siteSettings.UseTokenClaim)
//            {
//                var isPermitted = identity.FindClaimByType(ConstantClaim.Permission).Any(d => d.Value == _permission);
//                if (isPermitted) return;

//                {
//                    var apiResult = new ApiResult();
//                    apiResult.AddError(Errors.AuthorizationFaild);
//                    context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status403Forbidden };
//                }
//            }
//            else
//            {
//                var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
//                var permissions = await userService.GetClaimsByType(Guid.Parse(userId), ConstantClaim.Permission);

//                var isPermitted = permissions.Any(d => d.Value == _permission);
//                if (isPermitted) return;

//                {
//                    var apiResult = new ApiResult();
//                    apiResult.AddError(Errors.AuthorizationFaild);
//                    context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status403Forbidden };
//                }
//            }
//        }
//    }
//}
