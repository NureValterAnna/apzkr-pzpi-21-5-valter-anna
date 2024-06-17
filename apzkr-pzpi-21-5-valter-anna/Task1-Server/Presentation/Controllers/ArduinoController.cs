using Application.Dispensers.Commands.UpdateDispenser;
using Application.Dispensers.Commands.UpdateDispenserTempereture;
using Application.Interfaces;
using Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArduinoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArduinoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("update-temperature")]
        public async Task<IActionResult> UpdateTemperature([FromBody] UpdateDispenserTemperetureCommand request, CancellationToken cancellationToken)
        {
            var response = _mediator.Send(request, cancellationToken);
            return Ok(response.Result);
        }
    }
}
