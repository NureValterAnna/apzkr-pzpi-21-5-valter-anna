using Application.Dispensers.Commands.CreateDispenser;
using Application.Dispensers.Commands.DeleteDispenser;
using Application.Dispensers.Commands.UpdateDispenser;
using Application.Dispensers.Queries.GetDispensers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispenserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DispenserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddDispenser([FromBody] CreateDispenserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispenser(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteDispenserCommand { Id = id }, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllDispensers([FromQuery] string? temperatureUnit, CancellationToken cancellationToken)
        {
            if (temperatureUnit != "C" && temperatureUnit != "F")
            {
                return BadRequest("Invalid temperature unit. Please specify 'C' or 'F'.");
            }

            var response = await _mediator.Send(new GetDispensersQuery { TemperatureUnit = temperatureUnit}, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateDispenserLocationCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
