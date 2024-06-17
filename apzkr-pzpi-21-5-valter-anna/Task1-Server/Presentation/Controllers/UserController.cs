using Application.Users.Commands.ChangeRole;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Import;
using Application.Users.Queries.Export;
using Application.Users.Queries.GetUser;
using Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserQuery { Id = id }, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUsersQuery(), cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("export")]
        public async Task<IActionResult> ExportUsers(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ExportUsersQuery(), cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("import")]
        public async Task<IActionResult> ImportUsers([FromBody] ImportUsersCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send( request, cancellationToken);
            return Ok("Import successful");
        }
    }
}
