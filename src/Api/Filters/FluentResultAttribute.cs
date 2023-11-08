using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class FluentResultAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is BadRequestObjectResult { Value: ValidationProblemDetails value } badRequestObjectResult)
        {
            var result = new FluentResult<object>();
            var errorMessages = value.Errors.SelectMany(p => p.Value).Distinct();
            result.AddErrors(errorMessages);
            context.Result = new JsonResult(result) { StatusCode = badRequestObjectResult.StatusCode };
        }

        base.OnResultExecuting(context);
    }
}