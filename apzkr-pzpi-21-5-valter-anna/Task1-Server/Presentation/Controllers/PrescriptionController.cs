using Application.MedicineIntakeInformation.Queries.GetMedicineIntakeInformation;
using Application.MedicineIntakeInformation.Queries.GetPercentageOfMedicineTaken;
using Application.Prescriptions.Commands.CreatePrescription;
using Application.Prescriptions.Commands.DeletePrescription;
using Application.Prescriptions.Commands.UpdatePrescription;
using Application.Prescriptions.Queries.GetPrescriptionsByEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrescriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "doctor, admin")]
        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "doctor, admin")]
        [HttpPost("update-dose")]
        public async Task<IActionResult> UpdatePrescription([FromBody] UpdatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "doctor, admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeletePrescriptionCommand { Id = id }, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPrescriptionByEmail(CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
            var request = new GetPrescriptionsByEmailQuery();
            request.Email = email;

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}/medicine-intake-information")]
        public async Task<IActionResult> GetMedicineIntakeInformation(int id ,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetMedicineIntakeInformationQuery { PrescriptionId = id}, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}/percentage-of-medicine-taken")]
        public async Task<IActionResult> GetPercentageOfMedicineTakenQuery(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPercentageOfMedicineTakenQuery { PrescriptionId = id }, cancellationToken);
            return Ok(response);
        }
    }
}
