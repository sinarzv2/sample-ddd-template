using Application.AccountApplication.Command;
using Common.Constant;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.V1
{
    [ApiVersion("1")]
    public class AccountController : BaseV1Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ConstantRoute.Action)]
        [SwaggerOperation("Register user")]
        public async Task<IActionResult> Register(RegisterUserCommand registerRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(registerRequest, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return NoContent();
        }

        [HttpPost(ConstantRoute.Action)]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> Login([FromForm]LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result.Data);
        }
    }
}
