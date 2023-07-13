using Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [FluentResult]
    public class BaseController : ControllerBase
    {
    }
}
