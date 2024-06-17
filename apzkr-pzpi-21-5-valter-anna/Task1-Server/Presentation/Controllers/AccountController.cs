using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = _mediator.Send(request, cancellationToken);
            return Ok(JsonSerializer.Serialize(response.Result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request, CancellationToken cancellationToken)
        {
            var response = _mediator.Send(request, cancellationToken);
            return Ok(JsonSerializer.Serialize(response.Result));
        }
    }
}
