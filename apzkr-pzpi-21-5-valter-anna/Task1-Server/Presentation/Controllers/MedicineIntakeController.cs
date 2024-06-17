using Application.Dispensers.Commands.CreateDispenser;
using Application.MedicineIntake.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineIntakeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicineIntakeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MddicineIntake([FromBody] MedicineIntakeCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
