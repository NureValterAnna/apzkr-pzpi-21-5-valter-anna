using Application.Medicines.Commands.CreateMedicine;
using Application.Medicines.Commands.DeleteMedicine;
using Application.Medicines.Queries.GetMedicines;
using Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddMedicine([FromBody] CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteMedicineCommand { Id = id }, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllMedicines(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetMedicinesQuery(), cancellationToken);
            return Ok(response);
        }
    }
}
